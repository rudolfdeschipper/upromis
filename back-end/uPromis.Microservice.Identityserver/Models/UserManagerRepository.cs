using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using uPromis.APIUtils.APIMessaging;
using uPromis.Microservice.Identityserver.Data;
using uPromis.Services.Models;

namespace uPromis.Microservice.Identityserver.Models
{
    public class UserManagerRepository
    {
        readonly ApplicationDbContext context;

        public UserManagerRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public async Task<(List<UserDTO>, double)> GetList(SortAndFilterInformation sortAndFilterInformation, bool doPaging)
        {
            return await DoSortFilterAndPaging(sortAndFilterInformation, doPaging);
        }

        private async Task<(List<UserDTO>, double)> DoSortFilterAndPaging(SortAndFilterInformation sentModel, bool doPaging)
        {
            string whereClause = String.Empty;
            IQueryable<ApplicationUser> records = context.Users as DbSet<ApplicationUser>;
            int filteredCount = records.Count();

            if (sentModel != null)
            {
                string[] strings = {
                        "username",
                        "email",
                        "lastname",
                        "firstname",
                        ""
                };
                // filtering
                // column search is handled here:0
                if (sentModel.filtered != null)
                {
                    foreach (var item in sentModel.filtered)
                    {
                        if (!String.IsNullOrEmpty(item.value))
                        {
                            if (strings.Contains(item.id))
                            {
                                whereClause = whereClause + item.id + ".Contains(\"" + item.value + "\") &&";
                            }
                            else
                            {
                                whereClause = whereClause + item.id + ".ToString().Contains(\"" + item.value + "\") &&";
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(whereClause))
                    {
                        whereClause = whereClause[0..^2];
                        records = records.Where(whereClause);
                        filteredCount = await records.CountAsync();
                    }
                }
                // ordering
                if (sentModel.sorted != null)
                {
                    string orderBy = "";
                    foreach (var o in sentModel.sorted)
                    {
                        orderBy += " " + o.id + (o.desc ? " DESC" : " ASC") + ",";
                    }
                    if (orderBy.EndsWith(','))
                    {
                        orderBy = orderBy[0..^1];
                    }
                    if (!string.IsNullOrEmpty(orderBy))
                    {
                        records = records.OrderBy(orderBy);
                    }
                }

                // paging:
                if (doPaging)
                {
                    records = records
                        .Skip(sentModel.page * sentModel.pageSize)
                        .Take(sentModel.pageSize);
                }
            }
            return (await records.Select( u => Transform(u)).ToListAsync(), sentModel?.pageSize != 0 ? Math.Ceiling((double)(filteredCount / sentModel.pageSize)) : 1.0);
        }


        public ApplicationUser Get(string id)
        {
            var rec = context.Users.SingleOrDefault(r => r.Id == id);
            return rec;
        }

        private UserDTO Transform(ApplicationUser rec)
        {
            var claims = context.UserClaims.Where(r => r.UserId == rec.Id);
            UserDTO dto = new()
            {
                ID = rec.Id,
                Email = rec.Email,
                UserName = rec.UserName,
                Modifier = "Unchanged"
            };
            var firstName = claims.SingleOrDefault(t => t.ClaimType == "given_name")?.ClaimValue;
            var lastName = claims.SingleOrDefault(t => t.ClaimType == "family_name")?.ClaimValue;
            dto.FirstName = firstName;
            dto.LastName = lastName;

            return dto;
        }

        public UserDTO GetDTO(string id)
        {
            var rec = Get(id);
            if (rec != null)
            {
                return Transform(rec);
            }
            else
            {
                return null;
            }
        }

        public UserDTO Post(UserDTO rec)
        {
            ApplicationUser toPost = new()
            {
                Email = rec.Email,
                UserName = rec.UserName
            };

            context.Add(toPost);

            if( context.SaveChanges() > 0 )
            {
                context.Add(new IdentityUserClaim<string>() { UserId = toPost.Id, ClaimType = "given_name", ClaimValue = rec.FirstName });
                context.Add(new IdentityUserClaim<string>() { UserId = toPost.Id, ClaimType = "family_name", ClaimValue = rec.LastName });

                context.SaveChanges();

                return GetDTO(toPost.Id);

            }
            return null;

        }

        public UserDTO Put(UserDTO rec)
        {
            ApplicationUser toPut = Get(rec.ID);

            context.Update(toPut);

            toPut.Email = rec.Email;
            toPut.UserName = rec.UserName;

            if (context.SaveChanges() > 0)
            {
                var claims = context.UserClaims.Where(r => r.UserId == rec.ID);

                var firstName = claims.SingleOrDefault(t => t.ClaimType == "given_name");
                var lastName = claims.SingleOrDefault(t => t.ClaimType == "family_name");

                firstName.ClaimValue = rec.FirstName;
                lastName.ClaimValue = rec.LastName;

                context.SaveChanges();

                return GetDTO(toPut.Id);

            }
            return null;
        }

        public bool Delete(string ID)
        {
            var toDelete = Get(ID);

            var claimsToDelete = context.UserClaims.Where(r => r.UserId == ID);

            context.UserClaims.RemoveRange(
                claimsToDelete
            );

            context.Users.Remove(toDelete);

            return context.SaveChanges() > 0;
        }
    }
}
