using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ContactOfCrmUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Company_CrmCompanyId",
                schema: "crm",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "crm",
                table: "Contact");

            migrationBuilder.AlterColumn<int>(
                name: "CrmCompanyId",
                schema: "crm",
                table: "Contact",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Company_CrmCompanyId",
                schema: "crm",
                table: "Contact",
                column: "CrmCompanyId",
                principalSchema: "crm",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Company_CrmCompanyId",
                schema: "crm",
                table: "Contact");

            migrationBuilder.AlterColumn<int>(
                name: "CrmCompanyId",
                schema: "crm",
                table: "Contact",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                schema: "crm",
                table: "Contact",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Company_CrmCompanyId",
                schema: "crm",
                table: "Contact",
                column: "CrmCompanyId",
                principalSchema: "crm",
                principalTable: "Company",
                principalColumn: "Id");
        }
    }
}
