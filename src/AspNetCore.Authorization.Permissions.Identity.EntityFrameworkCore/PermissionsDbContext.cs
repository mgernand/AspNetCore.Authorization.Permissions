namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore
{
	using System;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.Model;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	///     Base class for the Entity Framework database context used for identity.
	/// </summary>
	[PublicAPI]
	public class PermissionsDbContext : PermissionsDbContext<PermissionsUser, PermissionsRole, PermissionsPermission, PermissionsTenant, string>
	{
		/// <summary>
		///     Initializes a new instance of <see cref="PermissionsIdentityDbContext" />.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public PermissionsDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="PermissionsIdentityDbContext" /> class.
		/// </summary>
		protected PermissionsDbContext()
		{
		}
	}

	/// <summary>
	///     Base class for the Entity Framework database context used for identity.
	/// </summary>
	/// <typeparam name="TPermission"></typeparam>
	/// <typeparam name="TTenant"></typeparam>
	[PublicAPI]
	public class PermissionsDbContext<TPermission, TTenant> : PermissionsDbContext<PermissionsUser, PermissionsRole, TPermission, TTenant, string>
		where TPermission : PermissionsPermission
		where TTenant : PermissionsTenant
	{
		/// <summary>
		///     Initializes a new instance of <see cref="PermissionsIdentityDbContext" />.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public PermissionsDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="PermissionsIdentityDbContext" /> class.
		/// </summary>
		protected PermissionsDbContext()
		{
		}
	}

	/// <summary>
	///     Base class for the Entity Framework database context used for identity.
	/// </summary>
	/// <typeparam name="TUser">The type of the user objects.</typeparam>
	/// <typeparam name="TPermission"></typeparam>
	/// <typeparam name="TTenant"></typeparam>
	[PublicAPI]
	public class PermissionsDbContext<TUser, TPermission, TTenant> : PermissionsDbContext<TUser, PermissionsRole, TPermission, TTenant, string>
		where TUser : PermissionsUser
		where TPermission : PermissionsPermission
		where TTenant : PermissionsTenant
	{
		/// <summary>
		///     Initializes a new instance of <see cref="PermissionsIdentityDbContext" />.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public PermissionsDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="PermissionsIdentityDbContext" /> class.
		/// </summary>
		protected PermissionsDbContext()
		{
		}
	}

	/// <summary>
	///     Base class for the Entity Framework database context used for identity.
	/// </summary>
	/// <typeparam name="TUser">The type of user objects.</typeparam>
	/// <typeparam name="TRole">The type of role objects.</typeparam>
	/// <typeparam name="TTenant">The type of the tenant objects.</typeparam>
	/// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
	/// <typeparam name="TPermission"></typeparam>
	[PublicAPI]
	public class PermissionsDbContext<TUser, TRole, TPermission, TTenant, TKey> : PermissionsDbContext<TUser, TRole, TPermission, TTenant, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>, PermissionsRolePermission<TKey>, PermissionsTenantRole<TKey>>
		where TUser : PermissionsUser<TKey>
		where TRole : PermissionsRole<TKey>
		where TPermission : PermissionsPermission<TKey>
		where TTenant : PermissionsTenant<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Initializes a new instance of the db context.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public PermissionsDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the class.
		/// </summary>
		protected PermissionsDbContext()
		{
		}
	}

	/// <summary>
	///     Base class for the Entity Framework database context used for identity.
	/// </summary>
	/// <typeparam name="TUser">The type of user objects.</typeparam>
	/// <typeparam name="TRole">The type of role objects.</typeparam>
	/// <typeparam name="TPermission">The type of permission objects.</typeparam>
	/// <typeparam name="TTenant">The type of tenant objects.</typeparam>
	/// <typeparam name="TKey">The type of the primary key for users and roles.</typeparam>
	/// <typeparam name="TUserClaim">The type of the user claim object.</typeparam>
	/// <typeparam name="TUserRole">The type of the user role object.</typeparam>
	/// <typeparam name="TUserLogin">The type of the user login object.</typeparam>
	/// <typeparam name="TRoleClaim">The type of the role claim object.</typeparam>
	/// <typeparam name="TUserToken">The type of the user token object.</typeparam>
	/// <typeparam name="TRolePermission">The type of the role permission object.</typeparam>
	/// <typeparam name="TTenantRole">The type of the tenant role object.</typeparam>
	[PublicAPI]
	public abstract class PermissionsDbContext<TUser, TRole, TPermission, TTenant, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken, TRolePermission, TTenantRole> : PermissionsIdentityDbContext
		where TUser : PermissionsUser<TKey>
		where TRole : PermissionsRole<TKey>
		where TPermission : PermissionsPermission<TKey>
		where TTenant : PermissionsTenant<TKey>
		where TKey : IEquatable<TKey>
		where TUserClaim : IdentityUserClaim<TKey>
		where TUserRole : IdentityUserRole<TKey>
		where TUserLogin : IdentityUserLogin<TKey>
		where TRoleClaim : IdentityRoleClaim<TKey>
		where TUserToken : IdentityUserToken<TKey>
		where TRolePermission : PermissionsRolePermission<TKey>
		where TTenantRole : PermissionsTenantRole<TKey>
	{
		/// <summary>
		///     Initializes a new instance of the class.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		protected PermissionsDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the class.
		/// </summary>
		protected PermissionsDbContext()
		{
		}

		/// <summary>
		///     Gets or sets the <see cref="DbSet{TEntity}" /> of permissions.
		/// </summary>
		public virtual DbSet<TPermission> Permissions { get; set; }

		/// <summary>
		///     Gets or sets the <see cref="DbSet{TPermissionRole}" /> of permission roles.
		/// </summary>
		public virtual DbSet<TRolePermission> PermissionRoles { get; set; }

		/// <summary>
		///     Gets or sets the <see cref="DbSet{TEntity}" /> of tenants.
		/// </summary>
		public virtual DbSet<TTenant> Tenants { get; set; }

		/// <inheritdoc />
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyIdentityPermissions<TUser, TRole, TPermission, TTenant, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken, TRolePermission, TTenantRole>(this);
		}
	}
}
