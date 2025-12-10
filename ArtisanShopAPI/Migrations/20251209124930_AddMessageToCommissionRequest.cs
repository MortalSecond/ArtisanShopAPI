using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtisanShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMessageToCommissionRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "message",
                table: "CommissionRequests",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "message",
                table: "CommissionRequests");
        }
    }
}
