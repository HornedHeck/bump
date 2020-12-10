using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Bump.VM {

    public class UserRolesVM {

        public List< IdentityRole > Roles { get; set; }

        public ISet< string > EnabledRoles { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

    }

}