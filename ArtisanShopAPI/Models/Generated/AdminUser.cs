using System;
using System.Collections.Generic;

namespace ArtisanShopAPI.Models.Generated;

public partial class AdminUser
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? LastLogin { get; set; }
}
