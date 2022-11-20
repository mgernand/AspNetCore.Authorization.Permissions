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
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class RoleClaimConfiguration<TKey> : RoleClaimConfiguration<IdentityRoleClaim<TKey>, TKey>
		where TKey : IEquatable<TKey>
	{
	}

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TRoleClaim"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class RoleClaimConfiguration<TRoleClaim, TKey> : IEntityTypeConfiguration<TRoleClaim>
		where TRoleClaim : IdentityRoleClaim<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the table name.
		/// </summary>
		public string Table { get; init; } = "AspNetRoleClaims";

		/// <inheritdoc />
		public virtual void Configure(EntityTypeBuilder<TRoleClaim> builder)
		{
			builder.ToTable(this.Table);

			builder.HasKey(rc => rc.Id);
		}
	}
}
