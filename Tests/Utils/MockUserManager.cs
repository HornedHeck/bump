using System;
using Bump.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests.Utils {

    public class MockUserManager : UserManager< BumpUser > {

        public MockUserManager() : base(
            new Mock< IUserStore< BumpUser > >().Object ,
            new Mock< IOptions< IdentityOptions > >().Object ,
            new Mock< IPasswordHasher< BumpUser > >().Object ,
            new IUserValidator< BumpUser >[0] ,
            new IPasswordValidator< BumpUser >[0] ,
            new Mock< ILookupNormalizer >().Object ,
            new Mock< IdentityErrorDescriber >().Object ,
            new Mock< IServiceProvider >().Object ,
            new Mock< ILogger< UserManager< BumpUser > > >().Object ) { }

    }

}