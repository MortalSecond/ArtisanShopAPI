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
        public DbSet<PricingConfig> PricingConfigs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Paintings
            modelBuilder.Entity<Painting>()
                .ToTable("paintings")
                .HasIndex(p=>p.Available)
                .HasDatabaseName("idx_paintings_available");
            modelBuilder.Entity<Painting>()
                .Property(p => p.Price)
                .HasColumnType("integer")
                .HasDefaultValue(0)
                .IsRequired();

            // Contact Inquiries
            modelBuilder.Entity<ContactInquiry>()
                .ToTable("contact_inquiries");

            modelBuilder.Entity<ContactInquiry>()
                .HasIndex(c => c.IsRead)
                .HasDatabaseName("idx_contact_inquiries_is_read");

            modelBuilder.Entity<ContactInquiry>()
                .HasIndex(c => c.SubmittedAt)
                .HasDatabaseName("idx_contact_inquiries_submitted_at");

            // Commission Requests
            modelBuilder.Entity<CommissionRequest>()
                .ToTable("commission_requests");

            modelBuilder.Entity<CommissionRequest>()
                .HasIndex(c => c.IsRead)
                .HasDatabaseName("idx_commission_requests_is_read");

            modelBuilder.Entity<CommissionRequest>()
                .HasIndex(c => c.SubmittedAt)
                .HasDatabaseName("idx_commission_requests_submitted_at");

            // Admin Users
            modelBuilder.Entity<AdminUser>()
                .ToTable("admin_users");

            modelBuilder.Entity<AdminUser>()
                .HasIndex(u => u.Username)
                .IsUnique()
                .HasDatabaseName("idx_admin_users_username");

            modelBuilder.Entity<AdminUser>()
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasDatabaseName("idx_admin_users_email");

            // Pricing Config (if you added this table)
            modelBuilder.Entity<PricingConfig>()
                .ToTable("pricing_config");
        }
    }
}
