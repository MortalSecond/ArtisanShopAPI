using System;
using System.Collections.Generic;

namespace ArtisanShopAPI.Models.Generated;

public partial class PricingConfig
{
    public int Id { get; set; }

    public decimal SizeSmall { get; set; }

    public decimal SizeMedium { get; set; }

    public decimal SizeLarge { get; set; }

    public decimal SizeExtraLarge { get; set; }

    public decimal FrameWooden { get; set; }

    public decimal FrameMirror { get; set; }

    public decimal FrameLedWooden { get; set; }

    public decimal FrameLedMirror { get; set; }

    public decimal StoneNone { get; set; }

    public decimal StoneLight { get; set; }

    public decimal StoneMedium { get; set; }

    public decimal StoneHeavy { get; set; }

    public decimal FeatureClock { get; set; }

    public decimal FeatureDiamondStrips { get; set; }

    public decimal FeatureStuds { get; set; }

    public decimal FeatureLeds { get; set; }

    public decimal TreatmentResin { get; set; }

    public decimal TreatmentGeode { get; set; }

    public decimal ShippingDomestic { get; set; }

    public decimal ShippingNorthAmerica { get; set; }

    public decimal ShippingInternational { get; set; }

    public DateTime UpdatedAt { get; set; }
}
