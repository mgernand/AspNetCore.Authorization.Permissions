// Copyright(c) .NET Foundation.All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Taken from PR: https://github.com/dotnet/aspnetcore/pull/13392
// Copyright(c) @dazinator (https://github.com/dazinator, http://darrelltunnell.net, Darrell Tunnell)

namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore.Configuration.Identity
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TRole"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class RoleConfiguration<TRole, TKey> : RoleConfiguration<TRole, IdentityUserRole<TKey>, IdentityRoleClaim<TKey>, TKey>
		where TRole : IdentityRole<TKey>
		where TKey : IEquatable<TKey>
	{
	}

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TRole"></typeparam>
	/// <typeparam name="TUserRole"></typeparam>
	/// <typeparam name="TRoleClaim"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class RoleConfiguration<TRole, TUserRole, TRoleClaim, TKey> : IEntityTypeConfiguration<TRole>
		where TRole : IdentityRole<TKey>
		where TUserRole : IdentityUserRole<TKey>
		where TRoleClaim : IdentityRoleClaim<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the table name.
		/// </summary>
		public string Table { get; init; } = "AspNetRoles";

		/// <inheritdoc />
		public virtual void Configure(EntityTypeBuilder<TRole> builder)
		{
			builder.ToTable(this.Table);

			builder.HasKey(r => r.Id);
			builder.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();

			builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
			builder.Property(u => u.Name).HasMaxLength(256);
			builder.Property(u => u.NormalizedName).HasMaxLength(256);

			builder.HasMany<TUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
			builder.HasMany<TRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
		}
	}
}
