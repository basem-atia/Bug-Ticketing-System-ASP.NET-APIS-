using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTicketingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class last : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedAt",
                table: "FileAttachments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "FileAttachments",
                keyColumn: "Id",
                keyValue: new Guid("2cdbc40a-b617-4858-98bd-71edd758ee68"),
                column: "UploadedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "FileAttachments",
                keyColumn: "Id",
                keyValue: new Guid("a289b995-a053-4318-8555-a5a6d9e5f75d"),
                column: "UploadedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "FileAttachments",
                keyColumn: "Id",
                keyValue: new Guid("e3ba3e0f-d2f5-406a-8fd8-70e9eca99e50"),
                column: "UploadedAt",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedAt",
                table: "FileAttachments");
        }
    }
}
