namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore.Configuration
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TRolePermission"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class RolePermissionConfiguration<TRolePermission, TKey> : IEntityTypeConfiguration<TRolePermission>
		where TRolePermission : IdentityRolePermission<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the table name.
		/// </summary>
		public string Table { get; init; } = "AspNetRolePermissions";

		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<TRolePermission> builder)
		{
			builder.ToTable(this.Table);

			builder.HasKey(x => new { x.RoleId, x.PermissionId });
		}
	}
}
