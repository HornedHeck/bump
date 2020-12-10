using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Bump.Auth {

    // ReSharper disable once ClassNeverInstantiated.Global
    public class ClaimsFactory : UserClaimsPrincipalFactory< BumpUser , IdentityRole > {

        public ClaimsFactory(
            UserManager< BumpUser > userManager
            , RoleManager< IdentityRole > roleManager
            , IOptions< IdentityOptions > optionsAccessor )
            : base( userManager , roleManager , optionsAccessor ) { }

        public override async Task< ClaimsPrincipal > CreateAsync( BumpUser user ) {
            var principal = await base.CreateAsync( user );
            if( !string.IsNullOrWhiteSpace( user.VisibleName ) )
                ( (ClaimsIdentity) principal.Identity ).AddClaims( new[] {
                    new Claim( ClaimTypes.GivenName , user.VisibleName )
                } );

            return principal;
        }

    }

}