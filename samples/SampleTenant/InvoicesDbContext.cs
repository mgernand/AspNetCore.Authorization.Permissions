namespace SampleTenant
{
	using AspNetCore.Authorization.Permissions.Abstractions;
	using AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.ValueGeneration;
	using SampleTenant.Model;

	public class InvoicesDbContext : TenantDbContext
	{
		/// <summary>
		///     Initializes a new instance of <see cref="InvoicesDbContext" />.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		/// <param name="tenantProvider"></param>
		public InvoicesDbContext(DbContextOptions<InvoicesDbContext> options, ITenantProvider tenantProvider)
			: base(options, tenantProvider)
		{
		}

		/// <summary>
		///     Gets or sets the <see cref="DbSet{TEntity}" /> of invoices.
		/// </summary>
		public virtual DbSet<Invoice> Invoices { get; set; }

		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.HasDefaultSchema("invoices");

			builder.Entity<Invoice>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.Property(x => x.Id).HasValueGenerator<SequentialGuidValueGenerator>();

				// Add a tenant query filter.
				entity.HasIndex(x => x.TenantId).HasDatabaseName("InvoiceTenantIdIndex");
				entity.HasQueryFilter(x => x.TenantId == this.TenantProvider.TenantId);

				// Startup invoices
				entity.HasData(new Invoice
				{
					Id = Guid.NewGuid(),
					Total = 99.95m,
					Note = "This is a Startup invoice.",
					TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2"
				});
				entity.HasData(new Invoice
				{
					Id = Guid.NewGuid(),
					Total = 99.95m,
					Note = "This is a Startup invoice.",
					TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2"
				});
				entity.HasData(new Invoice
				{
					Id = Guid.NewGuid(),
					Total = 99.95m,
					Note = "This is a Startup invoice.",
					TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2"
				});

				// Company invoices
				entity.HasData(new Invoice
				{
					Id = Guid.NewGuid(),
					Total = 199.95m,
					Note = "This is a Company invoice.",
					TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46"
				});
				entity.HasData(new Invoice
				{
					Id = Guid.NewGuid(),
					Total = 199.95m,
					Note = "This is a Company invoice.",
					TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46"
				});
				entity.HasData(new Invoice
				{
					Id = Guid.NewGuid(),
					Total = 199.95m,
					Note = "This is a Company invoice.",
					TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46"
				});

				// Corporate invoices
				entity.HasData(new Invoice
				{
					Id = Guid.NewGuid(),
					Total = 399.95m,
					Note = "This is a Corporate invoice.",
					TenantId = "49a049d2-23ad-41df-8806-240aebaa2f17"
				});
				entity.HasData(new Invoice
				{
					Id = Guid.NewGuid(),
					Total = 399.95m,
					Note = "This is a Corporate invoice.",
					TenantId = "49a049d2-23ad-41df-8806-240aebaa2f17"
				});
				entity.HasData(new Invoice
				{
					Id = Guid.NewGuid(),
					Total = 399.95m,
					Note = "This is a Corporate invoice.",
					TenantId = "49a049d2-23ad-41df-8806-240aebaa2f17"
				});
			});
		}
	}
}
