using System;
using Bump.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests.Utils
{
    public class MockUserManager : UserManager<BumpUser>
    {
        public MockUserManager() : base(
            store: new Mock<IUserStore<BumpUser>>().Object,
            optionsAccessor: new Mock<IOptions<IdentityOptions>>().Object,
            passwordHasher: new Mock<IPasswordHasher<BumpUser>>().Object,
            userValidators: new IUserValidator<BumpUser>[0],
            passwordValidators: new IPasswordValidator<BumpUser>[0],
            keyNormalizer: new Mock<ILookupNormalizer>().Object,
            errors: new Mock<IdentityErrorDescriber>().Object,
            services: new Mock<IServiceProvider>().Object,
            logger: new Mock<ILogger<UserManager<BumpUser>>>().Object)
        {
        }
    }
}