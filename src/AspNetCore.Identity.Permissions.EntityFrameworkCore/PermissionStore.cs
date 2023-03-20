namespace MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using MadEyeMatt.Extensions.Identity.Permissions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     Represents a new instance of a persistence store for permissions, using the default implementation
    ///     of <see cref="IdentityPermission" /> and <see cref="IdentityRole{TKey}" /> with a string as a primary key.
    /// </summary>
    [PublicAPI]
	public class PermissionStore : PermissionStore<IdentityPermission, IdentityRole, DbContext, string>
	{
		/// <summary>
		///     Constructs a new instance of <see cref="PermissionStore{TPermission, TRole}" />.
		/// </summary>
		/// <param name="context">The <see cref="DbContext" />.</param>
		/// <param name="describer">The <see cref="IdentityErrorDescriber" />.</param>
		public PermissionStore(DbContext context, IdentityErrorDescriber describer = null)
			: base(context, describer)
		{
		}
	}

	/// <summary>
	///     Represents a new instance of a persistence store for the specified permission type.
	/// </summary>
	/// <typeparam name="TPermission">The type of the class representing a permission</typeparam>
	[PublicAPI]
	public class PermissionStore<TPermission> : PermissionStore<TPermission, IdentityRole, DbContext, string>
		where TPermission : IdentityPermission
	{
		/// <summary>
		///     Constructs a new instance of <see cref="PermissionStore{TPermission, TRole}" />.
		/// </summary>
		/// <param name="context">The <see cref="DbContext" />.</param>
		/// <param name="describer">The <see cref="IdentityErrorDescriber" />.</param>
		public PermissionStore(DbContext context, IdentityErrorDescriber describer = null)
			: base(context, describer)
		{
		}
	}

	/// <summary>
	///     Represents a new instance of a persistence store for the specified permission and role types.
	/// </summary>
	/// <typeparam name="TPermission">The type of the class representing a permission</typeparam>
	/// <typeparam name="TRole">The type of the class representing a role</typeparam>
	[PublicAPI]
	public class PermissionStore<TPermission, TRole> : PermissionStore<TPermission, TRole, DbContext, string>
		where TPermission : IdentityPermission
		where TRole : IdentityRole
	{
		/// <summary>
		///     Constructs a new instance of <see cref="PermissionStore{TPermission, TRole}" />.
		/// </summary>
		/// <param name="context">The <see cref="DbContext" />.</param>
		/// <param name="describer">The <see cref="IdentityErrorDescriber" />.</param>
		public PermissionStore(DbContext context, IdentityErrorDescriber describer = null)
			: base(context, describer)
		{
		}
	}

	/// <summary>
	///     Represents a new instance of a persistence store for the specified permission, role and db context types.
	/// </summary>
	/// <typeparam name="TPermission">The type of the class representing a permission.</typeparam>
	/// <typeparam name="TRole">The type of the class representing a role</typeparam>
	/// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
	[PublicAPI]
	public class PermissionStore<TPermission, TRole, TContext> : PermissionStore<TPermission, TRole, TContext, string>
		where TPermission : IdentityPermission
		where TRole : IdentityRole
		where TContext : DbContext
	{
		/// <summary>
		///     Constructs a new instance of <see cref="PermissionStore{TPermission, TRole, TContext}" />.
		/// </summary>
		/// <param name="context">The <see cref="DbContext" />.</param>
		/// <param name="describer">The <see cref="IdentityErrorDescriber" />.</param>
		public PermissionStore(TContext context, IdentityErrorDescriber describer = null)
			: base(context, describer)
		{
		}
	}

	/// <summary>
	///     Represents a new instance of a persistence store for the specified permission, role, db context and key types.
	/// </summary>
	/// <typeparam name="TPermission">The type of the class representing a permission.</typeparam>
	/// <typeparam name="TRole"></typeparam>
	/// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
	/// <typeparam name="TKey">The type of the primary key for a permission.</typeparam>
	[PublicAPI]
	public class PermissionStore<TPermission, TRole, TContext, TKey> : PermissionStore<TPermission, TRole, TContext, TKey, IdentityRolePermission<TKey>>
		where TPermission : IdentityPermission<TKey>
		where TRole : IdentityRole<TKey>
		where TContext : DbContext
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Constructs a new instance of <see cref="PermissionStore{TRole, TContext, TKey}" />.
		/// </summary>
		/// <param name="context">The <see cref="DbContext" />.</param>
		/// <param name="describer">The <see cref="IdentityErrorDescriber" />.</param>
		public PermissionStore(TContext context, IdentityErrorDescriber describer = null)
			: base(context, describer)
		{
		}
	}

	/// <summary>
	///     Represents a new instance of a persistence store for the specified permission, role, db context, key and role
	///     permission types.
	/// </summary>
	/// <typeparam name="TPermission">The type of the class representing a permission.</typeparam>
	/// <typeparam name="TRole"></typeparam>
	/// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
	/// <typeparam name="TKey">The type of the primary key for a role.</typeparam>
	/// <typeparam name="TRolePermission">The type of the class representing a role permission.</typeparam>
	[PublicAPI]
	public class PermissionStore<TPermission, TRole, TContext, TKey, TRolePermission> : PermissionStoreBase<TPermission, TRole, TKey, TRolePermission>,
		IQueryablePermissionStore<TPermission>,
		IRolePermissionStore<TPermission>
		where TPermission : IdentityPermission<TKey>
		where TRole : IdentityRole<TKey>
		where TKey : IEquatable<TKey>
		where TContext : DbContext
		where TRolePermission : IdentityRolePermission<TKey>, new()
	{
		/// <summary>
		///     Constructs a new instance of <see cref="PermissionStore{TPermission, TContext, TKey, TRolePermission}" />.
		/// </summary>
		/// <param name="context">The <see cref="DbContext" />.</param>
		/// <param name="describer">The <see cref="IdentityErrorDescriber" />.</param>
		public PermissionStore(TContext context, IdentityErrorDescriber describer = null)
			: base(describer)
		{
			this.Context = context ?? throw new ArgumentNullException(nameof(context));
		}

		/// <summary>
		///     Gets the database context for this store.
		/// </summary>
		public virtual TContext Context { get; }

		/// <summary>
		///     Gets or sets a flag indicating if changes should be persisted after CreateAsync, UpdateAsync and DeleteAsync are
		///     called.
		/// </summary>
		/// <value>
		///     True if changes should be automatically persisted, otherwise false.
		/// </value>
		public bool AutoSaveChanges { get; set; } = true;

		private DbSet<TRolePermission> RolePermissions => this.Context.Set<TRolePermission>();

		private DbSet<TRole> Roles => this.Context.Set<TRole>();

		/// <inheritdoc />
		public override async Task<IdentityResult> CreateAsync(TPermission permission, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			if(permission == null)
			{
				throw new ArgumentNullException(nameof(permission));
			}

			this.Context.Add(permission);
			await this.SaveChanges(cancellationToken);
			return IdentityResult.Success;
		}

		/// <inheritdoc />
		public override async Task<IdentityResult> UpdateAsync(TPermission permission, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			if(permission == null)
			{
				throw new ArgumentNullException(nameof(permission));
			}

			this.Context.Attach(permission);
			permission.ConcurrencyStamp = Guid.NewGuid().ToString();
			this.Context.Update(permission);
			try
			{
				await this.SaveChanges(cancellationToken);
			}
			catch(DbUpdateConcurrencyException)
			{
				return IdentityResult.Failed(this.ErrorDescriber.ConcurrencyFailure());
			}

			return IdentityResult.Success;
		}

		/// <inheritdoc />
		public override async Task<IdentityResult> DeleteAsync(TPermission permission, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			if(permission == null)
			{
				throw new ArgumentNullException(nameof(permission));
			}

			this.Context.Remove(permission);
			try
			{
				await this.SaveChanges(cancellationToken);
			}
			catch(DbUpdateConcurrencyException)
			{
				return IdentityResult.Failed(this.ErrorDescriber.ConcurrencyFailure());
			}

			return IdentityResult.Success;
		}

		/// <inheritdoc />
		public override Task<TPermission> FindByIdAsync(string permissionId, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			TKey roleId = this.ConvertIdFromString(permissionId);
			return this.Permissions.FirstOrDefaultAsync(u => u.Id.Equals(roleId), cancellationToken);
		}

		/// <inheritdoc />
		public override Task<TPermission> FindByNameAsync(string normalizedPermissionName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			return this.Permissions.FirstOrDefaultAsync(r => r.NormalizedName == normalizedPermissionName, cancellationToken);
		}

		/// <inheritdoc />
		public override Task<string> GetNormalizedPermissionNameAsync(TPermission permission, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			if(permission == null)
			{
				throw new ArgumentNullException(nameof(permission));
			}

			return Task.FromResult(permission.NormalizedName);
		}

		/// <inheritdoc />
		public IQueryable<TPermission> Permissions => this.Context.Set<TPermission>();

		/// <inheritdoc />
		public override async Task<IList<TPermission>> GetPermissionsInRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			if(string.IsNullOrEmpty(normalizedRoleName))
			{
				throw new ArgumentNullException(nameof(normalizedRoleName));
			}

			TRole role = await this.FindRoleAsync(normalizedRoleName, cancellationToken);

			if(role != null)
			{
				IQueryable<TPermission> query = from rolePermission in this.RolePermissions
												join user in this.Permissions on rolePermission.PermissionId equals user.Id
												where rolePermission.RoleId.Equals(role.Id)
												select user;

				return await query.ToListAsync(cancellationToken);
			}

			return new List<TPermission>();
		}


		/// <inheritdoc />
		protected override Task<TRole> FindRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
		{
			return this.Roles.SingleOrDefaultAsync(r => r.NormalizedName == normalizedRoleName, cancellationToken);
		}

		/// <summary>Saves the current store.</summary>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
		private async Task SaveChanges(CancellationToken cancellationToken)
		{
			if(this.AutoSaveChanges)
			{
				await this.Context.SaveChangesAsync(cancellationToken);
			}
		}
	}
}
