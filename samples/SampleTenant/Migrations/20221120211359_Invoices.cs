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
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", nullable: true),
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
                columns: table => new
                {
                    TenantId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false)
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
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    PermissionId = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false)
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
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false)
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
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[,]
                {
                    { new Guid("4a580392-72b0-4f3c-ad47-567675836d3a"), "This is a Corporate invoice.", "49a049d2-23ad-41df-8806-240aebaa2f17", 399.95m },
                    { new Guid("a38f7a3b-7773-457f-8a33-a4a312518afe"), "This is a Startup invoice.", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", 99.95m },
                    { new Guid("a507c412-e31d-4cc3-a5d4-f0fe1cac0b20"), "This is a Startup invoice.", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", 99.95m },
                    { new Guid("b3ea691b-e869-4c62-b81a-1ebf98bdff1d"), "This is a Company invoice.", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", 199.95m },
                    { new Guid("b727e661-3d6c-4422-aeee-506b767953fa"), "This is a Company invoice.", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", 199.95m },
                    { new Guid("bbc3c3be-d621-4042-bd5c-0c18c95d535a"), "This is a Startup invoice.", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", 99.95m },
                    { new Guid("bffda45a-33aa-4ae6-90fc-ac5c0617b819"), "This is a Company invoice.", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", 199.95m },
                    { new Guid("ea5b5fb0-29fa-4398-9bb7-23234311f2bc"), "This is a Corporate invoice.", "49a049d2-23ad-41df-8806-240aebaa2f17", 399.95m },
                    { new Guid("fd3cfbb2-6525-4db8-bf33-f1d65b14bc79"), "This is a Corporate invoice.", "49a049d2-23ad-41df-8806-240aebaa2f17", 399.95m }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5b9c4926-3dc6-447c-a092-addab890a15f", "72cb863b-e0b3-4ad6-a325-276f5d19962f", "Invoice.Read", "INVOICE.READ" },
                    { "9c8dd197-bc4e-42b2-8789-f0b4481a05ed", "8ef1f4e9-d875-49ca-aae9-53cce0397c02", "Invoice.Statistics", "INVOICE.STATISTICS" },
                    { "9dcb49c9-e732-4fb9-80a1-2c5efda61ab2", "3be1d629-bc14-4611-92f1-ecc55660db5d", "Invoice.Send", "INVOICE.SEND" },
                    { "be5b92e5-c6c6-480b-b235-d4df402a73cc", "0668f069-5f7c-4b0f-ae2a-395ca8aa97e6", "Invoice.Write", "INVOICE.WRITE" },
                    { "e123b8c0-0646-4075-b73e-07ca9d611c8e", "03228ae7-70f9-4761-a331-154eb5a81fbc", "Invoice.Delete", "INVOICE.DELETE" },
                    { "ef54d62d-a36b-4ab3-b868-f170c0054fac", "4fa086a4-8134-4e83-9250-d6a720f19e96", "Invoice.Payment", "INVOICE.PAYMENT" },
                    { "f1af54df-c9e7-4570-850f-c563732c15b4", "8befd0e4-c83c-4be8-816e-0c0d1f292633", "Invoice.TaxExport", "INVOICE.TAXEXPORT" }
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
                columns: new[] { "RoleId", "TenantId", "Discriminator" },
                values: new object[,]
                {
                    { "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230", "49a049d2-23ad-41df-8806-240aebaa2f17", "PermissionsTenantRole" },
                    { "ecae3c35-0d88-424f-a1bc-31cba5add7a7", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", "PermissionsTenantRole" },
                    { "49161cff-c451-4c44-ac59-467883fe1517", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", "PermissionsTenantRole" }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "ConcurrencyStamp", "DatabaseName", "DisplayName", "HasSeparateDatabase", "IsHierarchical", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "49a049d2-23ad-41df-8806-240aebaa2f17", "1347433c-14fb-4b55-beb7-ab8046d3649e", null, "Corporate Corp.", false, false, "Corporate", "CORPORATE" },
                    { "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", "8d906293-edc8-442a-bc06-ea7d8745f967", null, "Startup LLC.", false, false, "Startup", "STARTUP" },
                    { "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", "542cfa31-1878-479b-82f3-cb9aba750840", null, "Company Inc.", false, false, "Company", "COMPANY" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId", "Discriminator" },
                values: new object[,]
                {
                    { "5b9c4926-3dc6-447c-a092-addab890a15f", "2c77ea15-1559-4b9b-bc20-1d64892e4297", "PermissionsRolePermission" },
                    { "e123b8c0-0646-4075-b73e-07ca9d611c8e", "2c77ea15-1559-4b9b-bc20-1d64892e4297", "PermissionsRolePermission" },
                    { "9c8dd197-bc4e-42b2-8789-f0b4481a05ed", "49161cff-c451-4c44-ac59-467883fe1517", "PermissionsRolePermission" },
                    { "5b9c4926-3dc6-447c-a092-addab890a15f", "b0df7eae-a4f9-4d58-8795-ead2aaf6a483", "PermissionsRolePermission" },
                    { "9c8dd197-bc4e-42b2-8789-f0b4481a05ed", "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230", "PermissionsRolePermission" },
                    { "f1af54df-c9e7-4570-850f-c563732c15b4", "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230", "PermissionsRolePermission" },
                    { "5b9c4926-3dc6-447c-a092-addab890a15f", "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", "PermissionsRolePermission" },
                    { "9dcb49c9-e732-4fb9-80a1-2c5efda61ab2", "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", "PermissionsRolePermission" },
                    { "be5b92e5-c6c6-480b-b235-d4df402a73cc", "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", "PermissionsRolePermission" },
                    { "ef54d62d-a36b-4ab3-b868-f170c0054fac", "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", "PermissionsRolePermission" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "04517a45-d6f5-4993-888b-04c924902b3a", 0, "55f7058a-767c-4e9f-936d-0309c85d3733", null, false, false, null, null, "EMPLOYEE@COMPANY", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "bbd3591b-f4ef-4666-aeca-34523b72c257", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", false, "employee@company" },
                    { "142838fe-7e64-484b-a769-87b327726715", 0, "9831f5ca-2d20-4c37-8585-1657107ddfaa", null, false, false, null, null, "EMPLOYEE@STARTUP", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "697c8ba4-1d71-4509-9271-457345b92568", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", false, "employee@startup" },
                    { "50cd8ad5-b945-4541-90c9-156f6940c18b", 0, "cd282b07-f2dc-4d0c-928b-0bea0b60f778", null, false, false, null, null, "MANAGER@STARTUP", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "881b6729-5707-4f0e-9fa7-249ec785b1a5", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", false, "manager@startup" },
                    { "90a4dd66-78d1-4fff-a507-7f88735f7ab6", 0, "a76deeac-a172-4ded-b520-7d9f926be401", null, false, false, null, null, "MANAGER@COMPANY", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "7336921f-5c4a-4344-96c2-8a15c66c44a5", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", false, "manager@company" },
                    { "a0f112af-5e39-4b3f-bc50-015591861ec0", 0, "ec16e24e-9bcf-48ec-aaf8-d20273bbc358", null, false, false, null, null, "BOSS@COMPANY", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "e723d482-2d93-4247-b657-3e147850989d", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", false, "boss@company" },
                    { "aeb83173-9ba7-4aa2-ab82-e434e2dcbe55", 0, "e6f94c04-cda5-41e0-9dd0-fa80ad8e3dbe", null, false, false, null, null, "MANAGER@CORPORATE", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "36ab77cb-0c0c-4c6c-a429-30f5ddd4d10c", "49a049d2-23ad-41df-8806-240aebaa2f17", false, "manager@corporate" },
                    { "dbcf2449-14b7-4766-9829-ae65604500b0", 0, "93a92c9c-18e4-4a98-826e-219b52ac367e", null, false, false, null, null, "BOSS@CORPORATE", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "8b163fa3-9ba6-4155-90bd-8006acf8ead9", "49a049d2-23ad-41df-8806-240aebaa2f17", false, "boss@corporate" },
                    { "e420f504-d953-4bec-95fd-1613fd760652", 0, "f8a94661-d38d-4789-ad72-a276329e3e35", null, false, false, null, null, "EMPLOYEE@CORPORATE", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "d844057a-8b48-41ac-ad8d-1460dbcb1f85", "49a049d2-23ad-41df-8806-240aebaa2f17", false, "employee@corporate" },
                    { "ea346013-ec20-4a69-8a60-8684ffb58a5f", 0, "bb8e8d90-02ee-4830-ab5e-d934b41d11ec", null, false, false, null, null, "BOSS@STARTUP", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "c892601f-3f47-4428-aedf-a6e2e7343686", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", false, "boss@startup" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[,]
                {
                    { "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", "04517a45-d6f5-4993-888b-04c924902b3a", "IdentityUserRole" },
                    { "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", "142838fe-7e64-484b-a769-87b327726715", "IdentityUserRole" },
                    { "2c77ea15-1559-4b9b-bc20-1d64892e4297", "50cd8ad5-b945-4541-90c9-156f6940c18b", "IdentityUserRole" },
                    { "2c77ea15-1559-4b9b-bc20-1d64892e4297", "90a4dd66-78d1-4fff-a507-7f88735f7ab6", "IdentityUserRole" },
                    { "b0df7eae-a4f9-4d58-8795-ead2aaf6a483", "a0f112af-5e39-4b3f-bc50-015591861ec0", "IdentityUserRole" },
                    { "2c77ea15-1559-4b9b-bc20-1d64892e4297", "aeb83173-9ba7-4aa2-ab82-e434e2dcbe55", "IdentityUserRole" },
                    { "b0df7eae-a4f9-4d58-8795-ead2aaf6a483", "dbcf2449-14b7-4766-9829-ae65604500b0", "IdentityUserRole" },
                    { "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", "e420f504-d953-4bec-95fd-1613fd760652", "IdentityUserRole" },
                    { "b0df7eae-a4f9-4d58-8795-ead2aaf6a483", "ea346013-ec20-4a69-8a60-8684ffb58a5f", "IdentityUserRole" }
                });

            migrationBuilder.CreateIndex(
                name: "InvoiceTenantIdIndex",
                table: "Invoices",
                column: "TenantId");

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
