namespace SamplePermissions
{
	using System;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Microsoft.EntityFrameworkCore.ValueGeneration;
	using SamplePermissions.Model;

	public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Invoice> builder)
		{
			builder.ToTable("Invoices");

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).HasValueGenerator<SequentialGuidValueGenerator>();

			// Company invoices
			builder.HasData(new Invoice
			{
				Id = Guid.NewGuid(),
				Total = 199.95m,
				Note = "This is a Company invoice."
			});
			builder.HasData(new Invoice
			{
				Id = Guid.NewGuid(),
				Total = 199.95m,
				Note = "This is a Company invoice."
			});
			builder.HasData(new Invoice
			{
				Id = Guid.NewGuid(),
				Total = 199.95m,
				Note = "This is a Company invoice."
			});
		}
	}
}
