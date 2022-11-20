namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore.Configuration
{
	using System;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.Model;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TTenant"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class TenantConfiguration<TTenant, TKey> : IEntityTypeConfiguration<TTenant>
		where TTenant : PermissionsTenant<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the table name.
		/// </summary>
		public string Table { get; init; } = "AspNetTenants";

		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<TTenant> builder)
		{
			builder.ToTable(this.Table);

			builder.HasKey(x => x.Id);
			builder.HasIndex(x => x.NormalizedName).HasDatabaseName("TenantNameIndex").IsUnique();
			builder.Property(x => x.ConcurrencyStamp).IsConcurrencyToken();
			builder.Property(x => x.Name).HasMaxLength(256);
			builder.Property(x => x.NormalizedName).HasMaxLength(256);
		}
	}
}
