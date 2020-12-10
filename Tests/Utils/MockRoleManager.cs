using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests.Utils {

    public class MockRoleManager : RoleManager< IdentityRole > {


        public MockRoleManager() : base(
            new Mock< IRoleStore< IdentityRole > >().Object ,
            null ,
            new Mock< ILookupNormalizer >().Object ,
            new Mock< IdentityErrorDescriber >().Object ,
            new Mock< ILogger< RoleManager< IdentityRole > > >().Object
        ) { }

    }

}