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
            userManager,
            new HttpContextAccessor(),
            new Mock<IUserClaimsPrincipalFactory<BumpUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<ILogger<SignInManager<BumpUser>>>().Object,
            new Mock<IAuthenticationSchemeProvider>().Object,
            new Mock<IUserConfirmation<BumpUser>>().Object
        )
        {
        }
    }
}