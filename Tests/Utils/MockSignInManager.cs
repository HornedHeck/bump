using Bump.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests.Utils
{
    public class MockSignInManager : SignInManager<BumpUser>
    {
        public MockSignInManager(UserManager<BumpUser> userManager) : base(
            userManager: userManager,
            contextAccessor: new HttpContextAccessor(),
            claimsFactory: new Mock<IUserClaimsPrincipalFactory<BumpUser>>().Object,
            optionsAccessor: new Mock<IOptions<IdentityOptions>>().Object,
            logger: new Mock<ILogger<SignInManager<BumpUser>>>().Object,
            schemes: new Mock<IAuthenticationSchemeProvider>().Object,
            confirmation: new Mock<IUserConfirmation<BumpUser>>().Object
        )
        {
        }
    }
}