using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bump.Auth
{
    public class BumpUserContext : IdentityDbContext<BumpUser>
    {
        public BumpUserContext(DbContextOptions<BumpUserContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // builder
                // .Entity<BumpUser>()
                // .HasIndex(it => it.VisibleName)
                // .IsUnique();
        }
    }
}