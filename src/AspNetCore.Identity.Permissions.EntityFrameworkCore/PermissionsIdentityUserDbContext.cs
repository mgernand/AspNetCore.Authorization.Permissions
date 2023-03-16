// Copyright(c) .NET Foundation.All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Taken from PR: https://github.com/dotnet/aspnetcore/pull/13392
// Copyright(c) @dazinator (https://github.com/dazinator, http://darrelltunnell.net, Darrell Tunnell)

namespace MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	///     Base class for the Entity Framework database context used for identity.
	/// </summary>
	/// <typeparam name="TUser">The type of the user objects.</typeparam>
	[PublicAPI]
	public class PermissionsIdentityUserDbContext<TUser> : PermissionsIdentityUserDbContext<TUser, string> where TUser : IdentityUser
	{
		/// <summary>
		///     Initializes a new instance of <see cref="PermissionsIdentityUserDbContext{TUser}" />.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public PermissionsIdentityUserDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="PermissionsIdentityUserDbContext{TUser}" /> class.
		/// </summary>
		protected PermissionsIdentityUserDbContext()
		{
		}
	}

	/// <summary>
	///     Base class for the Entity Framework database context used for identity.
	/// </summary>
	/// <typeparam name="TUser">The type of user objects.</typeparam>
	/// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
	[PublicAPI]
	public class PermissionsIdentityUserDbContext<TUser, TKey> : PermissionsIdentityUserDbContext<TUser, TKey, IdentityUserClaim<TKey>, IdentityUserLogin<TKey>, IdentityUserToken<TKey>>
		where TUser : IdentityUser<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Initializes a new instance of the db context.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public PermissionsIdentityUserDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the class.
		/// </summary>
		protected PermissionsIdentityUserDbContext()
		{
		}
	}

	/// <summary>
	///     Base class for the Entity Framework database context used for identity.
	/// </summary>
	/// <typeparam name="TUser">The type of user objects.</typeparam>
	/// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
	/// <typeparam name="TUserClaim">The type of the user claim object.</typeparam>
	/// <typeparam name="TUserLogin">The type of the user login object.</typeparam>
	/// <typeparam name="TUserToken">The type of the user token object.</typeparam>
	[PublicAPI]
	public abstract class PermissionsIdentityUserDbContext<TUser, TKey, TUserClaim, TUserLogin, TUserToken> : DbContext
		where TUser : IdentityUser<TKey>
		where TKey : IEquatable<TKey>
		where TUserClaim : IdentityUserClaim<TKey>
		where TUserLogin : IdentityUserLogin<TKey>
		where TUserToken : IdentityUserToken<TKey>
	{
		/// <summary>
		///     Initializes a new instance of the class.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public PermissionsIdentityUserDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the class.
		/// </summary>
		protected PermissionsIdentityUserDbContext()
		{
		}

		/// <summary>
		///     Gets or sets the <see cref="DbSet{TEntity}" /> of Users.
		/// </summary>
		public virtual DbSet<TUser> Users { get; set; }

		/// <summary>
		///     Gets or sets the <see cref="DbSet{TEntity}" /> of User claims.
		/// </summary>
		public virtual DbSet<TUserClaim> UserClaims { get; set; }

		/// <summary>
		///     Gets or sets the <see cref="DbSet{TEntity}" /> of User logins.
		/// </summary>
		public virtual DbSet<TUserLogin> UserLogins { get; set; }

		/// <summary>
		///     Gets or sets the <see cref="DbSet{TEntity}" /> of User tokens.
		/// </summary>
		public virtual DbSet<TUserToken> UserTokens { get; set; }

		/// <summary>
		///     Configures the schema needed for the identity framework.
		/// </summary>
		/// <param name="builder">
		///     The builder being used to construct the model for this context.
		/// </param>
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyIdentityUser<TUser, TKey, TUserClaim, TUserLogin, TUserToken>(this);
		}
	}
}
