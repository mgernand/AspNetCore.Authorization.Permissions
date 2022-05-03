using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleTenant.Migrations.InvoicesDb
{
    public partial class Invoices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "invoices");

            migrationBuilder.CreateTable(
                name: "Invoices",
                schema: "invoices",
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

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("0f7eecb1-53d2-41f3-bc4d-4fae75433f62"), "This is a Startup invoice.", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", 99.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("132d142c-b56f-4d2c-954f-3a91491f7e9e"), "This is a Startup invoice.", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", 99.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("4ecda8f8-a8a0-4b86-bac7-4d83461f7a13"), "This is a Company invoice.", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", 199.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("5aff46c9-cb63-4ece-a35e-643c8d0babff"), "This is a Corporate invoice.", "49a049d2-23ad-41df-8806-240aebaa2f17", 399.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("7fc4111a-ea66-4829-b145-f14e99879d38"), "This is a Corporate invoice.", "49a049d2-23ad-41df-8806-240aebaa2f17", 399.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("a6b7b5a0-041e-4fd2-89c6-0105a627083a"), "This is a Corporate invoice.", "49a049d2-23ad-41df-8806-240aebaa2f17", 399.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("c40673e6-b69e-40c0-a16d-8b01f04e747b"), "This is a Company invoice.", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", 199.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("c4279ed7-9272-4248-a6ce-8196634e71ca"), "This is a Company invoice.", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", 199.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("e91cad3d-2a48-4e64-aa0a-3929126622a1"), "This is a Startup invoice.", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", 99.95m });

            migrationBuilder.CreateIndex(
                name: "InvoiceTenantIdIndex",
                schema: "invoices",
                table: "Invoices",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices",
                schema: "invoices");
        }
    }
}
