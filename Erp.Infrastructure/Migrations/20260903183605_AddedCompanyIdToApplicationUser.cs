using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCompanyIdToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                schema: "identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "identity",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                schema: "identity",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                schema: "identity",
                table: "Users",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "identity",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Department",
                schema: "identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                schema: "identity",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
