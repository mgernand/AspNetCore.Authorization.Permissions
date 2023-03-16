namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     Provides the APIs for managing tenants in a persistence store.
	/// </summary>
	/// <typeparam name="TTenant">The type encapsulating a tenant.</typeparam>
	[PublicAPI]
	public class TenantManager<TTenant> : ITenantManager<TTenant>
		where TTenant : class
	{
		private bool disposed;

		/// <summary>
		///     Initializes a new instance of the <see cref="TenantManager{TTenant}" /> type.
		/// </summary>
		/// <param name="store">The persistence store the manager will operate over.</param>
		/// <param name="tenantValidators">A collection of validators for tenants.</param>
		/// <param name="keyNormalizer">The normalizer to use when normalizing tenant names to keys.</param>
		/// <param name="errors">The <see cref="IdentityErrorDescriber" /> used to provider error messages.</param>
		/// <param name="logger">The logger used to log messages, warnings and errors.</param>
		public TenantManager(
			ITenantStore<TTenant> store,
			IEnumerable<ITenantValidator<TTenant>> tenantValidators,
			ILookupNormalizer keyNormalizer,
			IdentityErrorDescriber errors,
			ILogger<TenantManager<TTenant>> logger)
		{
			this.Store = store ?? throw new ArgumentNullException(nameof(store));
			this.KeyNormalizer = keyNormalizer;
			this.ErrorDescriber = errors;
			this.Logger = logger;

			if(tenantValidators != null)
			{
				foreach(ITenantValidator<TTenant> v in tenantValidators)
				{
					this.TenantValidators.Add(v);
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
		protected ITenantStore<TTenant> Store { get; private set; }

		/// <summary>
		///     The cancellation token used to cancel operations.
		/// </summary>
		protected virtual CancellationToken CancellationToken => CancellationToken.None;

		/// <summary>
		///     Gets a list of validators for tenants to call before persistence.
		/// </summary>
		/// <value>A list of validators for tenants to call before persistence.</value>
		public IList<ITenantValidator<TTenant>> TenantValidators { get; } = new List<ITenantValidator<TTenant>>();

		/// <summary>
		///     Gets the <see cref="IdentityErrorDescriber" /> used to provider error messages.
		/// </summary>
		/// <value>
		///     The <see cref="IdentityErrorDescriber" /> used to provider error messages.
		/// </value>
		protected IdentityErrorDescriber ErrorDescriber { get; private set; }

		/// <summary>
		///     Gets the normalizer to use when normalizing tenant names to keys.
		/// </summary>
		/// <value>
		///     The normalizer to use when normalizing tenant names to keys.
		/// </value>
		protected ILookupNormalizer KeyNormalizer { get; private set; }

		/// <summary>
		///     Gets an IQueryable collection of tenants if the persistence store is an
		///     <see cref="IQueryableTenantStore{TTenant}" />,
		///     otherwise throws a <see cref="NotSupportedException" />.
		/// </summary>
		/// <value>
		///     An IQueryable collection of tenants if the persistence store is an
		///     <see cref="IQueryableTenantStore{TTenant}" />.
		/// </value>
		/// <exception cref="NotSupportedException">
		///     Thrown if the persistence store is not an
		///     <see cref="IQueryableTenantStore{TTenant}" />.
		/// </exception>
		/// <remarks>
		///     Callers to this property should use <see cref="SupportsQueryableTenants" /> to ensure the backing permission
		///     store supports
		///     returning an IQueryable list of permissions.
		/// </remarks>
		public virtual IQueryable<TTenant> Tenants
		{
			get
			{
				if(this.Store is IQueryableTenantStore<TTenant> queryableStore)
				{
					return queryableStore.Tenants;
				}

				throw new NotSupportedException("The tenant store is not an IQueryableTenantStore." /*Resources.StoreNotIQueryableRoleStore*/);
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
		public virtual bool SupportsQueryableTenants
		{
			get
			{
				this.ThrowIfDisposed();
				return this.Store is IQueryableTenantStore<TTenant>;
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
		///     Creates the specified <paramref name="tenant" /> in the persistence store.
		/// </summary>
		/// <param name="tenant">The tenant to create.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation.
		/// </returns>
		public virtual async Task<IdentityResult> CreateAsync(TTenant tenant)
		{
			this.ThrowIfDisposed();
			if(tenant == null)
			{
				throw new ArgumentNullException(nameof(tenant));
			}

			IdentityResult result = await this.ValidateTenantAsync(tenant);
			if(!result.Succeeded)
			{
				return result;
			}

			await this.UpdateNormalizedTenantNameAsync(tenant);
			result = await this.Store.CreateAsync(tenant, this.CancellationToken);
			return result;
		}

		/// <summary>
		///     Updates the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant to updated.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
		///     for the update.
		/// </returns>
		public virtual Task<IdentityResult> UpdateAsync(TTenant tenant)
		{
			this.ThrowIfDisposed();
			if(tenant == null)
			{
				throw new ArgumentNullException(nameof(tenant));
			}

			return this.UpdateTenantAsync(tenant);
		}

		/// <summary>
		///     Deletes the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant to delete.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
		///     for the delete.
		/// </returns>
		public virtual Task<IdentityResult> DeleteAsync(TTenant tenant)
		{
			this.ThrowIfDisposed();
			if(tenant == null)
			{
				throw new ArgumentNullException(nameof(tenant));
			}

			return this.Store.DeleteAsync(tenant, this.CancellationToken);
		}

		/// <summary>
		///     Gets a flag indicating whether the specified <paramref name="tenantName" /> exists.
		/// </summary>
		/// <param name="tenantName">The tenant name whose existence should be checked.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing true if the permission name exists,
		///     otherwise false.
		/// </returns>
		public virtual async Task<bool> TenantExistsAsync(string tenantName)
		{
			this.ThrowIfDisposed();
			if(tenantName == null)
			{
				throw new ArgumentNullException(nameof(tenantName));
			}

			return await this.FindByNameAsync(tenantName) != null;
		}

		/// <summary>
		///     Finds the role associated with the specified <paramref name="tenantId" /> if any.
		/// </summary>
		/// <param name="tenantId">The tenant ID whose tenant should be returned.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the permission
		///     associated with the specified <paramref name="tenantId" />
		/// </returns>
		public Task<TTenant> FindByIdAsync(string tenantId)
		{
			this.ThrowIfDisposed();
			if(tenantId == null)
			{
				throw new ArgumentNullException(nameof(tenantId));
			}

			return this.Store.FindByIdAsync(tenantId, this.CancellationToken);
		}

		/// <summary>
		///     Finds the role associated with the specified <paramref name="tenantName" /> if any.
		/// </summary>
		/// <param name="tenantName">The tenant ID whose tenant should be returned.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the permission
		///     associated with the specified <paramref name="tenantName" />
		/// </returns>
		public Task<TTenant> FindByNameAsync(string tenantName)
		{
			this.ThrowIfDisposed();
			if(tenantName == null)
			{
				throw new ArgumentNullException(nameof(tenantName));
			}

			return this.Store.FindByNameAsync(this.NormalizeName(tenantName), this.CancellationToken);
		}

		/// <summary>
		///     Updates the normalized name for the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The permission whose normalized name needs to be updated.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation.
		/// </returns>
		public virtual async Task UpdateNormalizedTenantNameAsync(TTenant tenant)
		{
			string name = await this.GetTenantIdAsync(tenant);
			await this.Store.SetNormalizedTenantNameAsync(tenant, this.NormalizeName(name), this.CancellationToken);
		}

		/// <summary>
		///     Gets the ID of the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant whose ID should be retrieved.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the ID of the
		///     specified <paramref name="tenant" />.
		/// </returns>
		public virtual Task<string> GetTenantIdAsync(TTenant tenant)
		{
			this.ThrowIfDisposed();
			return this.Store.GetTenantIdAsync(tenant, this.CancellationToken);
		}

		/// <summary>
		///     Gets the name of the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant whose name should be retrieved.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the ID of the
		///     specified <paramref name="tenant" />.
		/// </returns>
		public virtual Task<string> GetTenantNameAsync(TTenant tenant)
		{
			this.ThrowIfDisposed();
			return this.Store.GetTenantNameAsync(tenant, this.CancellationToken);
		}

		/// <summary>
		///     Gets the display name of the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant whose display name should be retrieved.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the ID of the
		///     specified <paramref name="tenant" />.
		/// </returns>
		public virtual Task<string> GetTenantDisplayNameAsync(TTenant tenant)
		{
			this.ThrowIfDisposed();
			return this.Store.GetTenantDisplayNameAsync(tenant, this.CancellationToken);
		}

		/// <summary>
		///     Sets the name of the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant whose name should be set.</param>
		/// <param name="tenantName">The name to set.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
		///     of the operation.
		/// </returns>
		public virtual async Task<IdentityResult> SetTenantNameAsync(TTenant tenant, string tenantName)
		{
			this.ThrowIfDisposed();

			await this.Store.SetTenantNameAsync(tenant, tenantName, this.CancellationToken);
			await this.UpdateNormalizedTenantNameAsync(tenant);
			return IdentityResult.Success;
		}

		/// <summary>
		///     Gets a list of role names the specified <paramref name="tenant" /> belongs to.
		/// </summary>
		/// <param name="tenant">The tenant whose role names to retrieve.</param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation, containing a list of role names.</returns>
		public virtual async Task<IList<string>> GetRolesAsync(TTenant tenant)
		{
			this.ThrowIfDisposed();
			ITenantRoleStore<TTenant> store = this.GetTenantRoleStore();
			if(tenant == null)
			{
				throw new ArgumentNullException(nameof(tenant));
			}

			return await store.GetRolesAsync(tenant, this.CancellationToken);
		}

		/// <summary>
		///     Gets a normalized representation of the specified <paramref name="tenantName" />.
		/// </summary>
		/// <param name="tenantName">The value to normalize.</param>
		/// <returns>A normalized representation of the specified <paramref name="tenantName" />.</returns>
		public virtual string NormalizeName(string tenantName)
		{
			return this.KeyNormalizer == null ? tenantName : this.KeyNormalizer.NormalizeName(tenantName);
		}

		/// <summary>
		///     Called to update the tenant after validating and updating the normalized tenant name.
		/// </summary>
		/// <param name="tenant">The tenant.</param>
		/// <returns>Whether the operation was successful.</returns>
		protected virtual async Task<IdentityResult> UpdateTenantAsync(TTenant tenant)
		{
			IdentityResult result = await this.ValidateTenantAsync(tenant);
			if(!result.Succeeded)
			{
				return result;
			}

			await this.UpdateNormalizedTenantNameAsync(tenant);
			return await this.Store.UpdateAsync(tenant, this.CancellationToken);
		}

		/// <summary>
		///     Should return <see cref="IdentityResult.Success" /> if validation is successful. This is
		///     called before saving the permission via Create or Update.
		/// </summary>
		/// <param name="tenant">The tenant</param>
		/// <returns>A <see cref="IdentityResult" /> representing whether validation was successful.</returns>
		protected virtual async Task<IdentityResult> ValidateTenantAsync(TTenant tenant)
		{
			List<IdentityError> errors = new List<IdentityError>();
			foreach(ITenantValidator<TTenant> v in this.TenantValidators)
			{
				IdentityResult result = await v.ValidateAsync(this, tenant);
				if(!result.Succeeded)
				{
					errors.AddRange(result.Errors);
				}
			}

			if(errors.Count > 0)
			{
				this.Logger.LogWarning(
					new EventId(0, "TenantValidationFailed"),
					"Permission {permissionId} validation failed: {errors}.", await this.GetTenantIdAsync(tenant), string.Join(";", errors.Select(e => e.Code)));
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

		private ITenantRoleStore<TTenant> GetTenantRoleStore()
		{
			if(this.Store is not ITenantRoleStore<TTenant> cast)
			{
				throw new NotSupportedException("The store was not a ITenantRoleStore");
			}

			return cast;
		}
	}
}
