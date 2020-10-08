using System.Data.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bump.Auth
{
    public class BumpUserContext : IdentityDbContext<BumpUser, IdentityRole, string>
    {
        public BumpUserContext(DbContextOptions<BumpUserContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}