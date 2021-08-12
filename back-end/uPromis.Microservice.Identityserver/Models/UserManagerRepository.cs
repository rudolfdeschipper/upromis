using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Security.Claims;
using System.Threading.Tasks;
using uPromis.APIUtils.APIMessaging;
using uPromis.Microservice.Identityserver.Data;
using uPromis.Services.Models;

namespace uPromis.Microservice.Identityserver.Models
{
    public class UserManagerRepository
    {
        private readonly ApplicationDbContext context;

        public static readonly ListValue[] RoleValues =
            {
                new ListValue() { Value = "IsContractOwner", Label = "Contract Owner" } ,
                new ListValue() { Value = "IsContractParticipant", Label = "Contract Participant" } ,
                new ListValue() { Value = "IsContractReader", Label = "Contract Reader" } ,
                new ListValue() { Value = "IsProjectOwner", Label = "Project Owner" } ,
                new ListValue() { Value = "IsProjectParticipant", Label = "Project Participant" } ,
                new ListValue() { Value = "IsProjectReader", Label = "Project Reader" } ,
                new ListValue() { Value = "IsInAdminRole", Label = "uPromis Administrator" }
            };


        public UserManagerRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public async Task<(List<ApplicationUserDTO>, double)> GetList(SortAndFilterInformation sortAndFilterInformation, bool doPaging)
        {
            return await DoSortFilterAndPaging(sortAndFilterInformation, doPaging);
        }

        private async Task<(List<ApplicationUserDTO>, double)> DoSortFilterAndPaging(SortAndFilterInformation sentModel, bool doPaging)
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
            var list = await records.ToListAsync();
            var dtoList = list.Select(u => Transform(context, u));
            return (dtoList.ToList(), sentModel?.pageSize != 0 ? Math.Ceiling((double)(filteredCount / sentModel.pageSize)) : 1.0);
        }


        public ApplicationUser Get(string id)
        {
            var rec = context.Users.SingleOrDefault(r => r.Id == id);
            return rec;
        }

        private static ApplicationUserDTO Transform(ApplicationDbContext context, ApplicationUser rec)
        {
            var claims = context.UserClaims.Where(r => r.UserId == rec.Id);
            ApplicationUserDTO dto = new()
            {
                Id = rec.Id,
                Email = rec.Email,
                UserName = rec.UserName,
                Modifier = "Unchanged"
            };
            var firstName = claims.SingleOrDefault(t => t.ClaimType == "given_name")?.ClaimValue;
            var lastName = claims.SingleOrDefault(t => t.ClaimType == "family_name")?.ClaimValue;
            dto.FirstName = firstName;
            dto.LastName = lastName;

            // Add the roles
            var roles = claims
                .Where(r =>
                   (r.ClaimType.Contains("Contract")
                   || r.ClaimType.Contains("Project")
                   || r.ClaimType == UserBootstrapper.AdminRoleName)
                   && r.ClaimValue == "true")
                .Select(r => new UserRoleDTO() { Role = r.ClaimType,
                    Modifier = "Unchanged" });
            dto.Roles.AddRange(roles);

            // put in ID
            int row = 1;
            foreach (var item in dto.Roles)
            {
                item.ID = row++;
                item.RoleLabel = RoleValues.First(v => v.Value.Equals(item.Role))?.Label;
            }

            return dto;
        }

        private void SaveRoles(ApplicationUser user, ApplicationUserDTO dto)
        {
            foreach (var item in dto.Roles.Where(r => r.Modifier == "Deleted"))
            {
                var claim = context.UserClaims.FirstOrDefault(c => c.UserId == user.Id && c.ClaimType == item.Role);

                context.UserClaims.Remove(claim);
            }
            foreach (var item in dto.Roles.Where(r => r.Modifier == "Added" ||r.Modifier == "Modified"))
            {
                var oldClaim = context.UserClaims.FirstOrDefault(c => c.UserId == user.Id && c.ClaimType == item.Role);
                var claim = new IdentityUserClaim<string>
                {
                    ClaimType = item.Role,
                    UserId = user.Id,
                    ClaimValue = "true"
                };

                if (oldClaim != null)
                {
                    oldClaim.ClaimValue = "true";
                }
                else
                {
                    context.UserClaims.Add(claim);
                }
            }
        }

        public ApplicationUserDTO GetDTO(string id)
        {
            var rec = Get(id);
            if (rec != null)
            {
                return Transform(context, rec);
            }
            else
            {
                return null;
            }
        }

        public ApplicationUserDTO Post(ApplicationUserDTO rec)
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

                SaveRoles(toPost, rec);

                context.SaveChanges();

                return GetDTO(toPost.Id);

            }
            return null;

        }

        public ApplicationUserDTO Put(ApplicationUserDTO rec)
        {
            ApplicationUser toPut = Get(rec.Id);

            context.Update(toPut);

            toPut.Email = rec.Email;
            toPut.UserName = rec.UserName;

            if (context.SaveChanges() > 0)
            {
                var claims = context.UserClaims.Where(r => r.UserId == rec.Id);

                var firstName = claims.SingleOrDefault(t => t.ClaimType == "given_name");
                var lastName = claims.SingleOrDefault(t => t.ClaimType == "family_name");

                firstName.ClaimValue = rec.FirstName;
                lastName.ClaimValue = rec.LastName;

                SaveRoles(toPut, rec);

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
