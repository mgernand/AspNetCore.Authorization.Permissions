namespace MadEyeMatt.Extensions.Identity.Permissions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     Provides the APIs for managing permissions in a persistence store.
	/// </summary>
	/// <typeparam name="TPermission">The type encapsulating a permission.</typeparam>
	[PublicAPI]
	public class PermissionManager<TPermission> : IDisposable
		where TPermission : class
	{
		private bool disposed;

		/// <summary>
		///     Initializes a new instance of the <see cref="PermissionManager{TPermission}" /> type.
		/// </summary>
		/// <param name="store">The persistence store the manager will operate over.</param>
		/// <param name="permissionValidators">A collection of validators for permissions.</param>
		/// <param name="keyNormalizer">The normalizer to use when normalizing permission names to keys.</param>
		/// <param name="errors">The <see cref="IdentityErrorDescriber" /> used to provider error messages.</param>
		/// <param name="logger">The logger used to log messages, warnings and errors.</param>
		public PermissionManager(
			IPermissionStore<TPermission> store,
			IEnumerable<IPermissionValidator<TPermission>> permissionValidators,
			ILookupNormalizer keyNormalizer,
			IdentityErrorDescriber errors,
			ILogger<PermissionManager<TPermission>> logger)
		{
			this.Store = store ?? throw new ArgumentNullException(nameof(store));
			this.KeyNormalizer = keyNormalizer;
			this.ErrorDescriber = errors;
			this.Logger = logger;

			if(permissionValidators != null)
			{
				foreach(IPermissionValidator<TPermission> v in permissionValidators)
				{
					this.PermissionValidators.Add(v);
				}
			}
		}

		/// <summary>
		///     Gets the <see cref="ILogger" /> used to log messages from the manager.
		/// </summary>
		/// <value>
		///     The <see cref="ILogger" /> used to log messages from the manager.
		/// </value>
		protected ILogger Logger { get; private set; }

		/// <summary>
		///     Gets the persistence store this instance operates over.
		/// </summary>
		/// <value>The persistence store this instance operates over.</value>
		protected IPermissionStore<TPermission> Store { get; private set; }

		/// <summary>
		///     The cancellation token used to cancel operations.
		/// </summary>
		protected virtual CancellationToken CancellationToken => CancellationToken.None;

		/// <summary>
		///     Gets a list of validators for permissions to call before persistence.
		/// </summary>
		/// <value>A list of validators for permissions to call before persistence.</value>
		protected IList<IPermissionValidator<TPermission>> PermissionValidators { get; } = new List<IPermissionValidator<TPermission>>();

		/// <summary>
		///     Gets the <see cref="IdentityErrorDescriber" /> used to provider error messages.
		/// </summary>
		/// <value>
		///     The <see cref="IdentityErrorDescriber" /> used to provider error messages.
		/// </value>
		protected IdentityErrorDescriber ErrorDescriber { get; private set; }

		/// <summary>
		///     Gets the normalizer to use when normalizing permission names to keys.
		/// </summary>
		/// <value>
		///     The normalizer to use when normalizing permission names to keys.
		/// </value>
		protected ILookupNormalizer KeyNormalizer { get; private set; }

		/// <summary>
		///     Gets an IQueryable collection of permissions if the persistence store is an
		///     <see cref="IQueryablePermissionStore{TPermission}" />,
		///     otherwise throws a <see cref="NotSupportedException" />.
		/// </summary>
		/// <value>
		///     An IQueryable collection of permissions if the persistence store is an
		///     <see cref="IQueryablePermissionStore{TPermission}" />.
		/// </value>
		/// <exception cref="NotSupportedException">
		///     Thrown if the persistence store is not an
		///     <see cref="IQueryablePermissionStore{TPermission}" />.
		/// </exception>
		/// <remarks>
		///     Callers to this property should use <see cref="SupportsQueryablePermissions" /> to ensure the backing permission
		///     store supports
		///     returning an IQueryable list of permissions.
		/// </remarks>
		public virtual IQueryable<TPermission> Permissions
		{
			get
			{
				if(this.Store is IQueryablePermissionStore<TPermission> queryableStore)
				{
					return queryableStore.Permissions;
				}

				throw new NotSupportedException("The permission store is not an IQueryablePermissionStore." /*Resources.StoreNotIQueryableRoleStore*/);
			}
		}

		/// <summary>
		///     Gets a flag indicating whether the underlying persistence store supports returning an <see cref="IQueryable" />
		///     collection of permissions.
		/// </summary>
		/// <value>
		///     true if the underlying persistence store supports returning an <see cref="IQueryable" /> collection of permissions,
		///     otherwise false.
		/// </value>
		public virtual bool SupportsQueryablePermissions
		{
			get
			{
				this.ThrowIfDisposed();
				return this.Store is IQueryablePermissionStore<TPermission>;
			}
		}

		/// <summary>
		///     Releases all resources used by the role manager.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		///     Creates the specified <paramref name="permission" /> in the persistence store.
		/// </summary>
		/// <param name="permission">The permission to create.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation.
		/// </returns>
		public virtual async Task<IdentityResult> CreateAsync(TPermission permission)
		{
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(permission);

			IdentityResult result = await this.ValidatePermissionAsync(permission);
			if(!result.Succeeded)
			{
				return result;
			}

			await this.UpdateNormalizedPermissionNameAsync(permission);
			result = await this.Store.CreateAsync(permission, this.CancellationToken);
			return result;
		}

		/// <summary>
		///     Updates the specified <paramref name="permission" />.
		/// </summary>
		/// <param name="permission">The permission to updated.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
		///     for the update.
		/// </returns>
		public virtual Task<IdentityResult> UpdateAsync(TPermission permission)
		{
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(permission);

            return this.UpdatePermissionAsync(permission);
		}

		/// <summary>
		///     Deletes the specified <paramref name="permission" />.
		/// </summary>
		/// <param name="permission">The permission to delete.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
		///     for the delete.
		/// </returns>
		public virtual Task<IdentityResult> DeleteAsync(TPermission permission)
		{
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(permission);

			return this.Store.DeleteAsync(permission, this.CancellationToken);
		}

		/// <summary>
		///     Gets a flag indicating whether the specified <paramref name="permissionName" /> exists.
		/// </summary>
		/// <param name="permissionName">The permission name whose existence should be checked.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing true if the permission name exists,
		///     otherwise false.
		/// </returns>
		public virtual async Task<bool> PermissionExistsAsync(string permissionName)
		{
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(permissionName);

            return await this.FindByNameAsync(permissionName) != null;
		}

		/// <summary>
		///     Updates the normalized name for the specified <paramref name="permission" />.
		/// </summary>
		/// <param name="permission">The permission whose normalized name needs to be updated.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation.
		/// </returns>
		public virtual async Task UpdateNormalizedPermissionNameAsync(TPermission permission)
		{
			string name = await this.GetPermissionNameAsync(permission);
			await this.Store.SetNormalizedPermissionNameAsync(permission, this.NormalizeName(name), this.CancellationToken);
		}

		/// <summary>
		///     Gets the name of the specified <paramref name="permission" />.
		/// </summary>
		/// <param name="permission">The permission whose name should be retrieved.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the name of the
		///     specified <paramref name="permission" />.
		/// </returns>
		public Task<string> GetPermissionNameAsync(TPermission permission)
		{
			this.ThrowIfDisposed();
			return this.Store.GetPermissionNameAsync(permission, this.CancellationToken);
		}

		/// <summary>
		///     Sets the name of the specified <paramref name="permission" />.
		/// </summary>
		/// <param name="permission">The permission whose name should be set.</param>
		/// <param name="permissionName">The name to set.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
		///     of the operation.
		/// </returns>
		public virtual async Task<IdentityResult> SetPermissionNameAsync(TPermission permission, string permissionName)
		{
			this.ThrowIfDisposed();

			await this.Store.SetPermissionNameAsync(permission, permissionName, this.CancellationToken);
			await this.UpdateNormalizedPermissionNameAsync(permission);
			return IdentityResult.Success;
		}

		/// <summary>
		///     Gets the ID of the specified <paramref name="permission" />.
		/// </summary>
		/// <param name="permission">The permission whose ID should be retrieved.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the ID of the
		///     specified <paramref name="permission" />.
		/// </returns>
		public virtual Task<string> GetPermissionIdAsync(TPermission permission)
		{
			this.ThrowIfDisposed();
			return this.Store.GetPermissionIdAsync(permission, this.CancellationToken);
		}

        /// <summary>
        ///     Gets a list of role names the specified <paramref name="permission" /> belongs to.
        /// </summary>
        /// <param name="permission">The permission whose role names to retrieve.</param>
        /// <returns>The <see cref="Task" /> that represents the asynchronous operation, containing a list of role names.</returns>
        public virtual async Task<IList<string>> GetRolesAsync(TPermission permission)
		{
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(permission);

            IRolePermissionStore<TPermission> store = this.GetRolePermissionStore();
			return await store.GetRolesAsync(permission, this.CancellationToken);
		}

		/// <summary>
		///     Gets a list of role ids the specified <paramref name="permission" /> belongs to.
		/// </summary>
		/// <param name="permission">The permission whose role ids to retrieve.</param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation, containing a list of role ids.</returns>
		public virtual async Task<IList<string>> GetRoleIdsAsync(TPermission permission)
		{
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(permission);

			IRolePermissionStore<TPermission> store = this.GetRolePermissionStore();
			return await store.GetRoleIdsAsync(permission, this.CancellationToken);
		}

        /// <summary>
        ///     Finds the role associated with the specified <paramref name="permissionId" /> if any.
        /// </summary>
        /// <param name="permissionId">The permission ID whose permission should be returned.</param>
        /// <returns>
        ///     The <see cref="Task" /> that represents the asynchronous operation, containing the permission
        ///     associated with the specified <paramref name="permissionId" />
        /// </returns>
        public Task<TPermission> FindByIdAsync(string permissionId)
		{
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(permissionId);

			return this.Store.FindByIdAsync(permissionId, this.CancellationToken);
		}

        /// <summary>
        ///     Finds the role associated with the specified <paramref name="permissionName" /> if any.
        /// </summary>
        /// <param name="permissionName">The permission name whose permission should be returned.</param>
        /// <returns>
        ///     The <see cref="Task" /> that represents the asynchronous operation, containing the permission
        ///     associated with the specified <paramref name="permissionName" />
        /// </returns>
        public Task<TPermission> FindByNameAsync(string permissionName)
		{
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(permissionName);

            return this.Store.FindByNameAsync(this.NormalizeName(permissionName), this.CancellationToken);
		}

		/// <summary>
		///     Returns a list of permissions from the permission store who are members of the specified
		///     <paramref name="roleName" />.
		/// </summary>
		/// <param name="roleName">The name of the role whose permission should be returned.</param>
		/// <returns>
		///     A <see cref="Task{TResult}" /> that represents the result of the asynchronous query, a list of
		///     <typeparamref name="TPermission" />s who
		///     are members of the specified role.
		/// </returns>
		public virtual Task<IList<TPermission>> GetPermissionsInRoleAsync(string roleName)
		{
			this.ThrowIfDisposed();
			IRolePermissionStore<TPermission> store = this.GetRolePermissionStore();
			ArgumentNullException.ThrowIfNull(roleName);

            return store.GetPermissionsInRoleAsync(this.NormalizeName(roleName), this.CancellationToken);
		}

		/// <summary>
		///     Returns the users permission claim values if present otherwise returns null.
		/// </summary>
		/// <param name="principal">The <see cref="ClaimsPrincipal" /> instance.</param>
		/// <returns>The users permission claim values, or empty list if the claim is not present.</returns>
		/// <remarks>
		///     The users permission claims are identified by
		///     <see cref="PermissionClaimTypes.PermissionClaimType" />.
		/// </remarks>
		public IList<string> GetPermissions(ClaimsPrincipal principal)
		{
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(principal);

            return principal.GetPermissions().ToList();
		}

		/// <summary>
		///     Gets a normalized representation of the specified <paramref name="permissionName" />.
		/// </summary>
		/// <param name="permissionName">The value to normalize.</param>
		/// <returns>A normalized representation of the specified <paramref name="permissionName" />.</returns>
		public virtual string NormalizeName(string permissionName)
		{
			return this.KeyNormalizer == null ? permissionName : this.KeyNormalizer.NormalizeName(permissionName);
		}

		/// <summary>
		///     Called to update the permission after validating and updating the normalized permission name.
		/// </summary>
		/// <param name="permission">The permission.</param>
		/// <returns>Whether the operation was successful.</returns>
		protected virtual async Task<IdentityResult> UpdatePermissionAsync(TPermission permission)
		{
			IdentityResult result = await this.ValidatePermissionAsync(permission);
			if(!result.Succeeded)
			{
				return result;
			}

			await this.UpdateNormalizedPermissionNameAsync(permission);
			return await this.Store.UpdateAsync(permission, this.CancellationToken);
		}

		/// <summary>
		///     Should return <see cref="IdentityResult.Success" /> if validation is successful. This is
		///     called before saving the permission via Create or Update.
		/// </summary>
		/// <param name="permission">The permission</param>
		/// <returns>A <see cref="IdentityResult" /> representing whether validation was successful.</returns>
		protected virtual async Task<IdentityResult> ValidatePermissionAsync(TPermission permission)
		{
			List<IdentityError> errors = new List<IdentityError>();
			foreach(IPermissionValidator<TPermission> validator in this.PermissionValidators)
			{
				IdentityResult result = await validator.ValidateAsync(this, permission);
				if(!result.Succeeded)
				{
					errors.AddRange(result.Errors);
				}
			}

			if(errors.Count > 0)
			{
				this.Logger.LogWarning(
					new EventId(0, "PermissionValidationFailed"),
					"Permission {permissionId} validation failed: {errors}.", await this.GetPermissionIdAsync(permission), string.Join(";", errors.Select(e => e.Code)));
				return IdentityResult.Failed(errors.ToArray());
			}

			return IdentityResult.Success;
		}

		/// <summary>
		///     Releases the unmanaged resources used by the role manager and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">
		///     true to release both managed and unmanaged resources; false to release only unmanaged
		///     resources.
		/// </param>
		protected virtual void Dispose(bool disposing)
		{
			if(disposing && !this.disposed)
			{
				this.Store.Dispose();
			}

			this.disposed = true;
		}

		/// <summary>
		///     Throws if this class has been disposed.
		/// </summary>
		protected void ThrowIfDisposed()
		{
			if(this.disposed)
			{
				throw new ObjectDisposedException(this.GetType().Name);
			}
		}

		private IRolePermissionStore<TPermission> GetRolePermissionStore()
		{
			if(this.Store is IRolePermissionStore<TPermission> cast)
			{
				return cast;
			}

			throw new NotSupportedException("The permission store was not a IRolePermissionStore.");
		}
	}
}
