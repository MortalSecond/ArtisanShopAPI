using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtisanShopAPI.Models
{
    [Table("pricing_config")]
    public class PricingConfig
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        // Sizes
        [Column("size_small")]
        public decimal SizeSmall { get; set; }

        [Column("size_medium")]
        public decimal SizeMedium { get; set; }

        [Column("size_large")]
        public decimal SizeLarge { get; set; }

        [Column("size_extra_large")]
        public decimal SizeExtraLarge { get; set; }

        // Frames
        [Column("frame_wooden")]
        public decimal FrameWooden { get; set; }

        [Column("frame_mirror")]
        public decimal FrameMirror { get; set; }

        [Column("frame_led_wooden")]
        public decimal FrameLedWooden { get; set; }

        [Column("frame_led_mirror")]
        public decimal FrameLedMirror { get; set; }

        // Stones
        [Column("stone_none")]
        public decimal StoneNone { get; set; }

        [Column("stone_light")]
        public decimal StoneLight { get; set; }

        [Column("stone_medium")]
        public decimal StoneMedium { get; set; }

        [Column("stone_heavy")]
        public decimal StoneHeavy { get; set; }

        // Features
        [Column("feature_clock")]
        public decimal FeatureClock { get; set; }

        [Column("feature_diamond_strips")]
        public decimal FeatureDiamondStrips { get; set; }

        [Column("feature_studs")]
        public decimal FeatureStuds { get; set; }

        [Column("feature_leds")]
        public decimal FeatureLeds { get; set; }

        // Treatments
        [Column("treatment_resin")]
        public decimal TreatmentResin { get; set; }

        [Column("treatment_geode")]
        public decimal TreatmentGeode { get; set; }

        // Shipping
        [Column("shipping_domestic")]
        public decimal ShippingDomestic { get; set; }

        [Column("shipping_north_america")]
        public decimal ShippingNorthAmerica { get; set; }

        [Column("shipping_international")]
        public decimal ShippingInternational { get; set; }

        // For record-keeping purposes
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}