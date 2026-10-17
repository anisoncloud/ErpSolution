using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class StatusInLeaveApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LeaveStatus",
                schema: "hrm",
                table: "LeaveApplications",
                newName: "Status");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveApplications_EmployeeId_LeaveType_LeaveStatus",
                schema: "hrm",
                table: "LeaveApplications",
                newName: "IX_LeaveApplications_EmployeeId_LeaveType_Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "hrm",
                table: "LeaveApplications",
                newName: "LeaveStatus");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveApplications_EmployeeId_LeaveType_Status",
                schema: "hrm",
                table: "LeaveApplications",
                newName: "IX_LeaveApplications_EmployeeId_LeaveType_LeaveStatus");
        }
    }
}
