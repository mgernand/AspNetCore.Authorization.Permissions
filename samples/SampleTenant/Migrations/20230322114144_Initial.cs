using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SampleTenant.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true),
                    TenantID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantRoles",
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
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
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
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
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
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
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
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
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
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
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
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantID", "Total" },
                values: new object[,]
                {
                    { new Guid("1190e6e7-75d4-4336-a168-e42b063e7485"), "This is a Company invoice.", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", 199.95m },
                    { new Guid("61a3c165-df02-445f-b4bf-d3a6d6f581d2"), "This is a Startup invoice.", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", 99.95m },
                    { new Guid("6e6cdffa-55ef-46cc-8020-c678feef6499"), "This is a Corporate invoice.", "49a049d2-23ad-41df-8806-240aebaa2f17", 399.95m },
                    { new Guid("753ffd62-a368-439d-9aa2-19f47fcb9e28"), "This is a Corporate invoice.", "49a049d2-23ad-41df-8806-240aebaa2f17", 399.95m },
                    { new Guid("8ceef93c-1bf8-4bd0-9556-7f6b0886f7db"), "This is a Startup invoice.", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", 99.95m },
                    { new Guid("a05d5bd8-c6a8-4286-a771-4311474742b8"), "This is a Corporate invoice.", "49a049d2-23ad-41df-8806-240aebaa2f17", 399.95m },
                    { new Guid("ab41432a-b190-4ee8-8923-747950ca534f"), "This is a Startup invoice.", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", 99.95m },
                    { new Guid("d686039e-a9de-4ec0-b6a1-40ded147636d"), "This is a Company invoice.", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", 199.95m },
                    { new Guid("f9598106-a510-4d31-957c-005b6e2c1892"), "This is a Company invoice.", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", 199.95m }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5b9c4926-3dc6-447c-a092-addab890a15f", "ed978ab6-ac7a-47ce-a670-fb776156ff5c", "Invoice.Read", "INVOICE.READ" },
                    { "9c8dd197-bc4e-42b2-8789-f0b4481a05ed", "0d92f40d-b529-4629-8a39-5abfcf55b1cb", "Invoice.Statistics", "INVOICE.STATISTICS" },
                    { "9dcb49c9-e732-4fb9-80a1-2c5efda61ab2", "7e3fdc41-1def-42d8-b698-22d8f67ca92e", "Invoice.Send", "INVOICE.SEND" },
                    { "be5b92e5-c6c6-480b-b235-d4df402a73cc", "90326547-5bcd-4ffd-9666-be3e12e63621", "Invoice.Write", "INVOICE.WRITE" },
                    { "e123b8c0-0646-4075-b73e-07ca9d611c8e", "03330399-aabb-4fba-b4d3-8252dc84311d", "Invoice.Delete", "INVOICE.DELETE" },
                    { "ef54d62d-a36b-4ab3-b868-f170c0054fac", "b189ffd2-202a-41d6-917e-69d5d46439af", "Invoice.Payment", "INVOICE.PAYMENT" },
                    { "f1af54df-c9e7-4570-850f-c563732c15b4", "5f2c2bbe-e1c8-4b9d-b5e4-06b2e34c0a66", "Invoice.TaxExport", "INVOICE.TAXEXPORT" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
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
                table: "TenantRoles",
                columns: new[] { "RoleId", "TenantId" },
                values: new object[,]
                {
                    { "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230", "49a049d2-23ad-41df-8806-240aebaa2f17" },
                    { "ecae3c35-0d88-424f-a1bc-31cba5add7a7", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2" },
                    { "49161cff-c451-4c44-ac59-467883fe1517", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46" }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "ConcurrencyStamp", "DatabaseName", "DisplayName", "HasSeparateDatabase", "IsHierarchical", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "49a049d2-23ad-41df-8806-240aebaa2f17", "6ae7c9e8-a348-4602-9b1a-6f760dc5a0d1", null, "Corporate Corp.", false, false, "Corporate", "CORPORATE" },
                    { "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", "99429101-7bd8-4b6b-83b0-295d9a8e068e", null, "Startup LLC.", false, false, "Startup", "STARTUP" },
                    { "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", "dc492669-04d9-466b-9b8c-8d69607ee2e3", null, "Company Inc.", false, false, "Company", "COMPANY" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
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
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "04517a45-d6f5-4993-888b-04c924902b3a", 0, "3cdf0e34-abef-43b2-a7ed-6cd6da89f1ab", null, false, false, null, null, "EMPLOYEE@COMPANY", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "64891915-5d85-4f74-8e4a-c7e7f072a0d8", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", false, "employee@company" },
                    { "142838fe-7e64-484b-a769-87b327726715", 0, "d113f27d-e5f2-4d25-9133-a3043e0cbc86", null, false, false, null, null, "EMPLOYEE@STARTUP", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "1c3c1fb7-65e6-4538-845b-41553afb5b87", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", false, "employee@startup" },
                    { "50cd8ad5-b945-4541-90c9-156f6940c18b", 0, "aabcac7e-9951-479e-806e-cf6329c4ef55", null, false, false, null, null, "MANAGER@STARTUP", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "cbc6ce1b-1e42-4b2a-b3ca-692c2b021c24", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", false, "manager@startup" },
                    { "90a4dd66-78d1-4fff-a507-7f88735f7ab6", 0, "f6e97f88-e3cc-4a86-bacb-87d1b9015453", null, false, false, null, null, "MANAGER@COMPANY", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "198439ee-4290-48ae-b7f6-48354abd690b", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", false, "manager@company" },
                    { "a0f112af-5e39-4b3f-bc50-015591861ec0", 0, "1990a8c7-6ad8-44bc-b683-1ba4ba6bf4e5", null, false, false, null, null, "BOSS@COMPANY", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "37728c18-84be-4667-b5bd-7c3577e61513", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", false, "boss@company" },
                    { "aeb83173-9ba7-4aa2-ab82-e434e2dcbe55", 0, "585336bf-dee2-4b2a-af1e-fe7387472653", null, false, false, null, null, "MANAGER@CORPORATE", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "4e6fff6a-e2ae-4d59-a6c4-fb30e295ead6", "49a049d2-23ad-41df-8806-240aebaa2f17", false, "manager@corporate" },
                    { "dbcf2449-14b7-4766-9829-ae65604500b0", 0, "bb0d464b-ec5b-4623-ae81-737fc7577723", null, false, false, null, null, "BOSS@CORPORATE", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "8ae11f84-0b7f-421b-9cbc-000eda90b58f", "49a049d2-23ad-41df-8806-240aebaa2f17", false, "boss@corporate" },
                    { "e420f504-d953-4bec-95fd-1613fd760652", 0, "2a3d32c4-037a-4a34-9e5a-599451c515a2", null, false, false, null, null, "EMPLOYEE@CORPORATE", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "394c97ba-d9ab-4c5a-bdf2-2af55b9be190", "49a049d2-23ad-41df-8806-240aebaa2f17", false, "employee@corporate" },
                    { "ea346013-ec20-4a69-8a60-8684ffb58a5f", 0, "b8ba7187-db51-434d-b1c2-2571f5002884", null, false, false, null, null, "BOSS@STARTUP", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "fd5d017f-8085-4b0a-bd07-203f75219711", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", false, "boss@startup" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
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
                name: "InvoiceTenantIdIndex",
                table: "Invoices",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "PermissionNameIndex",
                table: "Permissions",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "TenantNameIndex",
                table: "Tenants",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TenantId",
                table: "Users",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "TenantRoles");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
