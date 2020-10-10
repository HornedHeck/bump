using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Identity;

namespace Bump.Auth
{
    public static class IdentityExtensions
    {
        public static string VisibleName(this IIdentity identity)
        {
            return ((ClaimsIdentity) identity).FindFirst(ClaimTypes.GivenName)?.Value
                   ?? identity.Name
                   ?? string.Empty;
        }

        public static async Task<string> GetVisibleNameAsync(this UserManager<BumpUser> userManager, BumpUser user)
        {
            return (await userManager.GetClaimsAsync(user))
                   .FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName)?
                   .Value
                   ?? await userManager.GetUserNameAsync(user)
                   ?? string.Empty;
        }

        public static async Task<IdentityResult> SetVisibleNameAsync(
            this UserManager<BumpUser> userManager,
            BumpUser user,
            string visibleName)
        {
            var existingClaims = (await userManager.GetClaimsAsync(user))
                .FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName);

            var newClaim = new Claim(ClaimTypes.GivenName, visibleName);

            if (existingClaims != null)
            {
                return await userManager.ReplaceClaimAsync(user, existingClaims, newClaim);
            }

            return await userManager.AddClaimAsync(user, newClaim);
        }
    }
}