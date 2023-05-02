using Identity_Session.Core.CrossCuttingConcern.Role.Microsoft;
using Identity_Session.Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity_Session.DataAccess.Concrete.EntityFramework.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=KURTULUSOCAL\\KURTULUSOCL;Database=IdentitySession;user Id=sa;Password=123");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Audit> Audits { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ExceptionLogger> ExceptionLoggers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProfilePhoto> ProfilePhotos { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
    }
}
