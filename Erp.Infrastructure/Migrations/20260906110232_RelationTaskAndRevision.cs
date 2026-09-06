using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelationTaskAndRevision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectTaskId",
                schema: "tpm",
                table: "TaskRevisions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TaskRevisions_ProjectTaskId",
                schema: "tpm",
                table: "TaskRevisions",
                column: "ProjectTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskRevisions_ProjectTasks_ProjectTaskId",
                schema: "tpm",
                table: "TaskRevisions",
                column: "ProjectTaskId",
                principalSchema: "tpm",
                principalTable: "ProjectTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskRevisions_ProjectTasks_ProjectTaskId",
                schema: "tpm",
                table: "TaskRevisions");

            migrationBuilder.DropIndex(
                name: "IX_TaskRevisions_ProjectTaskId",
                schema: "tpm",
                table: "TaskRevisions");

            migrationBuilder.DropColumn(
                name: "ProjectTaskId",
                schema: "tpm",
                table: "TaskRevisions");
        }
    }
}
