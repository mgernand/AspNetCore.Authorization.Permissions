using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleTenant.Migrations
{
    public partial class Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identity");

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantRoles",
                schema: "identity",
                columns: table => new
                {
                    TenantId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantRoles", x => new { x.TenantId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    IsHierarchical = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasSeparateDatabase = table.Column<bool>(type: "INTEGER", nullable: false),
                    DatabaseName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "identity",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    PermissionId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "identity",
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "identity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "identity",
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "identity",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Permissions",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5b9c4926-3dc6-447c-a092-addab890a15f", "746a86e1-44d4-4281-8a10-2217ad7ad483", "Invoice.Read", "INVOICE.READ" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Permissions",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9c8dd197-bc4e-42b2-8789-f0b4481a05ed", "593f8685-796e-44b5-84c5-d4541d91578e", "Invoice.Statistics", "INVOICE.STATISTICS" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Permissions",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9dcb49c9-e732-4fb9-80a1-2c5efda61ab2", "71892720-0ff1-4101-abd5-b72c299f2559", "Invoice.Send", "INVOICE.SEND" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Permissions",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "be5b92e5-c6c6-480b-b235-d4df402a73cc", "3f1f0c42-3f7c-4c39-9cdc-236ece4277ed", "Invoice.Write", "INVOICE.WRITE" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Permissions",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e123b8c0-0646-4075-b73e-07ca9d611c8e", "89971c9c-8706-40a0-b39c-27c0e1e22c1e", "Invoice.Delete", "INVOICE.DELETE" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Permissions",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ef54d62d-a36b-4ab3-b868-f170c0054fac", "f488a008-2356-440b-ba9b-6f2e62e82727", "Invoice.Payment", "INVOICE.PAYMENT" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Permissions",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f1af54df-c9e7-4570-850f-c563732c15b4", "80ab66dc-d2f0-4dae-9702-048f3377f758", "Invoice.TaxExport", "INVOICE.TAXEXPORT" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2c77ea15-1559-4b9b-bc20-1d64892e4297", "fcf51faf-7779-4ce9-94f9-9301b9942122", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "49161cff-c451-4c44-ac59-467883fe1517", "6daabc8f-38bc-4d3e-b8d4-1420876f4062", "Basic", "BASIC" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b0df7eae-a4f9-4d58-8795-ead2aaf6a483", "c36d8a16-e2fd-4959-88ff-e5e1ddab902f", "Boss", "BOSS" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230", "417b8994-d399-45d5-8ecf-5efc674e4983", "Professional", "PROFESSIONAL" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", "1c4f2123-ed65-47d5-8e9b-4b5ab8baf1d5", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ecae3c35-0d88-424f-a1bc-31cba5add7a7", "f1a70954-9655-46b4-96c4-f8915430a3d7", "Free", "FREE" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "TenantRoles",
                columns: new[] { "RoleId", "TenantId" },
                values: new object[] { "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230", "49a049d2-23ad-41df-8806-240aebaa2f17" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "TenantRoles",
                columns: new[] { "RoleId", "TenantId" },
                values: new object[] { "ecae3c35-0d88-424f-a1bc-31cba5add7a7", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "TenantRoles",
                columns: new[] { "RoleId", "TenantId" },
                values: new object[] { "49161cff-c451-4c44-ac59-467883fe1517", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Tenants",
                columns: new[] { "Id", "ConcurrencyStamp", "DatabaseName", "DisplayName", "HasSeparateDatabase", "IsHierarchical", "Name", "NormalizedName" },
                values: new object[] { "49a049d2-23ad-41df-8806-240aebaa2f17", "3f01152e-9561-4997-8a1e-fe8001fdef2e", null, "Corporate Corp.", false, false, "Corporate", "CORPORATE" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Tenants",
                columns: new[] { "Id", "ConcurrencyStamp", "DatabaseName", "DisplayName", "HasSeparateDatabase", "IsHierarchical", "Name", "NormalizedName" },
                values: new object[] { "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", "5cf8ff61-95d4-412f-931e-34cedefc75f8", null, "Startup LLC.", false, false, "Startup", "STARTUP" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Tenants",
                columns: new[] { "Id", "ConcurrencyStamp", "DatabaseName", "DisplayName", "HasSeparateDatabase", "IsHierarchical", "Name", "NormalizedName" },
                values: new object[] { "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", "c4fcca6a-3af5-4bdf-a47e-052bbca47019", null, "Company Inc.", false, false, "Company", "COMPANY" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { "5b9c4926-3dc6-447c-a092-addab890a15f", "2c77ea15-1559-4b9b-bc20-1d64892e4297" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { "e123b8c0-0646-4075-b73e-07ca9d611c8e", "2c77ea15-1559-4b9b-bc20-1d64892e4297" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { "9c8dd197-bc4e-42b2-8789-f0b4481a05ed", "49161cff-c451-4c44-ac59-467883fe1517" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { "5b9c4926-3dc6-447c-a092-addab890a15f", "b0df7eae-a4f9-4d58-8795-ead2aaf6a483" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { "9c8dd197-bc4e-42b2-8789-f0b4481a05ed", "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { "f1af54df-c9e7-4570-850f-c563732c15b4", "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { "5b9c4926-3dc6-447c-a092-addab890a15f", "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { "9dcb49c9-e732-4fb9-80a1-2c5efda61ab2", "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { "be5b92e5-c6c6-480b-b235-d4df402a73cc", "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { "ef54d62d-a36b-4ab3-b868-f170c0054fac", "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "04517a45-d6f5-4993-888b-04c924902b3a", 0, "b40cd5bd-229f-46b9-8c22-778d183012fa", null, false, false, null, null, "EMPLOYEE@COMPANY", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "83439d64-555f-478b-8709-4a46adc74ca0", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", false, "employee@company" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "142838fe-7e64-484b-a769-87b327726715", 0, "bece63a6-bb53-4bdf-b12f-e9347125d612", null, false, false, null, null, "EMPLOYEE@STARTUP", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "caa7551e-ab64-4884-9e34-ba6e7ce08384", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", false, "employee@startup" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "50cd8ad5-b945-4541-90c9-156f6940c18b", 0, "58ebc778-a3eb-4921-b316-a8e5c971d014", null, false, false, null, null, "MANAGER@STARTUP", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "2b013caf-82c6-4334-8195-f6bfb517a3a0", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", false, "manager@startup" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "90a4dd66-78d1-4fff-a507-7f88735f7ab6", 0, "8331840f-fb13-4833-b89d-0d86ddf7b7fc", null, false, false, null, null, "MANAGER@COMPANY", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "8fbfde9d-4b98-4552-a401-78fe5c964bd8", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", false, "manager@company" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a0f112af-5e39-4b3f-bc50-015591861ec0", 0, "2b8e831d-1dab-4b67-a948-ff78764f8418", null, false, false, null, null, "BOSS@COMPANY", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "cfdf4f7d-6122-4543-9cb4-934b231b93a8", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", false, "boss@company" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "aeb83173-9ba7-4aa2-ab82-e434e2dcbe55", 0, "4b5f9ae5-12ca-4302-ba4b-03f8c2e4b140", null, false, false, null, null, "MANAGER@CORPORATE", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "a7b1495d-eec4-4539-82e7-88a9474ed79a", "49a049d2-23ad-41df-8806-240aebaa2f17", false, "manager@corporate" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dbcf2449-14b7-4766-9829-ae65604500b0", 0, "1f785651-b9d3-44f6-8b84-cec413486f4f", null, false, false, null, null, "BOSS@CORPORATE", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "373a388f-30f4-4e46-9aaf-503fdcd2e33b", "49a049d2-23ad-41df-8806-240aebaa2f17", false, "boss@corporate" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e420f504-d953-4bec-95fd-1613fd760652", 0, "9dc9552b-beb4-4c21-ae42-12480d978fc0", null, false, false, null, null, "EMPLOYEE@CORPORATE", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "870dd32a-959f-4527-b818-c49e78b13730", "49a049d2-23ad-41df-8806-240aebaa2f17", false, "employee@corporate" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ea346013-ec20-4a69-8a60-8684ffb58a5f", 0, "4231bcd4-3c33-4a4d-a4b7-523a020b9cb2", null, false, false, null, null, "BOSS@STARTUP", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "aeaf42a5-aace-4414-bf20-37e53026b7b9", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", false, "boss@startup" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", "04517a45-d6f5-4993-888b-04c924902b3a" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", "142838fe-7e64-484b-a769-87b327726715" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2c77ea15-1559-4b9b-bc20-1d64892e4297", "50cd8ad5-b945-4541-90c9-156f6940c18b" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2c77ea15-1559-4b9b-bc20-1d64892e4297", "90a4dd66-78d1-4fff-a507-7f88735f7ab6" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b0df7eae-a4f9-4d58-8795-ead2aaf6a483", "a0f112af-5e39-4b3f-bc50-015591861ec0" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "2c77ea15-1559-4b9b-bc20-1d64892e4297", "aeb83173-9ba7-4aa2-ab82-e434e2dcbe55" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b0df7eae-a4f9-4d58-8795-ead2aaf6a483", "dbcf2449-14b7-4766-9829-ae65604500b0" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", "e420f504-d953-4bec-95fd-1613fd760652" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b0df7eae-a4f9-4d58-8795-ead2aaf6a483", "ea346013-ec20-4a69-8a60-8684ffb58a5f" });

            migrationBuilder.CreateIndex(
                name: "PermissionNameIndex",
                schema: "identity",
                table: "Permissions",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "identity",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                schema: "identity",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "identity",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "TenantNameIndex",
                schema: "identity",
                table: "Tenants",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "identity",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "identity",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "identity",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "identity",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantId",
                schema: "identity",
                table: "Users",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "identity",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "TenantRoles",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "identity");
        }
    }
}
