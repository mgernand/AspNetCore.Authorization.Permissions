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
	/// <typeparam name="TPermission"></typeparam>
	/// <typeparam name="TRolePermission"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class PermissionConfiguration<TPermission, TRolePermission, TKey> : IEntityTypeConfiguration<TPermission>
		where TPermission : PermissionsPermission<TKey>
		where TRolePermission : IdentityRolePermission<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the table name.
		/// </summary>
		public string Table { get; init; } = "AspNetPermissions";

		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<TPermission> builder)
		{
			builder.ToTable(this.Table);

			builder.HasKey(x => x.Id);
			builder.HasIndex(x => x.NormalizedName).HasDatabaseName("PermissionNameIndex").IsUnique();
			builder.Property(x => x.ConcurrencyStamp).IsConcurrencyToken();
			builder.Property(x => x.Name).HasMaxLength(256);
			builder.Property(x => x.NormalizedName).HasMaxLength(256);

			builder.HasMany<TRolePermission>().WithOne().HasForeignKey(x => x.PermissionId).IsRequired();
		}
	}
}
