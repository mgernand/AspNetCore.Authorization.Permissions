namespace SamplePermissions
{
	using System;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.ValueGeneration;
	using SamplePermissions.Model;

	public class InvoicesDbContext : DbContext
	{
		/// <summary>
		///     Initializes a new instance of <see cref="InvoicesDbContext" />.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public InvoicesDbContext(DbContextOptions<InvoicesDbContext> options)
			: base(options)
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

				// Company invoices
				entity.HasData(new Invoice
				{
					Id = Guid.NewGuid(),
					Total = 199.95m,
					Note = "This is a Company invoice."
				});
				entity.HasData(new Invoice
				{
					Id = Guid.NewGuid(),
					Total = 199.95m,
					Note = "This is a Company invoice."
				});
				entity.HasData(new Invoice
				{
					Id = Guid.NewGuid(),
					Total = 199.95m,
					Note = "This is a Company invoice."
				});
			});
		}
	}
}
