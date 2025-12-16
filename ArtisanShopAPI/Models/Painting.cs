using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtisanShopAPI.Models
{
    [Table("paintings")]
    public class Painting
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(20)]
        [Column("title")]
        public string? Title { get; set; }

        [MaxLength(200)]
        [Column("details")]
        public string? Details { get; set; }

        [MaxLength(200)]
        [Column("image_url")]
        public string? ImageURL { get; set; }

        [Column("height")]
        public int? Height { get; set; }

        [Column("width")]
        public int? Width { get; set; }

        [Column("price")]
        public int? Price { get; set; }

        [Column("available")]
        public bool Available { get; set; }
    }
}
