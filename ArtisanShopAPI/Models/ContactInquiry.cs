using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtisanShopAPI.Models
{
    [Table("contact_inquiries")]
    public class ContactInquiry
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
        [MaxLength(100)]
        [Column("email")]
        public string Email { get; set; }

        [MaxLength(20)]
        [Column("phone")]
        public string? Phone { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("inquiry_type")]
        public string InquiryType { get; set; }

        [Required]
        [MaxLength(2000)]
        [Column("message")]
        public string Message { get; set; }

        [Column("submitted_at")]
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        [Column("is_read")]
        public bool IsRead { get; set; } = false;
    }
}
