using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyApplicationUserAndEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_UserId",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Salary",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCode",
                schema: "identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmationDate",
                schema: "hrm",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                schema: "hrm",
                table: "Employees",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_UserId",
                schema: "hrm",
                table: "Employees",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_UserId",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeCode",
                schema: "identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ConfirmationDate",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                schema: "hrm",
                table: "Employees",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                schema: "hrm",
                table: "Employees",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_UserId",
                schema: "hrm",
                table: "Employees",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
