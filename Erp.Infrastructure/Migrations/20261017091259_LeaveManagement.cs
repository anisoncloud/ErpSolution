using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LeaveManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_ManagerId",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ManagerId",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectItemStatus",
                schema: "tpm",
                table: "ProjectItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeCode",
                schema: "hrm",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "CompensationGrants",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    GrantedById = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompensationGrants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompensationGrants_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompensationGrants_Employees_GrantedById",
                        column: x => x.GrantedById,
                        principalSchema: "hrm",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveApplications",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LeaveType = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: false),
                    LeaveStatus = table.Column<int>(type: "int", nullable: false),
                    DecidedById = table.Column<int>(type: "int", nullable: false),
                    ManagerComment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveApplications_Employees_DecidedById",
                        column: x => x.DecidedById,
                        principalSchema: "hrm",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveApplications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveEntitlements",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    EmployeeType = table.Column<int>(type: "int", nullable: false),
                    LeaveType = table.Column<int>(type: "int", nullable: false),
                    TotalDays = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveEntitlements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveEntitlements_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "hrm",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeavePolicies",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveType = table.Column<int>(type: "int", nullable: false),
                    YearlyDays = table.Column<int>(type: "int", nullable: false),
                    LifetimeMaxTimes = table.Column<int>(type: "int", nullable: false),
                    FixedDays = table.Column<int>(type: "int", nullable: false),
                    MinDays = table.Column<int>(type: "int", nullable: false),
                    CountAllCalendarDays = table.Column<bool>(type: "bit", nullable: false),
                    SplitByYear = table.Column<bool>(type: "bit", nullable: false),
                    RequiredGender = table.Column<int>(type: "int", nullable: true),
                    CarryForwardCap = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeavePolicies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveApplicationYears",
                schema: "hrm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveApplicationId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    WorkingDays = table.Column<int>(type: "int", nullable: false),
                    BridgeWeeklyDays = table.Column<int>(type: "int", nullable: false),
                    BridgePublicDays = table.Column<int>(type: "int", nullable: false),
                    CarryForwardDays = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveApplicationYears", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveApplicationYears_LeaveApplications_LeaveApplicationId",
                        column: x => x.LeaveApplicationId,
                        principalSchema: "hrm",
                        principalTable: "LeaveApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeCode",
                schema: "hrm",
                table: "Employees",
                column: "EmployeeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmpManagerId",
                schema: "hrm",
                table: "Employees",
                column: "EmpManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_CompensationGrants_EmployeeId",
                schema: "hrm",
                table: "CompensationGrants",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompensationGrants_GrantedById",
                schema: "hrm",
                table: "CompensationGrants",
                column: "GrantedById");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_Date",
                schema: "hrm",
                table: "Holidays",
                column: "Date",
                unique: true,
                filter: "[IsDeleted]=0");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplications_DecidedById",
                schema: "hrm",
                table: "LeaveApplications",
                column: "DecidedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplications_EmployeeId_LeaveType_LeaveStatus",
                schema: "hrm",
                table: "LeaveApplications",
                columns: new[] { "EmployeeId", "LeaveType", "LeaveStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplicationYears_LeaveApplicationId",
                schema: "hrm",
                table: "LeaveApplicationYears",
                column: "LeaveApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplicationYears_Year_LeaveApplicationId",
                schema: "hrm",
                table: "LeaveApplicationYears",
                columns: new[] { "Year", "LeaveApplicationId" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveEntitlements_EmployeeId",
                schema: "hrm",
                table: "LeaveEntitlements",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveEntitlements_Year_EmployeeId_LeaveType",
                schema: "hrm",
                table: "LeaveEntitlements",
                columns: new[] { "Year", "EmployeeId", "LeaveType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeavePolicies_LeaveType",
                schema: "hrm",
                table: "LeavePolicies",
                column: "LeaveType",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_EmpManagerId",
                schema: "hrm",
                table: "Employees",
                column: "EmpManagerId",
                principalSchema: "hrm",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_EmpManagerId",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "CompensationGrants",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "Holidays",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "LeaveApplicationYears",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "LeaveEntitlements",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "LeavePolicies",
                schema: "hrm");

            migrationBuilder.DropTable(
                name: "LeaveApplications",
                schema: "hrm");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeCode",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmpManagerId",
                schema: "hrm",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectItemStatus",
                schema: "tpm",
                table: "ProjectItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeCode",
                schema: "hrm",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                schema: "hrm",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                schema: "hrm",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_ManagerId",
                schema: "hrm",
                table: "Employees",
                column: "ManagerId",
                principalSchema: "identity",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
