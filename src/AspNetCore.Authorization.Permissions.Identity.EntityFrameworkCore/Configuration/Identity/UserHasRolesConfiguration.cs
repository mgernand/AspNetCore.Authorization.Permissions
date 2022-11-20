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
	/// <typeparam name="TUser"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class UserHasRolesConfiguration<TUser, TKey> : UserHasRolesConfiguration<TUser, IdentityUserRole<TKey>, TKey>
		where TUser : IdentityUser<TKey>
		where TKey : IEquatable<TKey>
	{
	}

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TUser"></typeparam>
	/// <typeparam name="TUserRole"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class UserHasRolesConfiguration<TUser, TUserRole, TKey> : IEntityTypeConfiguration<TUser>
		where TUser : IdentityUser<TKey>
		where TUserRole : IdentityUserRole<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <inheritdoc />
		public virtual void Configure(EntityTypeBuilder<TUser> builder)
		{
			builder.HasMany<TUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
		}
	}
}
