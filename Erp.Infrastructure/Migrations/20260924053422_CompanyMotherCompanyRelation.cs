using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CompanyMotherCompanyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MotherCompanyId",
                schema: "crm",
                table: "Company",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Company_MotherCompanyId",
                schema: "crm",
                table: "Company",
                column: "MotherCompanyId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_MotherCompany_MotherCompanyId",
                schema: "crm",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_MotherCompanyId",
                schema: "crm",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "MotherCompanyId",
                schema: "crm",
                table: "Company");
        }
    }
}
