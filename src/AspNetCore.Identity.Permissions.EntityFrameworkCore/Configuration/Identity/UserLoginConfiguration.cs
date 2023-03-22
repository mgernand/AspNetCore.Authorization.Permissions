// Copyright(c) .NET Foundation.All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Taken from PR: https://github.com/dotnet/aspnetcore/pull/13392
// Copyright(c) @dazinator (https://github.com/dazinator, http://darrelltunnell.net, Darrell Tunnell)

namespace MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore.Configuration.Identity
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
	public class UserLoginConfiguration : UserLoginConfiguration<IdentityUserLogin<string>, string>
	{
	}

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TUserLogin"></typeparam>
	[PublicAPI]
	public class UserLoginConfiguration<TUserLogin> : UserLoginConfiguration<TUserLogin, string>
		where TUserLogin : IdentityUserLogin<string>
	{
	}

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TUserLogin"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class UserLoginConfiguration<TUserLogin, TKey> : IEntityTypeConfiguration<TUserLogin>
		where TUserLogin : IdentityUserLogin<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the table name.
		/// </summary>
		public string Table { get; init; } = "AspNetUserLogins";

		/// <summary>
		///     Specifies the maximum length.
		/// </summary>
		/// <remarks>The default is 256.</remarks>
		public int MaxKeyLength { get; init; } = 256;

        /// <inheritdoc />
        public virtual void Configure(EntityTypeBuilder<TUserLogin> builder)
		{
			builder.ToTable(this.Table);

			builder.HasKey(l => new { l.LoginProvider, l.ProviderKey });

			builder.Property(l => l.LoginProvider).HasMaxLength(this.MaxKeyLength);
			builder.Property(l => l.ProviderKey).HasMaxLength(this.MaxKeyLength);
		}
	}
}
