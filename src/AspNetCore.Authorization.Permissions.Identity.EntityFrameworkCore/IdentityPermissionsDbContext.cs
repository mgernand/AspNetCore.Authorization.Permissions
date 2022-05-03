namespace AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;

	/// <summary>
	///     Base class for the Entity Framework database context used for identity.
	/// </summary>
	[PublicAPI]
	public class IdentityPermissionsDbContext : IdentityPermissionsDbContext<PermissionsIdentityUser, PermissionsIdentityRole, PermissionsIdentityPermission, PermissionsIdentityTenant, string>
	{
		/// <summary>
		///     Initializes a new instance of <see cref="IdentityDbContext" />.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public IdentityPermissionsDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="IdentityDbContext" /> class.
		/// </summary>
		protected IdentityPermissionsDbContext()
		{
		}
	}

	/// <summary>
	///     Base class for the Entity Framework database context used for identity.
	/// </summary>
	/// <typeparam name="TPermission"></typeparam>
	/// <typeparam name="TTenant"></typeparam>
	[PublicAPI]
	public class IdentityPermissionsDbContext<TPermission, TTenant> : IdentityPermissionsDbContext<PermissionsIdentityUser, PermissionsIdentityRole, TPermission, TTenant, string>
		where TPermission : PermissionsIdentityPermission
		where TTenant : PermissionsIdentityTenant
	{
		/// <summary>
		///     Initializes a new instance of <see cref="IdentityDbContext" />.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public IdentityPermissionsDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="IdentityDbContext" /> class.
		/// </summary>
		protected IdentityPermissionsDbContext()
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
	public class IdentityPermissionsDbContext<TUser, TPermission, TTenant> : IdentityPermissionsDbContext<TUser, PermissionsIdentityRole, TPermission, TTenant, string>
		where TUser : PermissionsIdentityUser
		where TPermission : PermissionsIdentityPermission
		where TTenant : PermissionsIdentityTenant
	{
		/// <summary>
		///     Initializes a new instance of <see cref="IdentityDbContext" />.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public IdentityPermissionsDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="IdentityDbContext" /> class.
		/// </summary>
		protected IdentityPermissionsDbContext()
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
	public class IdentityPermissionsDbContext<TUser, TRole, TPermission, TTenant, TKey> : IdentityPermissionsDbContext<TUser, TRole, TPermission, TTenant, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>, IdentityRolePermission<TKey>, IdentityTenantRole<TKey>>
		where TUser : PermissionsIdentityUser<TKey>
		where TRole : PermissionsIdentityRole<TKey>
		where TPermission : PermissionsIdentityPermission<TKey>
		where TTenant : PermissionsIdentityTenant<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Initializes a new instance of the db context.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public IdentityPermissionsDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the class.
		/// </summary>
		protected IdentityPermissionsDbContext()
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
	public abstract class IdentityPermissionsDbContext<TUser, TRole, TPermission, TTenant, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken, TRolePermission, TTenantRole>
		: IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
		where TUser : PermissionsIdentityUser<TKey>
		where TRole : PermissionsIdentityRole<TKey>
		where TPermission : PermissionsIdentityPermission<TKey>
		where TTenant : PermissionsIdentityTenant<TKey>
		where TKey : IEquatable<TKey>
		where TUserClaim : IdentityUserClaim<TKey>
		where TUserRole : IdentityUserRole<TKey>
		where TUserLogin : IdentityUserLogin<TKey>
		where TRoleClaim : IdentityRoleClaim<TKey>
		where TUserToken : IdentityUserToken<TKey>
		where TRolePermission : IdentityRolePermission<TKey>
		where TTenantRole : IdentityTenantRole<TKey>
	{
		/// <summary>
		///     Initializes a new instance of the class.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		protected IdentityPermissionsDbContext(DbContextOptions options) : base(options)
		{
		}

		/// <summary>
		///     Initializes a new instance of the class.
		/// </summary>
		protected IdentityPermissionsDbContext()
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

			builder.Entity<TPermission>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.HasIndex(x => x.NormalizedName).HasDatabaseName("PermissionNameIndex").IsUnique();
				entity.ToTable("AspNetPermissions");
				entity.Property(x => x.ConcurrencyStamp).IsConcurrencyToken();

				entity.Property(x => x.Name).HasMaxLength(256);
				entity.Property(x => x.NormalizedName).HasMaxLength(256);

				entity.HasMany<TRolePermission>().WithOne().HasForeignKey(x => x.PermissionId).IsRequired();
			});

			builder.Entity<TRolePermission>(entity =>
			{
				entity.HasKey(x => new { x.RoleId, x.PermissionId });
				entity.ToTable("AspNetRolePermissions");
			});

			builder.Entity<TUser>(entity =>
			{
				entity.HasOne<TTenant>().WithMany().HasForeignKey(x => x.TenantId);
			});

			builder.Entity<TTenant>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.HasIndex(x => x.NormalizedName).HasDatabaseName("TenantNameIndex").IsUnique();
				entity.ToTable("AspNetTenants");
				entity.Property(x => x.ConcurrencyStamp).IsConcurrencyToken();

				entity.Property(x => x.Name).HasMaxLength(256);
				entity.Property(x => x.NormalizedName).HasMaxLength(256);
			});

			builder.Entity<TTenantRole>(entity =>
			{
				entity.HasKey(x => new { x.TenantId, x.RoleId });
				entity.ToTable("AspNetTenantRoles");
			});
		}
	}
}
