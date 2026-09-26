using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NormalizeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                schema: "crm",
                table: "MotherCompany",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                schema: "crm",
                table: "Domains",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                schema: "hrm",
                table: "Designations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                schema: "hrm",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                schema: "crm",
                table: "Company",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                schema: "hrm",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedName",
                schema: "crm",
                table: "MotherCompany");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                schema: "crm",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                schema: "hrm",
                table: "Designations");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                schema: "hrm",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                schema: "crm",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                schema: "hrm",
                table: "Companies");
        }
    }
}
