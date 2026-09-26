using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mothercompanyidNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_MotherCompany_MotherCompanyId",
                schema: "crm",
                table: "Company");

            migrationBuilder.AlterColumn<int>(
                name: "MotherCompanyId",
                schema: "crm",
                table: "Company",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_MotherCompany_MotherCompanyId",
                schema: "crm",
                table: "Company",
                column: "MotherCompanyId",
                principalSchema: "crm",
                principalTable: "MotherCompany",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_MotherCompany_MotherCompanyId",
                schema: "crm",
                table: "Company");

            migrationBuilder.AlterColumn<int>(
                name: "MotherCompanyId",
                schema: "crm",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_MotherCompany_MotherCompanyId",
                schema: "crm",
                table: "Company",
                column: "MotherCompanyId",
                principalSchema: "crm",
                principalTable: "MotherCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
