using Entities;
using Microsoft.AspNetCore.Identity;

namespace Bump.Auth
{
    public class BumpUser : IdentityUser
    {
        
        [PersonalData]
        public string VisibleName { get; set; }
        
        public User Map()
        {
            return new User(Id);
        }
    }
}