#nullable disable

namespace SamplePermissions.Migrations.InvoicesDb
{
	using System;
	using Microsoft.EntityFrameworkCore.Migrations;

	public partial class Invoices : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.EnsureSchema(
				"invoices");

			migrationBuilder.CreateTable(
				"Invoices",
				schema: "invoices",
				columns: table => new
				{
					Id = table.Column<Guid>("TEXT", nullable: false),
					Total = table.Column<decimal>("TEXT", nullable: false),
					Note = table.Column<string>("TEXT", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Invoices", x => x.Id);
				});

			migrationBuilder.InsertData(
				schema: "invoices",
				table: "Invoices",
				columns: new[] { "Id", "Note", "Total" },
				values: new object[] { new Guid("14776ea1-ae17-4b4a-9a34-03158e3e8cf5"), "This is a Company invoice.", 199.95m });

			migrationBuilder.InsertData(
				schema: "invoices",
				table: "Invoices",
				columns: new[] { "Id", "Note", "Total" },
				values: new object[] { new Guid("76496189-737f-4b96-93df-535d445a8cf2"), "This is a Company invoice.", 199.95m });

			migrationBuilder.InsertData(
				schema: "invoices",
				table: "Invoices",
				columns: new[] { "Id", "Note", "Total" },
				values: new object[] { new Guid("cc3b072e-af21-4104-8c52-1d144ec08f07"), "This is a Company invoice.", 199.95m });
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				"Invoices",
				"invoices");
		}
	}
}
