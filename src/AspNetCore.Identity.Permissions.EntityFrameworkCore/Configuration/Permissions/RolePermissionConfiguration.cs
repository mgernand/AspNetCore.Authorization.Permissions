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
	/// <typeparam name="TRolePermission"></typeparam>
	[PublicAPI]
	public class RolePermissionConfiguration<TRolePermission> : RolePermissionConfiguration<TRolePermission, string>
		where TRolePermission : PermissionsRolePermission<string>
	{
	}

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TRolePermission"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class RolePermissionConfiguration<TRolePermission, TKey> : IEntityTypeConfiguration<TRolePermission>
		where TRolePermission : PermissionsRolePermission<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the table name.
		/// </summary>
		public string Table { get; init; } = "AspNetRolePermissions";

		/// <inheritdoc />
		public virtual void Configure(EntityTypeBuilder<TRolePermission> builder)
		{
			builder.ToTable(this.Table);

			builder.HasKey(x => new { x.RoleId, x.PermissionId });
		}
	}
}
