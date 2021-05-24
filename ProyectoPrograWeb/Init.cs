using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPrograWeb
{
    public class Init
    {
        public static IdentityRole CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string strRole)
        {
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            IdentityRole? oRole = roleManager.FindByNameAsync(strRole).Result;
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            if (oRole == null)
            {
                oRole = new IdentityRole();
                oRole.Name = strRole;
                oRole.Id = Guid.NewGuid().ToString();
                IdentityResult roleResult = roleManager.
                CreateAsync(oRole).Result;
            }

            return oRole;
        }

        public static ApplicationUser CreateUserIfNotExistsAndAddRole(UserManager<ApplicationUser> userManager, string strEmail, string firstName, string lastName, string strPassword, string strRole)
        {
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            ApplicationUser? oUser = userManager.FindByNameAsync(strEmail).Result;
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

            if (oUser == null)
            {
                oUser = new ApplicationUser();
                oUser.UserName = strEmail;
                oUser.FirstName = firstName;
                oUser.LastName = lastName;
                oUser.Email = strEmail;
                oUser.EmailConfirmed = true;
                oUser.Id = Guid.NewGuid().ToString();

                IdentityResult result = userManager.CreateAsync(oUser, strPassword).Result;
            }

            if (oUser != null)
            {
                userManager.AddToRoleAsync(oUser, strRole).Wait();
            }

            return oUser;
        }
    }
}
