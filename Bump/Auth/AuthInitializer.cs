using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using static Bump.Auth.AuthConstants;

namespace Bump.Auth {

    public static class AuthInitializer {

        private const string AdminName = "Admin";
        private const string AdminPassword = "AdminPassword";


        public static async Task InitializeAsync( UserManager< BumpUser > userManager ,
            RoleManager< IdentityRole > roleManager , IConfiguration configuration ) {
            await ConfigureRoles( roleManager );
            await ConfigureAdmin( userManager , configuration );
        }

        private static async Task ConfigureAdmin( UserManager< BumpUser > userManager , IConfiguration configuration ) {
            if( await userManager.FindByNameAsync( AdminName ) == null ) {
                var admin = new BumpUser {
                    UserName = AdminName ,
                    VisibleName = AdminName
                };
                var res = await userManager.CreateAsync(
                    admin ,
                    //TODO secrets
                    // configuration[AdminPassword]
                    "Admin+1"
                );

                if( res.Succeeded ) await userManager.AddToRolesAsync( admin , Roles );
            }
        }

        private static async Task ConfigureRoles( RoleManager< IdentityRole > roleManager ) {
            foreach( var role in Roles )
                if( await roleManager.FindByNameAsync( role ) == null )
                    await roleManager.CreateAsync( new IdentityRole( role ) );
        }

    }

}