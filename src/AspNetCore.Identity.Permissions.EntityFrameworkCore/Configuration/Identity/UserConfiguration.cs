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
	public class UserConfiguration : UserConfiguration<IdentityUser, IdentityUserClaim<string>, IdentityUserLogin<string>, IdentityUserToken<string>, string>
	{
	}

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TUser"></typeparam>
	[PublicAPI]
	public class UserConfiguration<TUser> : UserConfiguration<TUser, IdentityUserClaim<string>, IdentityUserLogin<string>, IdentityUserToken<string>, string>
		where TUser : IdentityUser<string>
	{
	}

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TUser"></typeparam>
	/// <typeparam name="TUserClaim"></typeparam>
	/// <typeparam name="TUserLogin"></typeparam>
	/// <typeparam name="TUserToken"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class UserConfiguration<TUser, TUserClaim, TUserLogin, TUserToken, TKey> : IEntityTypeConfiguration<TUser>
		where TUser : IdentityUser<TKey>
		where TUserClaim : IdentityUserClaim<TKey>
		where TUserLogin : IdentityUserLogin<TKey>
		where TUserToken : IdentityUserToken<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the table name.
		/// </summary>
		public string Table { get; init; } = "AspNetUsers";

		/// <summary>
		///     If set, all properties on type <typeparamref name="TUser" /> marked with a
		///     <see cref="ProtectedPersonalDataAttribute" /> will be converted using this <see cref="ValueConverter" />.
		/// </summary>
		public ValueConverter<string, string> PersonalDataConverter { get; set; }

		/// <inheritdoc />
		public virtual void Configure(EntityTypeBuilder<TUser> builder)
		{
			builder.ToTable(this.Table);

			builder.HasKey(u => u.Id);
			builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
			builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

			builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
			builder.Property(u => u.UserName).HasMaxLength(256);
			builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
			builder.Property(u => u.Email).HasMaxLength(256);
			builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

			if(this.PersonalDataConverter != null)
			{
				IEnumerable<PropertyInfo> personalDataProps = typeof(TUser)
					.GetProperties()
					.Where(prop => Attribute.IsDefined(prop, typeof(ProtectedPersonalDataAttribute)));

				foreach(PropertyInfo p in personalDataProps)
				{
					if(p.PropertyType != typeof(string))
					{
						throw new InvalidOperationException(Resources.CanOnlyProtectStrings);
					}

					builder.Property(typeof(string), p.Name).HasConversion(this.PersonalDataConverter);
				}
			}

			builder.HasMany<TUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
			builder.HasMany<TUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
			builder.HasMany<TUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
		}
	}
}
