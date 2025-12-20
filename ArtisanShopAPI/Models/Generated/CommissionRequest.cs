using System;
using System.Collections.Generic;

namespace ArtisanShopAPI.Models.Generated;

public partial class CommissionRequest
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string Size { get; set; } = null!;

    public string StoneCoverage { get; set; } = null!;

    public string Frame { get; set; } = null!;

    public string? Features { get; set; }

    public string? Treatments { get; set; }

    public string Shipping { get; set; } = null!;

    public decimal EstimatedPrice { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime SubmittedAt { get; set; }

    public bool IsRead { get; set; }

    public string? Message { get; set; }
}
