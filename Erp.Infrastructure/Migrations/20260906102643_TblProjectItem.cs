using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TblProjectItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tpm");

            migrationBuilder.CreateTable(
                name: "ProjectItems",
                schema: "tpm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DemoStartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    WorkOrderDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ProjectDays = table.Column<int>(type: "int", nullable: true),
                    ProjectDeliveryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    ProjectValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Advanced = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProjectDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Proposal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftwareRequirement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectItems", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectItems",
                schema: "tpm");
        }
    }
}
