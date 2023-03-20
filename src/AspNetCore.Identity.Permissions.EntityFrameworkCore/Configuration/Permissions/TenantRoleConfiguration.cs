namespace MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore.Configuration.Permissions
{
    using System;
    using JetBrains.Annotations;
    using MadEyeMatt.Extensions.Identity.Permissions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    ///     An entity type configuration.
    /// </summary>
    /// <typeparam name="TTenantRole"></typeparam>
    [PublicAPI]
	public class TenantRoleConfiguration<TTenantRole> : TenantRoleConfiguration<TTenantRole, string>
		where TTenantRole : IdentityTenantRole<string>
	{
	}

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
		public virtual void Configure(EntityTypeBuilder<TTenantRole> builder)
		{
			builder.ToTable(this.Table);

			builder.HasKey(x => new { x.TenantId, x.RoleId });
		}
	}
}
