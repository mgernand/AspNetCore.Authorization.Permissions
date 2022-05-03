using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamplePermissions.Migrations.InvoicesDb
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
                    Note = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "Total" },
                values: new object[] { new Guid("39fa77fc-9d61-4aa7-8b03-ee35c6e9a272"), "This is a Company invoice.", 199.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "Total" },
                values: new object[] { new Guid("82af347d-b5b1-412c-9064-0e63a76ffc90"), "This is a Company invoice.", 199.95m });

            migrationBuilder.InsertData(
                schema: "invoices",
                table: "Invoices",
                columns: new[] { "Id", "Note", "Total" },
                values: new object[] { new Guid("cafa090b-f6f7-46aa-a654-48b6115c8aeb"), "This is a Company invoice.", 199.95m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices",
                schema: "invoices");
        }
    }
}
