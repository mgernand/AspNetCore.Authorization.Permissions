namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using System.Reflection;
    using System.Security.Claims;
    using System.Threading;
	using System.Threading.Tasks;
    using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
    ///		Extension methods for the <see cref="UserManager{T}"/> type.
    /// </summary>
    [PublicAPI]
	public static class UserManagerExtensions
	{
        /// <summary>
        ///     Gets the tenant ID for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="manager">The user manager.</param>
        /// <param name="user">The user whose tenant name should be retrieved.</param>
        /// <returns>
        ///     The <see cref="Task" /> that represents the asynchronous operation, containing the name for the specified
        ///     <paramref name="user" />.
        /// </returns>
        public static async Task<string> GetTenantIdAsync<TUser>(this UserManager<TUser> manager, TUser user) 
			where TUser : class
		{
			manager.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(user);

			PropertyInfo tokenPropertyInfo = manager.GetType().GetProperty("CancellationToken", BindingFlags.NonPublic | BindingFlags.Instance);
			CancellationToken cancellationToken = (CancellationToken)(tokenPropertyInfo?.GetValue(manager) ?? CancellationToken.None);

            PropertyInfo storePropertyInfo = manager.GetType().GetProperty("Store", BindingFlags.NonPublic | BindingFlags.Instance);
			IUserStore<TUser> store = storePropertyInfo?.GetValue(manager) as IUserStore<TUser>;

			string tenantId = await store.GetTenantIdAsync(user, cancellationToken);
			return tenantId;
		}

        ///  <summary>
        ///      Sets the given <paramref name="tenantId" /> for the specified <paramref name="user" />.
        ///  </summary>
        ///  <param name="manager">The user manager.</param>
        ///  <param name="user">The user whose tenant id should be set.</param>
        ///  <param name="tenantId">The tenant id to set.</param>
        ///  <returns>
        /// 		The <see cref="Task" /> that represents the asynchronous operation.
        ///  </returns>
        public static async Task SetTenantIdAsync<TUser>(this UserManager<TUser> manager, TUser user, string tenantId)
			where TUser : class
		{
			manager.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(user);

			PropertyInfo tokenPropertyInfo = manager.GetType().GetProperty("CancellationToken", BindingFlags.NonPublic | BindingFlags.Instance);
			CancellationToken cancellationToken = (CancellationToken)(tokenPropertyInfo?.GetValue(manager) ?? CancellationToken.None);

			PropertyInfo storePropertyInfo = manager.GetType().GetProperty("Store", BindingFlags.NonPublic | BindingFlags.Instance);
			IUserStore<TUser> store = storePropertyInfo?.GetValue(manager) as IUserStore<TUser>;

			await store.SetTenantIdAsync(user, tenantId, cancellationToken);
		}

        /// <summary>
        ///     Returns the Tenant ID claim value if present otherwise returns null.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="principal">The <see cref="ClaimsPrincipal" /> instance.</param>
        /// <returns>The Tenant ID claim value, or null if the claim is not present.</returns>
        /// <remarks>
        ///     The Tenant ID claim is identified by
        ///     <see cref="PermissionClaimTypes.TenantIdClaimType" />.
        /// </remarks>
        public static string GetTenantId<TUser>(this UserManager<TUser> manager, ClaimsPrincipal principal) 
			where TUser : class
		{
			manager.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(principal);

			return principal.GetTenantId();
		}

		/// <summary>
		///     Returns the Tenant Name claim value if present otherwise returns null.
		/// </summary>
		/// <param name="manager"></param>
		/// <param name="principal">The <see cref="ClaimsPrincipal" /> instance.</param>
		/// <returns>The Tenant Name claim value, or null if the claim is not present.</returns>
		/// <remarks>
		///     The Tenant Name claim is identified by
		///     <see cref="PermissionClaimTypes.TenantNameClaimType" />.
		/// </remarks>
		public static string GetTenantName<TUser>(this UserManager<TUser> manager, ClaimsPrincipal principal)
			where TUser : class
        {
			manager.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(principal);

			return principal.GetTenantName();
		}

		/// <summary>
		///     Returns the Tenant DisplayName claim value if present otherwise returns null.
		/// </summary>
		/// <param name="manager"></param>
		/// <param name="principal">The <see cref="ClaimsPrincipal" /> instance.</param>
		/// <returns>The Tenant DisplayName claim value, or null if the claim is not present.</returns>
		/// <remarks>
		///     The Tenant DisplayName claim is identified by
		///     <see cref="PermissionClaimTypes.TenantDisplayNameClaimType" />.
		/// </remarks>
		public static string GetTenantDisplayName<TUser>(this UserManager<TUser> manager, ClaimsPrincipal principal) 
			where TUser : class
		{
			manager.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(principal);

			return principal.GetTenantDisplayName();
		}

		private static void ThrowIfDisposed<TUser>(this UserManager<TUser> manager)
			where TUser : class
		{
			MethodInfo methodInfo = manager.GetType().GetMethod("ThrowIfDisposed", BindingFlags.NonPublic | BindingFlags.Instance);
			methodInfo?.Invoke(manager, Array.Empty<object>());
		}
    }
}
