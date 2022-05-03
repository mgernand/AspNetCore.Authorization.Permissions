using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamplePermissions.Migrations
{
    public partial class Identity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identity");

            migrationBuilder.CreateTable(
                name: "AspNetTenants",
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
                    table.PrimaryKey("PK_AspNetTenants", x => x.Id);
                });

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
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", nullable: true),
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
                        name: "FK_Users_AspNetTenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "identity",
                        principalTable: "AspNetTenants",
                        principalColumn: "Id");
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
                values: new object[] { "5b9c4926-3dc6-447c-a092-addab890a15f", "bbc91bd8-677b-43e4-a4b9-b9c8042800d2", "Invoice.Read", "INVOICE.READ" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Permissions",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9dcb49c9-e732-4fb9-80a1-2c5efda61ab2", "26a64c80-f452-411d-9eda-870aa10beee8", "Invoice.Send", "INVOICE.SEND" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Permissions",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "be5b92e5-c6c6-480b-b235-d4df402a73cc", "5156f402-22a8-4223-937f-8103fc673da9", "Invoice.Write", "INVOICE.WRITE" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Permissions",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e123b8c0-0646-4075-b73e-07ca9d611c8e", "26142354-be5d-4f5d-adfc-ddd582295ff6", "Invoice.Delete", "INVOICE.DELETE" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Permissions",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ef54d62d-a36b-4ab3-b868-f170c0054fac", "9a48a37d-4973-49ef-a4dd-3c54a7988cd3", "Invoice.Payment", "INVOICE.PAYMENT" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2c77ea15-1559-4b9b-bc20-1d64892e4297", "f51c6d1c-e043-4493-8b3a-1b67340795e8", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b0df7eae-a4f9-4d58-8795-ead2aaf6a483", "68094f47-6d38-4428-bc98-2225144bfb74", "Boss", "BOSS" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", "b19c5abe-c633-42b0-870e-d4f5f026a3e9", "Employee", "EMPLOYEE" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "04517a45-d6f5-4993-888b-04c924902b3a", 0, "a4fbef50-ab25-4829-b97a-365a10705d86", null, false, false, null, null, "EMPLOYEE@COMPANY", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "04017357-dd57-48b4-9d0f-b2f0b36ea874", null, false, "employee@company" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "90a4dd66-78d1-4fff-a507-7f88735f7ab6", 0, "e23a4ce4-85d2-4db1-82c3-3624b0ea2534", null, false, false, null, null, "MANAGER@COMPANY", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "bcf4d471-98c7-4181-a018-b8ddb9a5332d", null, false, "manager@company" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a0f112af-5e39-4b3f-bc50-015591861ec0", 0, "96e8bbbd-62f9-490e-810a-6d722d7bda02", null, false, false, null, null, "BOSS@COMPANY", "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==", null, false, "bf21e5cf-1f5a-40b9-86e0-534dff3af2b2", null, false, "boss@company" });

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
                values: new object[] { "5b9c4926-3dc6-447c-a092-addab890a15f", "b0df7eae-a4f9-4d58-8795-ead2aaf6a483" });

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
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2", "04517a45-d6f5-4993-888b-04c924902b3a" });

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

            migrationBuilder.CreateIndex(
                name: "TenantNameIndex",
                schema: "identity",
                table: "AspNetTenants",
                column: "NormalizedName",
                unique: true);

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
                name: "Tenants",
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
                name: "AspNetTenants",
                schema: "identity");
        }
    }
}
