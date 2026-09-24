using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CompanyDomainRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                schema: "crm",
                table: "Domains",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Domains_CompanyId",
                schema: "crm",
                table: "Domains",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Domains_Company_CompanyId",
                schema: "crm",
                table: "Domains",
                column: "CompanyId",
                principalSchema: "crm",
                principalTable: "Company",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Domains_Company_CompanyId",
                schema: "crm",
                table: "Domains");

            migrationBuilder.DropIndex(
                name: "IX_Domains_CompanyId",
                schema: "crm",
                table: "Domains");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "crm",
                table: "Domains");
        }
    }
}
