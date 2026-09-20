using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MaintFieldsToProjectItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "tpm",
                table: "ProjectItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LiveOnServer",
                schema: "tpm",
                table: "ProjectItems",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "LiveServerDate",
                schema: "tpm",
                table: "ProjectItems",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "MaintStartDate",
                schema: "tpm",
                table: "ProjectItems",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaintValue",
                schema: "tpm",
                table: "ProjectItems",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Maintenance",
                schema: "tpm",
                table: "ProjectItems",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectItemStatus",
                schema: "tpm",
                table: "ProjectItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectItemType",
                schema: "tpm",
                table: "ProjectItems",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "tpm",
                table: "ProjectItems");

            migrationBuilder.DropColumn(
                name: "LiveOnServer",
                schema: "tpm",
                table: "ProjectItems");

            migrationBuilder.DropColumn(
                name: "LiveServerDate",
                schema: "tpm",
                table: "ProjectItems");

            migrationBuilder.DropColumn(
                name: "MaintStartDate",
                schema: "tpm",
                table: "ProjectItems");

            migrationBuilder.DropColumn(
                name: "MaintValue",
                schema: "tpm",
                table: "ProjectItems");

            migrationBuilder.DropColumn(
                name: "Maintenance",
                schema: "tpm",
                table: "ProjectItems");

            migrationBuilder.DropColumn(
                name: "ProjectItemStatus",
                schema: "tpm",
                table: "ProjectItems");

            migrationBuilder.DropColumn(
                name: "ProjectItemType",
                schema: "tpm",
                table: "ProjectItems");
        }
    }
}
