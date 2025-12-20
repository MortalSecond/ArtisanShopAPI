using System;
using System.Collections.Generic;

namespace ArtisanShopAPI.Models.Generated;

public partial class Painting
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Details { get; set; }

    public string? ImageUrl { get; set; }

    public int? Height { get; set; }

    public int? Width { get; set; }

    public int Price { get; set; }

    public bool Available { get; set; }
}
