using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Bump.Models
{
    public class UserRolesViewModel
    {
        public List<IdentityRole> Roles { get; set; }

        public ISet<string> EnabledRoles { get; set; }

        public string Name { get; set; }
        
        public string UserId { get; set; }
    }
}