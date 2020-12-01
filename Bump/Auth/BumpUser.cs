using Entities;
using Microsoft.AspNetCore.Identity;

namespace Bump.Auth
{
    public class BumpUser : IdentityUser
    {
        public string VisibleName { get; set; }
    }
}