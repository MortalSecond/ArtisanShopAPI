using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ArtisanShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCommissionRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommissionRequests",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    size = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    stone_coverage = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    frame = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    features = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    treatments = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    shipping = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    estimated_price = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    image_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    submitted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_read = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionRequests", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommissionRequests");
        }
    }
}
