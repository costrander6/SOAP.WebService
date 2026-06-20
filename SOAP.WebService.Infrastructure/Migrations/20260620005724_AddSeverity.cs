using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOAP.WebService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeverity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "ScanResults");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Timestamp",
                table: "WorkflowRuns",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Severity",
                table: "Findings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "WorkflowRuns");

            migrationBuilder.DropColumn(
                name: "Severity",
                table: "Findings");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Timestamp",
                table: "ScanResults",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
