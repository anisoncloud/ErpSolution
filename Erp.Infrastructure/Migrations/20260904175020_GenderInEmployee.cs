using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GenderInEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerId",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                schema: "hrm",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                schema: "hrm",
                table: "Employees",
                type: "int",
                nullable: true);
        }
    }
}
