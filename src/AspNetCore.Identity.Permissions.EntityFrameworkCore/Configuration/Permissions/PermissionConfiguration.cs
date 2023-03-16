namespace MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore.Configuration.Permissions
{
	using System;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.Permissions.Model;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TPermission"></typeparam>
	/// <typeparam name="TRolePermission"></typeparam>
	[PublicAPI]
	public class PermissionConfiguration<TPermission, TRolePermission> : PermissionConfiguration<TPermission, TRolePermission, string>
		where TPermission : IdentityPermission<string>
		where TRolePermission : IdentityRolePermission<string>
	{
	}

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TPermission"></typeparam>
	/// <typeparam name="TRolePermission"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class PermissionConfiguration<TPermission, TRolePermission, TKey> : IEntityTypeConfiguration<TPermission>
		where TPermission : IdentityPermission<TKey>
		where TRolePermission : IdentityRolePermission<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the table name.
		/// </summary>
		public string Table { get; init; } = "AspNetPermissions";

		/// <summary>
		///     Specifies the maximum length.
		/// </summary>
		/// <remarks>The default is 256. Only applied if greater than 0.</remarks>
		public int MaxKeyLength { get; init; } = 256;

		/// <inheritdoc />
		public virtual void Configure(EntityTypeBuilder<TPermission> builder)
		{
			builder.ToTable(this.Table);

			builder.HasKey(x => x.Id);
			builder.HasIndex(x => x.NormalizedName).HasDatabaseName("PermissionNameIndex").IsUnique();

			builder.Property(x => x.ConcurrencyStamp).IsConcurrencyToken();

			if(this.MaxKeyLength > 0)
			{
				builder.Property(x => x.Name).HasMaxLength(this.MaxKeyLength);
				builder.Property(x => x.NormalizedName).HasMaxLength(this.MaxKeyLength);
			}

			builder.HasMany<TRolePermission>().WithOne().HasForeignKey(x => x.PermissionId).IsRequired();
		}
	}
}
