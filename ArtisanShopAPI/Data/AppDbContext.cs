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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Painting>().ToTable("paintings");
        }
    }
}
