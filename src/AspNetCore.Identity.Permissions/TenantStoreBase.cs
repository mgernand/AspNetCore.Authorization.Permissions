﻿namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.Permissions.Model;
	using Microsoft.AspNetCore.Identity;

    /// <summary>
    ///     Initializes a new instance of a persistence store for tenants.
    /// </summary>
    /// <typeparam name="TTenant">The type of the class representing a tenant.</typeparam>
    /// <typeparam name="TRole">The type representing a role.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for a tenant.</typeparam>
    /// <typeparam name="TTenantRole">The type representing a tenant role.</typeparam>
    [PublicAPI]
	public abstract class TenantStoreBase<TTenant, TRole, TKey, TTenantRole> : ITenantRoleStore<TTenant>
		where TTenant : PermissionsTenant<TKey>
		where TRole : IdentityRole<TKey>
		where TKey : IEquatable<TKey>
		where TTenantRole : IdentityTenantRole<TKey>, new()
	{
		private bool disposed;

		/// <summary>
		///     Constructs a new instance of <see cref="TenantStoreBase{TTenant,TRole,TKey,TTenantRole}" />.
		/// </summary>
		/// <param name="describer">The <see cref="IdentityErrorDescriber" />.</param>
		protected TenantStoreBase(IdentityErrorDescriber describer)
		{
			this.ErrorDescriber = describer ?? throw new ArgumentNullException(nameof(describer));
		}

		/// <summary>
		///     Gets or sets the <see cref="IdentityErrorDescriber" /> for any error that occurred with the current operation.
		/// </summary>
		public IdentityErrorDescriber ErrorDescriber { get; set; }

		/// <summary>
		///     Dispose the stores
		/// </summary>
		public void Dispose()
		{
			this.disposed = true;
		}

		/// <inheritdoc />
		public abstract Task<IdentityResult> CreateAsync(TTenant tenant, CancellationToken cancellationToken);

		/// <inheritdoc />
		public abstract Task<IdentityResult> UpdateAsync(TTenant tenant, CancellationToken cancellationToken);

		/// <inheritdoc />
		public abstract Task<IdentityResult> DeleteAsync(TTenant tenant, CancellationToken cancellationToken);

		/// <inheritdoc />
		public virtual Task<string> GetTenantIdAsync(TTenant tenant, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			if(tenant == null)
			{
				throw new ArgumentNullException(nameof(tenant));
			}

			return Task.FromResult(this.ConvertIdToString(tenant.Id));
		}

		/// <inheritdoc />
		public virtual Task<string> GetTenantNameAsync(TTenant tenant, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			if(tenant == null)
			{
				throw new ArgumentNullException(nameof(tenant));
			}

			return Task.FromResult(tenant.Name);
		}

		/// <inheritdoc />
		public virtual Task<string> GetNormalizedTenantNameAsync(TTenant tenant, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			if(tenant == null)
			{
				throw new ArgumentNullException(nameof(tenant));
			}

			return Task.FromResult(tenant.NormalizedName);
		}

		/// <inheritdoc />
		public virtual Task<string> GetTenantDisplayNameAsync(TTenant tenant, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			if(tenant == null)
			{
				throw new ArgumentNullException(nameof(tenant));
			}

			return Task.FromResult(tenant.DisplayName);
		}

		/// <inheritdoc />
		public virtual Task SetTenantNameAsync(TTenant tenant, string tenantName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			if(tenant == null)
			{
				throw new ArgumentNullException(nameof(tenant));
			}

			tenant.Name = tenantName;
			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public virtual Task SetNormalizedTenantNameAsync(TTenant tenant, string normalizedTenantName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			if(tenant == null)
			{
				throw new ArgumentNullException(nameof(tenant));
			}

			tenant.NormalizedName = normalizedTenantName;
			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public virtual Task SetTenantDisplayNameAsync(TTenant tenant, string tenantDisplayName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			if(tenant == null)
			{
				throw new ArgumentNullException(nameof(tenant));
			}

			tenant.Name = tenantDisplayName;
			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public abstract Task<TTenant> FindByIdAsync(string tenantId, CancellationToken cancellationToken);

		/// <inheritdoc />
		public abstract Task<TTenant> FindByNameAsync(string normalizedTenantName, CancellationToken cancellationToken);

		/// <inheritdoc />
		public abstract Task AddToRoleAsync(TTenant tenant, string roleName, CancellationToken cancellationToken);

		/// <inheritdoc />
		public abstract Task RemoveFromRoleAsync(TTenant tenant, string roleName, CancellationToken cancellationToken);

		/// <inheritdoc />
		public abstract Task<IList<string>> GetRolesAsync(TTenant tenant, CancellationToken cancellationToken);

		/// <inheritdoc />
		public abstract Task<bool> IsInRoleAsync(TTenant tenant, string roleName, CancellationToken cancellationToken);

		/// <inheritdoc />
		public abstract Task<IList<TTenant>> GetTenantsInRoleAsync(string roleName, CancellationToken cancellationToken);

		/// <summary>
		///     Return a role with the normalized name if it exists.
		/// </summary>
		/// <param name="normalizedRoleName">The normalized role name.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>The role if it exists.</returns>
		protected abstract Task<TRole> FindRoleAsync(string normalizedRoleName, CancellationToken cancellationToken);

		/// <summary>
		///     Return a user role for the tenantId and roleId if it exists.
		/// </summary>
		/// <param name="tenantId">The tenant's id.</param>
		/// <param name="roleId">The role's id.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>The user role if it exists.</returns>
		protected abstract Task<TTenantRole> FindTenantRoleAsync(TKey tenantId, TKey roleId, CancellationToken cancellationToken);

		/// <summary>
		///     Return a user with the matching tenantId if it exists.
		/// </summary>
		/// <param name="tenantId">The tenant's id.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>The user if it exists.</returns>
		protected abstract Task<TTenant> FindTenantAsync(TKey tenantId, CancellationToken cancellationToken);

		/// <summary>
		///     Called to create a new instance of a <see cref="IdentityTenantRole{TKey}" />.
		/// </summary>
		/// <param name="tenant">The associated tenant.</param>
		/// <param name="role">The associated role.</param>
		/// <returns></returns>
		protected virtual TTenantRole CreateTenantRole(TTenant tenant, TRole role)
		{
			return new TTenantRole
			{
				TenantId = tenant.Id,
				RoleId = role.Id
			};
		}

		/// <summary>
		///     Converts the provided <paramref name="id" /> to a strongly typed key object.
		/// </summary>
		/// <param name="id">The id to convert.</param>
		/// <returns>An instance of <typeparamref name="TKey" /> representing the provided <paramref name="id" />.</returns>
		protected virtual TKey ConvertIdFromString(string id)
		{
			if(id == null)
			{
				return default;
			}

			return (TKey)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(id);
		}

		/// <summary>
		///     Converts the provided <paramref name="id" /> to its string representation.
		/// </summary>
		/// <param name="id">The id to convert.</param>
		/// <returns>An <see cref="string" /> representation of the provided <paramref name="id" />.</returns>
		protected virtual string ConvertIdToString(TKey id)
		{
			return id.Equals(default) ? null : id.ToString();
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
	}
}