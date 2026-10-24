using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartmentHeadEmployeeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeadEmployeeId",
                schema: "hrm",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_HeadEmployeeId",
                schema: "hrm",
                table: "Departments",
                column: "HeadEmployeeId",
                unique: true,
                filter: "[HeadEmployeeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_HeadEmployeeId",
                schema: "hrm",
                table: "Departments",
                column: "HeadEmployeeId",
                principalSchema: "hrm",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_HeadEmployeeId",
                schema: "hrm",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_HeadEmployeeId",
                schema: "hrm",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "HeadEmployeeId",
                schema: "hrm",
                table: "Departments");
        }
    }
}
