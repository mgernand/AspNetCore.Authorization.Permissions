// Copyright(c) .NET Foundation.All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Taken from PR: https://github.com/dotnet/aspnetcore/pull/13392
// Copyright(c) @dazinator (https://github.com/dazinator, http://darrelltunnell.net, Darrell Tunnell)

namespace MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore.Configuration.Identity
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore.Properties;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	[PublicAPI]
	public class UserTokenConfiguration : UserTokenConfiguration<IdentityUserToken<string>, string>
	{
	}

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TUserToken"></typeparam>
	[PublicAPI]
	public class UserTokenConfiguration<TUserToken> : UserTokenConfiguration<TUserToken, string>
		where TUserToken : IdentityUserToken<string>
	{
	}

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TUserToken"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class UserTokenConfiguration<TUserToken, TKey> : IEntityTypeConfiguration<TUserToken>
		where TUserToken : IdentityUserToken<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the table name.
		/// </summary>
		public string Table { get; init; } = "AspNetUserTokens";

		/// <summary>
		///     Specifies the maximum length.
		/// </summary>
		/// <remarks>The default is 256. Only applied if greater than 0.</remarks>
		public int MaxKeyLength { get; init; }

		/// <summary>
		///     If set, all properties on type <typeparamref name="TUserToken" /> marked with a
		///     <see cref="ProtectedPersonalDataAttribute" /> will be converted using this <see cref="ValueConverter" />.
		/// </summary>
		public ValueConverter<string, string> PersonalDataConverter { get; set; }

		/// <inheritdoc />
		public virtual void Configure(EntityTypeBuilder<TUserToken> builder)
		{
			builder.ToTable(this.Table);

			builder.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

			if(this.MaxKeyLength > 0)
			{
				builder.Property(t => t.LoginProvider).HasMaxLength(this.MaxKeyLength);
				builder.Property(t => t.Name).HasMaxLength(this.MaxKeyLength);
			}

			if(this.PersonalDataConverter != null)
			{
				IEnumerable<PropertyInfo> tokenProps = typeof(TUserToken)
					.GetProperties()
					.Where(prop => Attribute.IsDefined(prop, typeof(ProtectedPersonalDataAttribute)));

				foreach(PropertyInfo p in tokenProps)
				{
					if(p.PropertyType != typeof(string))
					{
						throw new InvalidOperationException(Resources.CanOnlyProtectStrings);
					}

					builder.Property(typeof(string), p.Name).HasConversion(this.PersonalDataConverter);
				}
			}
		}
	}
}
