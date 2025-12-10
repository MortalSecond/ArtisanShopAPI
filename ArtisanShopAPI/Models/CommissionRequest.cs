using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtisanShopAPI.Models
{
    public class CommissionRequest
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength (100)]
        [Column("email")]
        public string Email { get; set; }

        [MaxLength(20)]
        [Column("phone")]
        public string? Phone { get; set; }

        [MaxLength(1000)]
        [Column("message")]
        public string? Message { get; set; }

        // Commission Configuration
        [Required]
        [MaxLength(20)]
        [Column("size")]
        public string Size { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("stone_coverage")]
        public string StoneCoverage { get; set; }

        [MaxLength(20)]
        [Column("frame")]
        public string Frame { get; set; }

        [MaxLength(200)]
        [Column("features")]
        public string? Features { get; set; }

        [MaxLength(200)]
        [Column("treatments")]
        public string? Treatments { get; set; }

        [MaxLength(20)]
        [Column("shipping")]
        public string Shipping { get; set; }

        [Column("estimated_price", TypeName = "decimal(10,2)")]
        public decimal EstimatedPrice { get; set; }

        // Optional Painting Image

        [MaxLength(500)]
        [Column("image_url")]
        public string? ImageUrl { get; set; }

        [Column("submitted_at")]
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        [Column("is_read")]
        public bool IsRead { get; set; } = false;
    }
}
