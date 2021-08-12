using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uPromis.Microservice.Identityserver.Data;

namespace uPromis.Microservice.Identityserver.Models
{
    public class UserBootstrapper
    {

        public const string AdminRoleName = "IsInAdminRole";
        private const string AdminRoleValue = "true";
        public const string DefaultAdminUser = "DefaultUser";

        /// <summary>
        /// Returns true of there is at least one user with the admin role and that user is not the default user
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AdminRoleExistForNonDefaultUser(UserManager<ApplicationUser> context)
        {
            return (await UsersWithAdminRole(context)).Any(u => u.UserName != DefaultAdminUser);
        }
        private static async Task<IList<ApplicationUser>> UsersWithAdminRole(UserManager<ApplicationUser> context)
        {
            return await context.GetUsersForClaimAsync(new System.Security.Claims.Claim(AdminRoleName, AdminRoleValue));
        }

        private static async Task<IEnumerable<IdentityError>> CreateDefaultAdminUser(UserManager<ApplicationUser> context)
        {
            IEnumerable<IdentityError> errors;
            ApplicationUser defaultAdmin = new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = DefaultAdminUser,
                NormalizedUserName = DefaultAdminUser,
                Email = "",
                EmailConfirmed = true,
                LockoutEnabled = false
            };

            System.Security.Claims.Claim claim = CreateAdminClaim();
            System.Security.Claims.Claim firstName = new("given_name", "Default");
            System.Security.Claims.Claim lastName = new("family_name", "User");

            // TODO: get pwd from the application setting
            errors = (await context.CreateAsync(defaultAdmin, "Admin123$")).Errors;
            if (!errors.Any())
            {
                errors = errors.Concat((await context.AddClaimAsync(defaultAdmin, claim)).Errors);
                errors = errors.Concat((await context.AddClaimAsync(defaultAdmin, firstName)).Errors);
                errors = errors.Concat((await context.AddClaimAsync(defaultAdmin, lastName)).Errors);
            }

            return errors;
        }

        private static ApplicationUser DefaultAdminUserExists(UserManager<ApplicationUser> context)
        {
            return context.Users.FirstOrDefault(u => u.UserName == DefaultAdminUser);
        }

        private static System.Security.Claims.Claim CreateAdminClaim()
        {
            return new System.Security.Claims.Claim(AdminRoleName, AdminRoleValue);
        }

        /// <summary>
        /// Check if users with admin role exist
        /// If so, ensure default user has no admin role
        /// If not, check for default user
        /// If it exists, add admin role
        /// If it does not exist, create it with the admin role
        /// Result: at least one user with the admin role exists
        /// Return true if the default user was created
        /// </summary>
        public async Task<(bool Created, IEnumerable<IdentityError> Errors)> EnsureAdminRoleExists(UserManager<ApplicationUser> context)
        {
            var user = DefaultAdminUserExists(context);

            if (await AdminRoleExistForNonDefaultUser(context))
            {
                if (user != null)
                {
                    await EnsureDefaultUserHasNoAdminRole(context, user);
                }
                return (false, null);
            }

            if (user != null) 
            {
                await EnsureDefaultUserHasAdminRole (context, user);
            }
            else
            {
                return (true, await CreateDefaultAdminUser(context));
            }
            return (false, null);
        }

        /// <summary>
        /// Once it is clear that at least one other user is admin, we can remove the admin
        /// claim from the default user.
        /// The call is idempotent, so can be called whether the default user has such a claim or not.
        /// It will ensure that after the call, no such claim exists
        /// </summary>
        /// <param name="defaultUser"></param>
        public async Task<bool> EnsureDefaultUserHasNoAdminRole(UserManager<ApplicationUser> context, ApplicationUser defaultUser)
        {
            // check if claim exists, if so, remove it
            var claim = (await context.GetClaimsAsync(defaultUser))?.FirstOrDefault(c => c.Type == AdminRoleName);
            if (claim != null)
            {
                await context.RemoveClaimAsync(defaultUser, claim);
            }
            return true;
        }
        /// <summary>
        /// Once it is clear that no other user is admin, we must assign it back to 
        /// the default user.
        /// The call is idempotent, so can be called whether the default user has such a claim or not.
        /// It will ensure that after the call, the claim exists
        /// </summary>
        /// <param name="defaultUser"></param>
        public async Task<bool> EnsureDefaultUserHasAdminRole(UserManager<ApplicationUser> context, ApplicationUser defaultUser)
        {
            // check if claim exists, if so, remove it
            var claim = (await context.GetClaimsAsync(defaultUser))?.FirstOrDefault(c => c.Type == AdminRoleName);
            if (claim != null)
            {
                await context.RemoveClaimAsync(defaultUser, claim);
            }
            // and add it again with the correct value
            claim = CreateAdminClaim();
            await context.AddClaimAsync(defaultUser, claim);
            return true;
        }
    }
}
