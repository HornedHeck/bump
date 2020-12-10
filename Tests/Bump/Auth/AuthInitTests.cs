using System.Threading.Tasks;
using Bump.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Tests.Utils;

namespace Tests.Bump.Auth {

    public class AuthInitTests {

        private readonly Mock< IConfiguration > Configuration = new Mock< IConfiguration >();

        private readonly Mock< MockRoleManager > RoleManager = new Mock< MockRoleManager >();

        private readonly Mock< MockUserManager > UserManager = new Mock< MockUserManager >();

        private static BumpUser AdminQuery => It.Is< BumpUser >( u => u.UserName == AuthConstants.Admin );

        [Test]
        public async Task InitTest() {

            UserManager
                .Setup( um => um.CreateAsync(
                    AdminQuery ,
                    It.IsAny< string >()
                ) )
                .Returns( Task.FromResult( IdentityResult.Success ) );

            await AuthInitializer.InitializeAsync( UserManager.Object , RoleManager.Object , Configuration.Object );

            RoleManager.Verify(
                rm => rm.CreateAsync( It.IsAny< IdentityRole >() ) ,
                Times.Exactly( AuthConstants.Roles.Length )
            );

            UserManager.Verify( um => um.CreateAsync(
                AdminQuery ,
                It.IsAny< string >()
            ) );

            UserManager.Verify( um => um.AddToRolesAsync(
                AdminQuery ,
                AuthConstants.Roles
            ) );
        }

    }

}