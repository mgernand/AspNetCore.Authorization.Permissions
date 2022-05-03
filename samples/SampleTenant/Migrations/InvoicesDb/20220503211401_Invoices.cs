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
                values: new object[] { new Guid("2bb87062-9c45-4b22-8d3b-3ae4b4044f60"), "This is a Company invoice.", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", 199.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("36701e5d-1d44-4d2e-b2a1-feeec8b4a604"), "This is a Corporate invoice.", "49a049d2-23ad-41df-8806-240aebaa2f17", 399.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("495a1d75-43cb-48c0-bcae-dd0894477a9b"), "This is a Company invoice.", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", 199.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("4c86fb7a-4467-4034-988e-3d6835345823"), "This is a Startup invoice.", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", 99.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("6aa993da-fe2e-4b79-8eff-8cf9a111d6ce"), "This is a Corporate invoice.", "49a049d2-23ad-41df-8806-240aebaa2f17", 399.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("7eae891b-5d17-4e16-a918-742043d8ac7b"), "This is a Startup invoice.", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", 99.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("9e2260df-525d-441c-94bb-63acf789700d"), "This is a Company invoice.", "ee5128d3-4cad-4bcc-aa64-f6abbb30da46", 199.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("b109560a-ccbd-42b8-b14d-3fec2b50be99"), "This is a Corporate invoice.", "49a049d2-23ad-41df-8806-240aebaa2f17", 399.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "TenantId", "Total" },
                values: new object[] { new Guid("f38fd74b-5c0e-4747-aaf8-c39833a407da"), "This is a Startup invoice.", "7d706acd-f5fd-4979-9e3f-c77a0bd596b2", 99.95m });

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
