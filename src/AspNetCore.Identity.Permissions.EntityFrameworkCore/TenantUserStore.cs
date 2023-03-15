namespace MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore
{
	using System;
	using System.Linq;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.Permissions.Model;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	///     Represents a new instance of a persistence store for users, using the default implementation
	///     of <see cref="PermissionsUser{TKey}" /> with a string as a primary key.
	/// </summary>
	[PublicAPI]
	public class TenantUserStore : TenantUserStore<PermissionsUser>
	{
		/// <summary>
		///     Constructs a new instance of <see cref="TenantUserStore" />.
		/// </summary>
		/// <param name="context">The <see cref="DbContext" />.</param>
		/// <param name="describer">The <see cref="IdentityErrorDescriber" />.</param>
		public TenantUserStore(DbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
		{
		}
	}

	/// <summary>
	///     Creates a new instance of a persistence store for the specified user type.
	/// </summary>
	/// <typeparam name="TUser">The type representing a user.</typeparam>
	[PublicAPI]
	public class TenantUserStore<TUser> : TenantUserStore<TUser, DbContext, string>
		where TUser : PermissionsUser, new()
	{
		/// <summary>
		///     Constructs a new instance of <see cref="TenantUserStore{TUser}" />.
		/// </summary>
		/// <param name="context">The <see cref="DbContext" />.</param>
		/// <param name="describer">The <see cref="IdentityErrorDescriber" />.</param>
		public TenantUserStore(DbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
		{
		}
	}

	/// <summary>
	///     Represents a new instance of a persistence store for the specified user and role types.
	/// </summary>
	/// <typeparam name="TUser">The type representing a user.</typeparam>
	/// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
	[PublicAPI]
	public class TenantUserStore<TUser, TContext> : TenantUserStore<TUser, TContext, string>
		where TUser : PermissionsUser
		where TContext : DbContext
	{
		/// <summary>
		///     Constructs a new instance of <see cref="TenantUserStore{TUser,TContext,TKey}" />.
		/// </summary>
		/// <param name="context">The <see cref="DbContext" />.</param>
		/// <param name="describer">The <see cref="IdentityErrorDescriber" />.</param>
		public TenantUserStore(TContext context, IdentityErrorDescriber describer = null) : base(context, describer)
		{
		}
	}

	/// <summary>
	///     Represents a new instance of a persistence store for the specified user and role types.
	/// </summary>
	/// <typeparam name="TUser">The type representing a user.</typeparam>
	/// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
	/// <typeparam name="TKey">The type of the primary key for a role.</typeparam>
	[PublicAPI]
	public class TenantUserStore<TUser, TContext, TKey> : TenantUserStoreBase<TUser, TKey>
		where TUser : PermissionsUser<TKey>
		where TContext : DbContext
		where TKey : IEquatable<TKey>
	{
		/// <inheritdoc />
		public TenantUserStore(TContext context, IdentityErrorDescriber describer = null)
			: base(describer)
		{
			this.Context = context;
		}

		/// <summary>
		///     Gets the database context for this store.
		/// </summary>
		public virtual TContext Context { get; }

		/// <inheritdoc />
		public override IQueryable<TUser> Users => this.Context.Set<TUser>();
	}
}
