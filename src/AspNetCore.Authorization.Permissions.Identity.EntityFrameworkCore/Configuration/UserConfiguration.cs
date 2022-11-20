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
	/// <typeparam name="TUser"></typeparam>
	/// <typeparam name="TTenant"></typeparam>
	[PublicAPI]
	public class UserConfiguration<TUser, TTenant> : UserConfiguration<TUser, TTenant, string>
		where TUser : PermissionsUser<string>
		where TTenant : PermissionsTenant<string>
	{
	}

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TUser"></typeparam>
	/// <typeparam name="TTenant"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class UserConfiguration<TUser, TTenant, TKey> : IEntityTypeConfiguration<TUser>
		where TUser : PermissionsUser<TKey>
		where TTenant : PermissionsTenant<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <inheritdoc />
		public void Configure(EntityTypeBuilder<TUser> builder)
		{
			builder.HasOne<TTenant>().WithMany().HasForeignKey(x => x.TenantId);
		}
	}
}
