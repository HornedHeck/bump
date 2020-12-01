using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using static Bump.Auth.AuthConstants;

namespace Bump.Auth
{
    public class AuthInitializer
    {
        private const string AdminName = "Admin";
        private const string AdminPassword = "AdminPassword";


        public static async Task InitializeAsync(UserManager<BumpUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await ConfigureRoles(roleManager);
            await ConfigureAdmin(userManager);
        }

        private static async Task ConfigureAdmin(UserManager<BumpUser> userManager)
        {
            if (await userManager.FindByNameAsync(AdminName) == null)
            {
                var admin = new BumpUser
                {
                    UserName = AdminName,
                    VisibleName = AdminName
                };
                var res = await userManager.CreateAsync(
                    admin,
                    Environment.GetEnvironmentVariable(AdminPassword)
                );

                if (res.Succeeded)
                {
                    await userManager.AddToRolesAsync(admin, Roles);
                }
            }
        }

        private static async Task ConfigureRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Roles)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}