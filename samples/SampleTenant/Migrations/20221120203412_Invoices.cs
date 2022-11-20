using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SampleTenant.Migrations
{
    /// <inheritdoc />
    public partial class Invoices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetPermissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetTenantRoles",
                columns: table => new
                {
                    TenantId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetTenantRoles", x => new { x.TenantId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AspNetTenants",
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
                    table.PrimaryKey("PK_AspNetTenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    TenantId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRolePermissions",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    PermissionId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_AspNetRolePermissions_AspNetPermissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "AspNetPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
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
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AspNetTenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
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
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetPermissions",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5b9c4926-3dc6-447c-a092-addab890a15f", "626b75c2-76e1-4f9d-88ed-1d1b7d710e6e", "Invoice.Read", "INVOICE.READ" },
                    { "9c8dd197-bc4e-42b2-8789-f0b4481a05ed", "49d91e1d-6ac3-4827-af2a-8391f3084d87", "Invoice.Statistics", "INVOICE.STATISTICS" },
                    { "9dcb49c9-e732-4fb9-80a1-2c5efda61ab2", "b3bd5abc-7bd6-4453-9132-41d6cfb0b28b", "Invoice.Send", "INVOICE.SEND" },
                    { "be5b92e5-c6c6-480b-b235-d4df402a73cc", "6bcf879b-6ef3-4177-9457-6aefeefdc6d2", "Invoice.Write", "INVOICE.WRITE" },
                    { "e123b8c0-0646-4075-b73e-07ca9d611c8e", "55d78950-5227-4efb-bd1e-39766047c7c0", "Invoice.Delete", "INVOICE.DELETE" },
                    { "ef54d62d-a36b-4ab3-b868-f170c0054fac", "1f192eea-1a4e-4acc-8162-5ed065816b2d", "Invoice.Payment", "INVOICE.PAYMENT" },
                    { "f1af54df-c9e7-4570-850f-c563732c15b4", "eeec973d-f8a4-4fcd-a681-09b51d0e72bf", "Invoice.TaxExport", "INVOICE.TAXEXPORT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2c77ea15-1559-4b9b-bc20-1d64892e4297", null, "Manager", "MANAGER" },
                    { "49161cff-c451-4c44-ac59-467883fe1517", null, "Basic", "BASIC" },
                    { "b0df7eae-a4f9-4d58-8795-ead2aaf6a483", null, "Boss", "BOSS" },
                    { "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230", null, "Professional", "PROFESSIONAL" },
                    { "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", null, "Employee", "EMPLOYEE" },
                    { "ecae3c35-0d88-424f-a1bc-31cba5add7a7", null, "Free", "FREE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetTenantRoles",
                columns: new[] { "RoleId", "TenantId" },
                values: new object[,]
                {
                    { "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230", "49a049d2-23ad-41df-8806-240aebaa2f17" },
                    { "ecae3c35-0d88-424f-a1bc-31cba5add7a7", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2" },
                    { "49161cff-c451-4c44-ac59-467883fe1517", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46" }
                });

            migrationBuilder.InsertData(
                table: "AspNetTenants",
                columns: new[] { "Id", "ConcurrencyStamp", "DatabaseName", "DisplayName", "HasSeparateDatabase", "IsHierarchical", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "49a049d2-23ad-41df-8806-240aebaa2f17", "9b74f601-69f3-436c-9315-b06893ce4f89", null, "Corporate Corp.", false, false, "Corporate", "CORPORATE" },
                    { "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", "4f8b9e22-0692-4837-af50-1f2b1671ff7f", null, "Startup LLC.", false, false, "Startup", "STARTUP" },
                    { "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", "4e10ba64-7208-4418-9932-6a1d310ee3fa", null, "Company Inc.", false, false, "Company", "COMPANY" }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[,]
                {
                    { new Guid("1fb592c3-8484-449f-9cc6-c2a6bc936cfd"), "This is a Company invoice.", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", 199.95m },
                    { new Guid("34ac948d-f6ad-4e5d-9cbd-3e6773587864"), "This is a Corporate invoice.", "49a049d2-23ad-41df-8806-240aebaa2f17", 399.95m },
                    { new Guid("5acb0e24-60be-4120-9c70-4a5ac9f0a5fb"), "This is a Corporate invoice.", "49a049d2-23ad-41df-8806-240aebaa2f17", 399.95m },
                    { new Guid("5d9233ea-f8bc-4b7e-b4b2-189dd7b86a8e"), "This is a Corporate invoice.", "49a049d2-23ad-41df-8806-240aebaa2f17", 399.95m },
                    { new Guid("7fa7d8b3-67ff-41fa-8425-5f90444d1d3c"), "This is a Startup invoice.", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", 99.95m },
                    { new Guid("cae12e98-f7e6-403e-a71c-533097b254f6"), "This is a Company invoice.", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", 199.95m },
                    { new Guid("d5130ae2-a8c5-48f8-9582-8059dc4bc4a3"), "This is a Startup invoice.", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", 99.95m },
                    { new Guid("ede11066-166f-4fc4-b845-e30358220aba"), "This is a Startup invoice.", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", 99.95m },
                    { new Guid("fde2cb02-7b01-40f1-addf-fca24d04f116"), "This is a Company invoice.", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", 199.95m }
                });

            migrationBuilder.InsertData(
                table: "AspNetRolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { "5b9c4926-3dc6-447c-a092-addab890a15f", "2c77ea15-1559-4b9b-bc20-1d64892e4297" },
                    { "e123b8c0-0646-4075-b73e-07ca9d611c8e", "2c77ea15-1559-4b9b-bc20-1d64892e4297" },
                    { "9c8dd197-bc4e-42b2-8789-f0b4481a05ed", "49161cff-c451-4c44-ac59-467883fe1517" },
                    { "5b9c4926-3dc6-447c-a092-addab890a15f", "b0df7eae-a4f9-4d58-8795-ead2aaf6a483" },
                    { "9c8dd197-bc4e-42b2-8789-f0b4481a05ed", "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230" },
                    { "f1af54df-c9e7-4570-850f-c563732c15b4", "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230" },
                    { "5b9c4926-3dc6-447c-a092-addab890a15f", "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2" },
                    { "9dcb49c9-e732-4fb9-80a1-2c5efda61ab2", "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2" },
                    { "be5b92e5-c6c6-480b-b235-d4df402a73cc", "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2" },
                    { "ef54d62d-a36b-4ab3-b868-f170c0054fac", "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "04517a45-d6f5-4993-888b-04c924902b3a", 0, "464c9b49-15fe-49dc-8bc7-f3737c855dc0", null, false, false, null, null, "EMPLOYEE@COMPANY", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "036c650c-05ba-44d4-944e-795479196385", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", false, "employee@company" },
                    { "142838fe-7e64-484b-a769-87b327726715", 0, "f147c43f-a446-4f0e-b669-b7c50a5caa2b", null, false, false, null, null, "EMPLOYEE@STARTUP", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "ee04b58a-25e8-454b-8cbb-d7f838ce9751", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", false, "employee@startup" },
                    { "50cd8ad5-b945-4541-90c9-156f6940c18b", 0, "f6f5e567-b3e2-45c7-9748-309717787860", null, false, false, null, null, "MANAGER@STARTUP", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "facd5e02-baa3-497e-92bd-8532ecc3ab0f", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", false, "manager@startup" },
                    { "90a4dd66-78d1-4fff-a507-7f88735f7ab6", 0, "0e1248f4-df77-4760-810e-6bb0dbe42e5b", null, false, false, null, null, "MANAGER@COMPANY", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "365e9599-ef4f-4925-8043-4ca16447a5e3", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", false, "manager@company" },
                    { "a0f112af-5e39-4b3f-bc50-015591861ec0", 0, "39c52ead-d667-4b23-875f-684ce2224db9", null, false, false, null, null, "BOSS@COMPANY", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "6edc13e4-a5cc-445b-850d-7e41313b7c6b", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", false, "boss@company" },
                    { "aeb83173-9ba7-4aa2-ab82-e434e2dcbe55", 0, "8c332a32-f9de-4a85-bb2e-fddbca718033", null, false, false, null, null, "MANAGER@CORPORATE", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "9d4b7359-6dfd-47dd-88d2-3b21bde6799b", "49a049d2-23ad-41df-8806-240aebaa2f17", false, "manager@corporate" },
                    { "dbcf2449-14b7-4766-9829-ae65604500b0", 0, "c132ebc9-8f88-46b6-afaf-1c10e7483f10", null, false, false, null, null, "BOSS@CORPORATE", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "6edf2a92-ffc9-4313-bb18-02a12801cc34", "49a049d2-23ad-41df-8806-240aebaa2f17", false, "boss@corporate" },
                    { "e420f504-d953-4bec-95fd-1613fd760652", 0, "e82dfd0a-4257-4afc-b5ce-f16fd41c97cc", null, false, false, null, null, "EMPLOYEE@CORPORATE", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "8a5b9b1e-9d0b-4e4c-a43c-c0d30ddf218b", "49a049d2-23ad-41df-8806-240aebaa2f17", false, "employee@corporate" },
                    { "ea346013-ec20-4a69-8a60-8684ffb58a5f", 0, "97aded30-44ac-49d1-8a7f-99d5a097be73", null, false, false, null, null, "BOSS@STARTUP", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "810f738d-89b3-4ad3-8e13-65ed7ba6b91f", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", false, "boss@startup" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", "04517a45-d6f5-4993-888b-04c924902b3a" },
                    { "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", "142838fe-7e64-484b-a769-87b327726715" },
                    { "2c77ea15-1559-4b9b-bc20-1d64892e4297", "50cd8ad5-b945-4541-90c9-156f6940c18b" },
                    { "2c77ea15-1559-4b9b-bc20-1d64892e4297", "90a4dd66-78d1-4fff-a507-7f88735f7ab6" },
                    { "b0df7eae-a4f9-4d58-8795-ead2aaf6a483", "a0f112af-5e39-4b3f-bc50-015591861ec0" },
                    { "2c77ea15-1559-4b9b-bc20-1d64892e4297", "aeb83173-9ba7-4aa2-ab82-e434e2dcbe55" },
                    { "b0df7eae-a4f9-4d58-8795-ead2aaf6a483", "dbcf2449-14b7-4766-9829-ae65604500b0" },
                    { "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", "e420f504-d953-4bec-95fd-1613fd760652" },
                    { "b0df7eae-a4f9-4d58-8795-ead2aaf6a483", "ea346013-ec20-4a69-8a60-8684ffb58a5f" }
                });

            migrationBuilder.CreateIndex(
                name: "PermissionNameIndex",
                table: "AspNetPermissions",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRolePermissions_PermissionId",
                table: "AspNetRolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "TenantNameIndex",
                table: "AspNetTenants",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TenantId",
                table: "AspNetUsers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "InvoiceTenantIdIndex",
                table: "Invoices",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetRolePermissions");

            migrationBuilder.DropTable(
                name: "AspNetTenantRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "AspNetPermissions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AspNetTenants");
        }
    }
}
