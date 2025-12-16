using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ArtisanShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPricingConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PricingConfigs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    size_small = table.Column<decimal>(type: "numeric", nullable: false),
                    size_medium = table.Column<decimal>(type: "numeric", nullable: false),
                    size_large = table.Column<decimal>(type: "numeric", nullable: false),
                    size_extra_large = table.Column<decimal>(type: "numeric", nullable: false),
                    frame_wooden = table.Column<decimal>(type: "numeric", nullable: false),
                    frame_mirror = table.Column<decimal>(type: "numeric", nullable: false),
                    frame_led_wooden = table.Column<decimal>(type: "numeric", nullable: false),
                    frame_led_mirror = table.Column<decimal>(type: "numeric", nullable: false),
                    stone_none = table.Column<decimal>(type: "numeric", nullable: false),
                    stone_light = table.Column<decimal>(type: "numeric", nullable: false),
                    stone_medium = table.Column<decimal>(type: "numeric", nullable: false),
                    stone_heavy = table.Column<decimal>(type: "numeric", nullable: false),
                    feature_clock = table.Column<decimal>(type: "numeric", nullable: false),
                    feature_diamond_strips = table.Column<decimal>(type: "numeric", nullable: false),
                    feature_studs = table.Column<decimal>(type: "numeric", nullable: false),
                    feature_leds = table.Column<decimal>(type: "numeric", nullable: false),
                    treatment_resin = table.Column<decimal>(type: "numeric", nullable: false),
                    treatment_geode = table.Column<decimal>(type: "numeric", nullable: false),
                    shipping_domestic = table.Column<decimal>(type: "numeric", nullable: false),
                    shipping_north_america = table.Column<decimal>(type: "numeric", nullable: false),
                    shipping_international = table.Column<decimal>(type: "numeric", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingConfigs", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PricingConfigs");
        }
    }
}
