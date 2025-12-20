using System;
using System.Collections.Generic;
using ArtisanShopAPI.Models.Generated;
using Microsoft.EntityFrameworkCore;

namespace ArtisanShopAPI.Data;

public partial class RailwayContext : DbContext
{
    public RailwayContext()
    {
    }

    public RailwayContext(DbContextOptions<RailwayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminUser> AdminUsers { get; set; }

    public virtual DbSet<CommissionRequest> CommissionRequests { get; set; }

    public virtual DbSet<ContactInquiry> ContactInquiries { get; set; }

    public virtual DbSet<Painting> Paintings { get; set; }

    public virtual DbSet<PricingConfig> PricingConfigs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=hopper.proxy.rlwy.net;Port=44448;Database=railway;Username=postgres;Password=nQqecNbMnWpZkVZXkZMCPBLVPtOPwhAv");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminUser>(entity =>
        {
            entity.ToTable("admin_users");

            entity.HasIndex(e => e.Email, "idx_admin_users_email").IsUnique();

            entity.HasIndex(e => e.Username, "idx_admin_users_username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.LastLogin).HasColumnName("last_login");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<CommissionRequest>(entity =>
        {
            entity.ToTable("commission_requests");

            entity.HasIndex(e => e.IsRead, "idx_commission_requests_is_read");

            entity.HasIndex(e => e.SubmittedAt, "idx_commission_requests_submitted_at");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.EstimatedPrice)
                .HasPrecision(10, 2)
                .HasColumnName("estimated_price");
            entity.Property(e => e.Features)
                .HasMaxLength(200)
                .HasColumnName("features");
            entity.Property(e => e.Frame)
                .HasMaxLength(20)
                .HasColumnName("frame");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .HasColumnName("image_url");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.Message)
                .HasMaxLength(1000)
                .HasColumnName("message");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Shipping)
                .HasMaxLength(20)
                .HasColumnName("shipping");
            entity.Property(e => e.Size)
                .HasMaxLength(20)
                .HasColumnName("size");
            entity.Property(e => e.StoneCoverage)
                .HasMaxLength(20)
                .HasColumnName("stone_coverage");
            entity.Property(e => e.SubmittedAt).HasColumnName("submitted_at");
            entity.Property(e => e.Treatments)
                .HasMaxLength(200)
                .HasColumnName("treatments");
        });

        modelBuilder.Entity<ContactInquiry>(entity =>
        {
            entity.ToTable("contact_inquiries");

            entity.HasIndex(e => e.IsRead, "idx_contact_inquiries_is_read");

            entity.HasIndex(e => e.SubmittedAt, "idx_contact_inquiries_submitted_at");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommissionConfigJson).HasMaxLength(5000);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.InquiryType)
                .HasMaxLength(50)
                .HasColumnName("inquiry_type");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.Message)
                .HasMaxLength(2000)
                .HasColumnName("message");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.SubmittedAt).HasColumnName("submitted_at");
        });

        modelBuilder.Entity<Painting>(entity =>
        {
            entity.ToTable("paintings");

            entity.HasIndex(e => e.Available, "idx_paintings_available");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Available)
                .HasDefaultValue(false)
                .HasColumnName("available");
            entity.Property(e => e.Details)
                .HasMaxLength(200)
                .HasColumnName("details");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(200)
                .HasColumnName("image_url");
            entity.Property(e => e.Price)
                .HasDefaultValue(0)
                .HasColumnName("price");
            entity.Property(e => e.Title)
                .HasMaxLength(20)
                .HasColumnName("title");
            entity.Property(e => e.Width).HasColumnName("width");
        });

        modelBuilder.Entity<PricingConfig>(entity =>
        {
            entity.ToTable("pricing_config");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FeatureClock).HasColumnName("feature_clock");
            entity.Property(e => e.FeatureDiamondStrips).HasColumnName("feature_diamond_strips");
            entity.Property(e => e.FeatureLeds).HasColumnName("feature_leds");
            entity.Property(e => e.FeatureStuds).HasColumnName("feature_studs");
            entity.Property(e => e.FrameLedMirror).HasColumnName("frame_led_mirror");
            entity.Property(e => e.FrameLedWooden).HasColumnName("frame_led_wooden");
            entity.Property(e => e.FrameMirror).HasColumnName("frame_mirror");
            entity.Property(e => e.FrameWooden).HasColumnName("frame_wooden");
            entity.Property(e => e.ShippingDomestic).HasColumnName("shipping_domestic");
            entity.Property(e => e.ShippingInternational).HasColumnName("shipping_international");
            entity.Property(e => e.ShippingNorthAmerica).HasColumnName("shipping_north_america");
            entity.Property(e => e.SizeExtraLarge).HasColumnName("size_extra_large");
            entity.Property(e => e.SizeLarge).HasColumnName("size_large");
            entity.Property(e => e.SizeMedium).HasColumnName("size_medium");
            entity.Property(e => e.SizeSmall).HasColumnName("size_small");
            entity.Property(e => e.StoneHeavy).HasColumnName("stone_heavy");
            entity.Property(e => e.StoneLight).HasColumnName("stone_light");
            entity.Property(e => e.StoneMedium).HasColumnName("stone_medium");
            entity.Property(e => e.StoneNone).HasColumnName("stone_none");
            entity.Property(e => e.TreatmentGeode).HasColumnName("treatment_geode");
            entity.Property(e => e.TreatmentResin).HasColumnName("treatment_resin");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
