namespace MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore.Configuration.Permissions
{
	using System;
	using JetBrains.Annotations;
	using MadEyeMatt.Extensions.Identity.Permissions.Stores;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TUser"></typeparam>
	/// <typeparam name="TTenant"></typeparam>
	[PublicAPI]
	public class UserBelongsToTenantConfiguration<TUser, TTenant> : TenantUserConfiguration<TUser, TTenant, string>
		where TUser : IdentityTenantUser<string>
		where TTenant : IdentityTenant<string>
	{
	}

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TUser"></typeparam>
	/// <typeparam name="TTenant"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class TenantUserConfiguration<TUser, TTenant, TKey> : IEntityTypeConfiguration<TUser>
		where TUser : IdentityTenantUser<TKey>
		where TTenant : IdentityTenant<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <inheritdoc />
		public virtual void Configure(EntityTypeBuilder<TUser> builder)
		{
			builder.HasOne<TTenant>().WithMany().HasForeignKey(x => x.TenantId);
		}
	}
}
