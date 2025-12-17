using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtisanShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexesAndConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_pricing_configs",
                table: "pricing_configs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_admin_user",
                table: "admin_user");

            migrationBuilder.RenameTable(
                name: "pricing_configs",
                newName: "pricing_config");

            migrationBuilder.RenameTable(
                name: "admin_user",
                newName: "admin_users");

            migrationBuilder.AlterColumn<int>(
                name: "price",
                table: "paintings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_pricing_config",
                table: "pricing_config",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_admin_users",
                table: "admin_users",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "idx_paintings_available",
                table: "paintings",
                column: "available");

            migrationBuilder.CreateIndex(
                name: "idx_contact_inquiries_is_read",
                table: "contact_inquiries",
                column: "is_read");

            migrationBuilder.CreateIndex(
                name: "idx_contact_inquiries_submitted_at",
                table: "contact_inquiries",
                column: "submitted_at");

            migrationBuilder.CreateIndex(
                name: "idx_commission_requests_is_read",
                table: "commission_requests",
                column: "is_read");

            migrationBuilder.CreateIndex(
                name: "idx_commission_requests_submitted_at",
                table: "commission_requests",
                column: "submitted_at");

            migrationBuilder.CreateIndex(
                name: "idx_admin_users_email",
                table: "admin_users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_admin_users_username",
                table: "admin_users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_paintings_available",
                table: "paintings");

            migrationBuilder.DropIndex(
                name: "idx_contact_inquiries_is_read",
                table: "contact_inquiries");

            migrationBuilder.DropIndex(
                name: "idx_contact_inquiries_submitted_at",
                table: "contact_inquiries");

            migrationBuilder.DropIndex(
                name: "idx_commission_requests_is_read",
                table: "commission_requests");

            migrationBuilder.DropIndex(
                name: "idx_commission_requests_submitted_at",
                table: "commission_requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pricing_config",
                table: "pricing_config");

            migrationBuilder.DropPrimaryKey(
                name: "PK_admin_users",
                table: "admin_users");

            migrationBuilder.DropIndex(
                name: "idx_admin_users_email",
                table: "admin_users");

            migrationBuilder.DropIndex(
                name: "idx_admin_users_username",
                table: "admin_users");

            migrationBuilder.RenameTable(
                name: "pricing_config",
                newName: "pricing_configs");

            migrationBuilder.RenameTable(
                name: "admin_users",
                newName: "admin_user");

            migrationBuilder.AlterColumn<int>(
                name: "price",
                table: "paintings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_pricing_configs",
                table: "pricing_configs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_admin_user",
                table: "admin_user",
                column: "id");
        }
    }
}
