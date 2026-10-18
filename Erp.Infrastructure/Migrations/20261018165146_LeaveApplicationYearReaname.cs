using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LeaveApplicationYearReaname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BridgeWeeklyDays",
                schema: "hrm",
                table: "LeaveApplicationYears",
                newName: "BridgedWeeklyDays");

            migrationBuilder.RenameColumn(
                name: "BridgePublicDays",
                schema: "hrm",
                table: "LeaveApplicationYears",
                newName: "BridgedPublicDays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BridgedWeeklyDays",
                schema: "hrm",
                table: "LeaveApplicationYears",
                newName: "BridgeWeeklyDays");

            migrationBuilder.RenameColumn(
                name: "BridgedPublicDays",
                schema: "hrm",
                table: "LeaveApplicationYears",
                newName: "BridgePublicDays");
        }
    }
}
