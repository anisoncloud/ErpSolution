using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ManagerInEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmpManagerId",
                schema: "hrm",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                schema: "hrm",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                schema: "hrm",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_ManagerId",
                schema: "hrm",
                table: "Employees",
                column: "ManagerId",
                principalSchema: "identity",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_ManagerId",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ManagerId",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmpManagerId",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                schema: "hrm",
                table: "Employees");
        }
    }
}
