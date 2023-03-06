// Copyright(c) .NET Foundation.All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Taken from PR: https://github.com/dotnet/aspnetcore/pull/13392
// Copyright(c) @dazinator (https://github.com/dazinator, http://darrelltunnell.net, Darrell Tunnell)

namespace MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore
{
	using System;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	///     Base class for the Entity Framework database context used for identity.
	/// </summary>
	public class PermissionsIdentityDbContext : PermissionsIdentityDbContext<IdentityUser, IdentityRole, string>
	{
		/// <summary>
		///     Initializes a new instance of <see cref="PermissionsIdentityDbContext" />.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public PermissionsIdentityDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="PermissionsIdentityDbContext" /> class.
		/// </summary>
		protected PermissionsIdentityDbContext()
		{
		}
	}

	/// <summary>
	///     Base class for the Entity Framework database context used for identity.
	/// </summary>
	/// <typeparam name="TUser">The type of the user objects.</typeparam>
	public class PermissionsIdentityDbContext<TUser> : PermissionsIdentityDbContext<TUser, IdentityRole, string> where TUser : IdentityUser
	{
		/// <summary>
		///     Initializes a new instance of <see cref="PermissionsIdentityDbContext" />.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public PermissionsIdentityDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="PermissionsIdentityDbContext" /> class.
		/// </summary>
		protected PermissionsIdentityDbContext()
		{
		}
	}

	/// <summary>
	///     Base class for the Entity Framework database context used for identity.
	/// </summary>
	/// <typeparam name="TUser">The type of user objects.</typeparam>
	/// <typeparam name="TRole">The type of role objects.</typeparam>
	/// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
	public class PermissionsIdentityDbContext<TUser, TRole, TKey> : PermissionsIdentityDbContext<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
		where TUser : IdentityUser<TKey>
		where TRole : IdentityRole<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Initializes a new instance of the db context.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public PermissionsIdentityDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the class.
		/// </summary>
		protected PermissionsIdentityDbContext()
		{
		}
	}

	/// <summary>
	///     Base class for the Entity Framework database context used for identity.
	/// </summary>
	/// <typeparam name="TUser">The type of user objects.</typeparam>
	/// <typeparam name="TRole">The type of role objects.</typeparam>
	/// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
	/// <typeparam name="TUserClaim">The type of the user claim object.</typeparam>
	/// <typeparam name="TUserRole">The type of the user role object.</typeparam>
	/// <typeparam name="TUserLogin">The type of the user login object.</typeparam>
	/// <typeparam name="TRoleClaim">The type of the role claim object.</typeparam>
	/// <typeparam name="TUserToken">The type of the user token object.</typeparam>
	public abstract class PermissionsIdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken> : PermissionsIdentityUserDbContext<TUser, TKey, TUserClaim, TUserLogin, TUserToken>
		where TUser : IdentityUser<TKey>
		where TRole : IdentityRole<TKey>
		where TKey : IEquatable<TKey>
		where TUserClaim : IdentityUserClaim<TKey>
		where TUserRole : IdentityUserRole<TKey>
		where TUserLogin : IdentityUserLogin<TKey>
		where TRoleClaim : IdentityRoleClaim<TKey>
		where TUserToken : IdentityUserToken<TKey>
	{
		/// <summary>
		///     Initializes a new instance of the class.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public PermissionsIdentityDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the class.
		/// </summary>
		protected PermissionsIdentityDbContext()
		{
		}

		/// <summary>
		///     Gets or sets the <see cref="DbSet{TEntity}" /> of User roles.
		/// </summary>
		public virtual DbSet<TUserRole> UserRoles { get; set; }

		/// <summary>
		///     Gets or sets the <see cref="DbSet{TEntity}" /> of roles.
		/// </summary>
		public virtual DbSet<TRole> Roles { get; set; }

		/// <summary>
		///     Gets or sets the <see cref="DbSet{TEntity}" /> of role claims.
		/// </summary>
		public virtual DbSet<TRoleClaim> RoleClaims { get; set; }

		/// <summary>
		///     Configures the schema needed for the identity framework.
		/// </summary>
		/// <param name="builder">
		///     The builder being used to construct the model for this context.
		/// </param>
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyIdentityUserRoles<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>(this);
		}
	}
}
