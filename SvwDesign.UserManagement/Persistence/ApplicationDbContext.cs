using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SvwDesign.UserManagement
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    { 

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }
         
         
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.HasDefaultSchema("dbo");

            builder.Entity<ApplicationUser>()
                .Property(w => w.LastUpdateDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Entity<ApplicationUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(builder);
        } 
    }
}
