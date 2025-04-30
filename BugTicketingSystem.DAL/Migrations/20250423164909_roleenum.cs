using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BugTicketingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class roleenum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bugs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bugs_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BugUser",
                columns: table => new
                {
                    BugsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BugUser", x => new { x.BugsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_BugUser_Bugs_BugsId",
                        column: x => x.BugsId,
                        principalTable: "Bugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BugUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BugId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileAttachments_Bugs_BugId",
                        column: x => x.BugId,
                        principalTable: "Bugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("1c017e23-6987-4d3b-911f-d3cf432a1c19"), "Examination System For Students To Evaluate their Level of Understanding Each Subject They Studied", "Examination System" },
                    { new Guid("80fa2674-074a-4c48-8b6c-b63eb7ba60f1"), "E-Commerce Website to Practise On Angular And NodeJs", "E-Commerce Website" },
                    { new Guid("b8e4ce6b-3606-4535-882a-3ceaf55b6808"), "E-Commerce Website to Practise On Angular And Asp.Net Using Apis", "E-Commerce Website2" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { new Guid("a94a3ec6-77da-47b2-8428-bcbf372a3676"), "Tester" },
                    { new Guid("dc435455-5ae0-43ab-919b-3029b64fb488"), "Developer" },
                    { new Guid("f8b4285f-300e-4cd7-930c-f6f290faea0f"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "EmailAddress", "FullName", "Password" },
                values: new object[,]
                {
                    { new Guid("59915d5a-e035-4da7-aa76-b767ae1844d6"), "basem@gmail.com", "Basem Attia Elsayed", "Hasehed_Password_2" },
                    { new Guid("bb31e5e5-85a9-4d06-9fac-6b3cb7cd495d"), "karim@gmail.com", "Karim Mohamed Helmy", "Hasehed_Password_234" },
                    { new Guid("e63d04ce-c434-428d-866a-aff44a00916c"), "mohamed@gmail.com", "Mohamed ElSayed Tabei", "Hasehed_Password_235" }
                });

            migrationBuilder.InsertData(
                table: "Bugs",
                columns: new[] { "Id", "Description", "ProjectId", "Status", "Title" },
                values: new object[,]
                {
                    { new Guid("328ebc19-b6f1-4193-ae06-bcbc19105ba4"), "When Using Seeding, Error You Must Use Async Seeding Also", new Guid("1c017e23-6987-4d3b-911f-d3cf432a1c19"), "Solved", "Error In Using Seeding" },
                    { new Guid("a546459d-089c-41b4-9a44-2629babaec06"), "Error In Signing In Using Auth", new Guid("80fa2674-074a-4c48-8b6c-b63eb7ba60f1"), "Open", "Error In login" },
                    { new Guid("ce66d99b-a171-445d-b2a7-30fc399412ff"), "When Using Paymob, An Error Occurred", new Guid("b8e4ce6b-3606-4535-882a-3ceaf55b6808"), "Solved", "Error In Payment" }
                });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("a94a3ec6-77da-47b2-8428-bcbf372a3676"), new Guid("59915d5a-e035-4da7-aa76-b767ae1844d6") },
                    { new Guid("dc435455-5ae0-43ab-919b-3029b64fb488"), new Guid("59915d5a-e035-4da7-aa76-b767ae1844d6") },
                    { new Guid("dc435455-5ae0-43ab-919b-3029b64fb488"), new Guid("e63d04ce-c434-428d-866a-aff44a00916c") },
                    { new Guid("f8b4285f-300e-4cd7-930c-f6f290faea0f"), new Guid("bb31e5e5-85a9-4d06-9fac-6b3cb7cd495d") },
                    { new Guid("f8b4285f-300e-4cd7-930c-f6f290faea0f"), new Guid("e63d04ce-c434-428d-866a-aff44a00916c") }
                });

            migrationBuilder.InsertData(
                table: "BugUser",
                columns: new[] { "BugsId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("328ebc19-b6f1-4193-ae06-bcbc19105ba4"), new Guid("59915d5a-e035-4da7-aa76-b767ae1844d6") },
                    { new Guid("328ebc19-b6f1-4193-ae06-bcbc19105ba4"), new Guid("bb31e5e5-85a9-4d06-9fac-6b3cb7cd495d") },
                    { new Guid("a546459d-089c-41b4-9a44-2629babaec06"), new Guid("bb31e5e5-85a9-4d06-9fac-6b3cb7cd495d") },
                    { new Guid("ce66d99b-a171-445d-b2a7-30fc399412ff"), new Guid("59915d5a-e035-4da7-aa76-b767ae1844d6") },
                    { new Guid("ce66d99b-a171-445d-b2a7-30fc399412ff"), new Guid("e63d04ce-c434-428d-866a-aff44a00916c") }
                });

            migrationBuilder.InsertData(
                table: "FileAttachments",
                columns: new[] { "Id", "BugId", "FileName", "FilePath" },
                values: new object[,]
                {
                    { new Guid("2cdbc40a-b617-4858-98bd-71edd758ee68"), new Guid("328ebc19-b6f1-4193-ae06-bcbc19105ba4"), "C# Code", "/uploads/file.sln" },
                    { new Guid("a289b995-a053-4318-8555-a5a6d9e5f75d"), new Guid("a546459d-089c-41b4-9a44-2629babaec06"), "Error ScreenShot", "/uploads/error_screenshot.png" },
                    { new Guid("e3ba3e0f-d2f5-406a-8fd8-70e9eca99e50"), new Guid("ce66d99b-a171-445d-b2a7-30fc399412ff"), "Error ScreenShot of Paymob", "/uploads/error_screenshot2.png" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bugs_ProjectId",
                table: "Bugs",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BugUser_UsersId",
                table: "BugUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_FileAttachments_BugId",
                table: "FileAttachments",
                column: "BugId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BugUser");

            migrationBuilder.DropTable(
                name: "FileAttachments");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "Bugs");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
