namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore.Configuration
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TTenantRole"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class TenantRoleConfiguration<TTenantRole, TKey> : IEntityTypeConfiguration<TTenantRole>
		where TTenantRole : IdentityTenantRole<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the table name.
		/// </summary>
		public string Table { get; init; } = "AspNetTenantRoles";

		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<TTenantRole> builder)
		{
			builder.ToTable(this.Table);

			builder.HasKey(x => new { x.TenantId, x.RoleId });
		}
	}
}
