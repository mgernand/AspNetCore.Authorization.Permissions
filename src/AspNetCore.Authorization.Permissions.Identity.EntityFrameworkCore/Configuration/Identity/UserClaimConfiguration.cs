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
	[PublicAPI]
	public class UserClaimConfiguration : UserClaimConfiguration<IdentityUserClaim<string>>
	{
	}

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TUserClaim"></typeparam>
	[PublicAPI]
	public class UserClaimConfiguration<TUserClaim> : UserClaimConfiguration<TUserClaim, string>
		where TUserClaim : IdentityUserClaim<string>
	{
	}

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TUserClaim"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class UserClaimConfiguration<TUserClaim, TKey> : IEntityTypeConfiguration<TUserClaim>
		where TUserClaim : IdentityUserClaim<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the table name.
		/// </summary>
		public string Table { get; init; } = "AspNetUserClaims";

		/// <inheritdoc />
		public virtual void Configure(EntityTypeBuilder<TUserClaim> builder)
		{
			builder.ToTable(this.Table);

			builder.HasKey(uc => uc.Id);
		}
	}
}
