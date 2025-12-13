using ArtisanShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtisanShopAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Painting> Paintings { get; set; } = null!;
        public DbSet<ContactInquiry> ContactInquiries { get; set; }
        public DbSet<CommissionRequest> CommissionRequests { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Painting>().ToTable("paintings");
        }
    }
}
