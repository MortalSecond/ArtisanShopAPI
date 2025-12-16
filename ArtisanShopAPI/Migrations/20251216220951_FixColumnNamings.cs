using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtisanShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnNamings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PricingConfigs",
                table: "PricingConfigs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactInquiries",
                table: "ContactInquiries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommissionRequests",
                table: "CommissionRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers");

            migrationBuilder.RenameTable(
                name: "PricingConfigs",
                newName: "pricing_configs");

            migrationBuilder.RenameTable(
                name: "ContactInquiries",
                newName: "contact_inquiries");

            migrationBuilder.RenameTable(
                name: "CommissionRequests",
                newName: "commission_requests");

            migrationBuilder.RenameTable(
                name: "AdminUsers",
                newName: "admin_user");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "contact_inquiries",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "contact_inquiries",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "contact_inquiries",
                newName: "message");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "contact_inquiries",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "contact_inquiries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "SubmittedAt",
                table: "contact_inquiries",
                newName: "submitted_at");

            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "contact_inquiries",
                newName: "is_read");

            migrationBuilder.RenameColumn(
                name: "InquiryType",
                table: "contact_inquiries",
                newName: "inquiry_type");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pricing_configs",
                table: "pricing_configs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contact_inquiries",
                table: "contact_inquiries",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_commission_requests",
                table: "commission_requests",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_admin_user",
                table: "admin_user",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_pricing_configs",
                table: "pricing_configs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contact_inquiries",
                table: "contact_inquiries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_commission_requests",
                table: "commission_requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_admin_user",
                table: "admin_user");

            migrationBuilder.RenameTable(
                name: "pricing_configs",
                newName: "PricingConfigs");

            migrationBuilder.RenameTable(
                name: "contact_inquiries",
                newName: "ContactInquiries");

            migrationBuilder.RenameTable(
                name: "commission_requests",
                newName: "CommissionRequests");

            migrationBuilder.RenameTable(
                name: "admin_user",
                newName: "AdminUsers");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "ContactInquiries",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ContactInquiries",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "message",
                table: "ContactInquiries",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "ContactInquiries",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ContactInquiries",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "submitted_at",
                table: "ContactInquiries",
                newName: "SubmittedAt");

            migrationBuilder.RenameColumn(
                name: "is_read",
                table: "ContactInquiries",
                newName: "IsRead");

            migrationBuilder.RenameColumn(
                name: "inquiry_type",
                table: "ContactInquiries",
                newName: "InquiryType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PricingConfigs",
                table: "PricingConfigs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactInquiries",
                table: "ContactInquiries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommissionRequests",
                table: "CommissionRequests",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdminUsers",
                table: "AdminUsers",
                column: "id");
        }
    }
}
