using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Bump.Auth
{
    public class AuthInitializer
    {
        private static readonly string[] Roles = {
            "Admin",
            "Moderator"
        };

        private const string AdminName = "Admin";
        private const string AdminPassword = "Admin+1";

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
                    UserName = AdminName
                };
                var res = await userManager.CreateAsync(admin, AdminPassword);
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