using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOAP.WebService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddApiKeyAssociations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApiKeyHash",
                table: "WorkflowRuns",
                newName: "Owner");

            migrationBuilder.CreateTable(
                name: "ApiKeyAssociations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    KeyHash = table.Column<string>(type: "text", nullable: false),
                    Owner = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    RevokedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeyAssociations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiKeyAssociations");

            migrationBuilder.RenameColumn(
                name: "Owner",
                table: "WorkflowRuns",
                newName: "ApiKeyHash");
        }
    }
}
