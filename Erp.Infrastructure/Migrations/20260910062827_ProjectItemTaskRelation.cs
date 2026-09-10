using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProjectItemTaskRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_ProjectItems_ProjectItemId",
                schema: "tpm",
                table: "ProjectTasks");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTasks_ProjectItemId",
                schema: "tpm",
                table: "ProjectTasks");

            migrationBuilder.DropColumn(
                name: "ProjectItemId",
                schema: "tpm",
                table: "ProjectTasks");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_ProjectId",
                schema: "tpm",
                table: "ProjectTasks",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_ProjectItems_ProjectId",
                schema: "tpm",
                table: "ProjectTasks",
                column: "ProjectId",
                principalSchema: "tpm",
                principalTable: "ProjectItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTasks_ProjectItems_ProjectId",
                schema: "tpm",
                table: "ProjectTasks");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTasks_ProjectId",
                schema: "tpm",
                table: "ProjectTasks");

            migrationBuilder.AddColumn<int>(
                name: "ProjectItemId",
                schema: "tpm",
                table: "ProjectTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_ProjectItemId",
                schema: "tpm",
                table: "ProjectTasks",
                column: "ProjectItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTasks_ProjectItems_ProjectItemId",
                schema: "tpm",
                table: "ProjectTasks",
                column: "ProjectItemId",
                principalSchema: "tpm",
                principalTable: "ProjectItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
