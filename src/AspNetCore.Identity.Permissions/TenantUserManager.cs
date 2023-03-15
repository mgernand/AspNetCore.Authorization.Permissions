namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     Provides the APIs for managing tenant users in a persistence store.
	/// </summary>
	/// <typeparam name="TUser">The type encapsulating a tenant user.</typeparam>
	[PublicAPI]
	public class TenantUserManager<TUser> : UserManager<TUser>, ITenantUserManager<TUser>
		where TUser : class
	{
		private readonly ITenantUserStore<TUser> tenantUserStore;

		/// <inheritdoc />
		public TenantUserManager(
			ITenantUserStore<TUser> tenantUserStore,
			IUserStore<TUser> userStore,
			IOptions<IdentityOptions> optionsAccessor,
			IPasswordHasher<TUser> passwordHasher,
			IEnumerable<IUserValidator<TUser>> userValidators,
			IEnumerable<IPasswordValidator<TUser>> passwordValidators,
			ILookupNormalizer keyNormalizer,
			IdentityErrorDescriber errors,
			IServiceProvider services,
			ILogger<TenantUserManager<TUser>> logger)
			: base(userStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
		{
			this.tenantUserStore = tenantUserStore;
		}

		/// <summary>
		///     Gets the tenant ID for the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user whose tenant name should be retrieved.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the name for the specified
		///     <paramref name="user" />.
		/// </returns>
		public virtual async Task<string> GetTenantIdAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if(user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			return await this.tenantUserStore.GetTenantIdAsync(user, this.CancellationToken);
		}

		/// <summary>
		///     Returns the Tenant ID claim value if present otherwise returns null.
		/// </summary>
		/// <param name="principal">The <see cref="ClaimsPrincipal" /> instance.</param>
		/// <returns>The Tenant ID claim value, or null if the claim is not present.</returns>
		/// <remarks>
		///     The Tenant ID claim is identified by
		///     <see cref="PermissionClaimTypes.TenantIdClaimType" />.
		/// </remarks>
		public string GetTenantId(ClaimsPrincipal principal)
		{
			this.ThrowIfDisposed();
            if (principal == null)
			{
				throw new ArgumentNullException(nameof(principal));
			}

			return principal.GetTenantId();
		}

		/// <summary>
		///     Returns the Tenant Name claim value if present otherwise returns null.
		/// </summary>
		/// <param name="principal">The <see cref="ClaimsPrincipal" /> instance.</param>
		/// <returns>The Tenant Name claim value, or null if the claim is not present.</returns>
		/// <remarks>
		///     The Tenant Name claim is identified by
		///     <see cref="PermissionClaimTypes.TenantNameClaimType" />.
		/// </remarks>
		public string GetTenantName(ClaimsPrincipal principal)
		{
			this.ThrowIfDisposed();
            if (principal == null)
			{
				throw new ArgumentNullException(nameof(principal));
			}

			return principal.GetTenantName();
		}

		/// <summary>
		///     Returns the Tenant DisplayName claim value if present otherwise returns null.
		/// </summary>
		/// <param name="principal">The <see cref="ClaimsPrincipal" /> instance.</param>
		/// <returns>The Tenant DisplayName claim value, or null if the claim is not present.</returns>
		/// <remarks>
		///     The Tenant DisplayName claim is identified by
		///     <see cref="PermissionClaimTypes.TenantDisplayNameClaimType" />.
		/// </remarks>
		public string GetTenantDisplayName(ClaimsPrincipal principal)
		{
			this.ThrowIfDisposed();
            if (principal == null)
			{
				throw new ArgumentNullException(nameof(principal));
			}

			return principal.GetTenantDisplayName();
		}
	}
}
