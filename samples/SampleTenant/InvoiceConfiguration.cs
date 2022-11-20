namespace SampleTenant
{
	using System;
	using MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Microsoft.EntityFrameworkCore.ValueGeneration;
	using SampleTenant.Model;

	public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
	{
		private readonly ITenantProvider tenantProvider;

		public InvoiceConfiguration(ITenantProvider tenantProvider)
		{
			this.tenantProvider = tenantProvider;
		}

		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<Invoice> builder)
		{
			builder.ToTable("Invoices");

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).HasValueGenerator<SequentialGuidValueGenerator>();

			// Add a tenant query filter.
			builder.HasIndex(x => x.TenantId).HasDatabaseName("InvoiceTenantIdIndex");
			builder.HasQueryFilter(x => x.TenantId == this.tenantProvider.TenantId);

			// Startup invoices
			builder.HasData(new Invoice
			{
				Id = Guid.NewGuid(),
				Total = 99.95m,
				Note = "This is a Startup invoice.",
				TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2"
			});
			builder.HasData(new Invoice
			{
				Id = Guid.NewGuid(),
				Total = 99.95m,
				Note = "This is a Startup invoice.",
				TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2"
			});
			builder.HasData(new Invoice
			{
				Id = Guid.NewGuid(),
				Total = 99.95m,
				Note = "This is a Startup invoice.",
				TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2"
			});

			// Company invoices
			builder.HasData(new Invoice
			{
				Id = Guid.NewGuid(),
				Total = 199.95m,
				Note = "This is a Company invoice.",
				TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46"
			});
			builder.HasData(new Invoice
			{
				Id = Guid.NewGuid(),
				Total = 199.95m,
				Note = "This is a Company invoice.",
				TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46"
			});
			builder.HasData(new Invoice
			{
				Id = Guid.NewGuid(),
				Total = 199.95m,
				Note = "This is a Company invoice.",
				TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46"
			});

			// Corporate invoices
			builder.HasData(new Invoice
			{
				Id = Guid.NewGuid(),
				Total = 399.95m,
				Note = "This is a Corporate invoice.",
				TenantId = "49a049d2-23ad-41df-8806-240aebaa2f17"
			});
			builder.HasData(new Invoice
			{
				Id = Guid.NewGuid(),
				Total = 399.95m,
				Note = "This is a Corporate invoice.",
				TenantId = "49a049d2-23ad-41df-8806-240aebaa2f17"
			});
			builder.HasData(new Invoice
			{
				Id = Guid.NewGuid(),
				Total = 399.95m,
				Note = "This is a Corporate invoice.",
				TenantId = "49a049d2-23ad-41df-8806-240aebaa2f17"
			});
		}
	}
}
