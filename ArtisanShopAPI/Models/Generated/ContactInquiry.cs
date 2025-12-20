using System;
using System.Collections.Generic;

namespace ArtisanShopAPI.Models.Generated;

public partial class ContactInquiry
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string InquiryType { get; set; } = null!;

    public string Message { get; set; } = null!;

    public DateTime SubmittedAt { get; set; }

    public bool IsRead { get; set; }

    public string? CommissionConfigJson { get; set; }
}
