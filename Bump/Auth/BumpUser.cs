using Entities;
using Microsoft.AspNetCore.Identity;

namespace Bump.Auth
{
    public class BumpUser : IdentityUser
    {
        public User Map()
        {
            return new User(Id);
        }
    }
}