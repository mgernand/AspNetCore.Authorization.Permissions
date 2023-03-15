namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System.Security.Claims;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     Provides an abstraction for managing tenant users in a persistence store.
	/// </summary>
	/// <typeparam name="TUser">The type encapsulating a tenant user.</typeparam>
	[PublicAPI]
	public interface ITenantUserManager<TUser> : IUserManager<TUser>
		where TUser : class
	{
		/// <summary>
		///     Gets the tenant ID for the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user whose tenant ID should be retrieved.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the ID for the specified
		///     <paramref name="user" />.
		/// </returns>
		Task<string> GetTenantIdAsync(TUser user);

		/// <summary>
		///     Returns the Tenant ID claim value if present otherwise returns null.
		/// </summary>
		/// <param name="principal">The <see cref="T:System.Security.Claims.ClaimsPrincipal" /> instance.</param>
		/// <returns>The Tenant ID claim value, or null if the claim is not present.</returns>
		/// <remarks>
		///     The Tenant ID claim is identified by
		///     <see cref="F:MadEyeMatt.AspNetCore.Authorization.Permissions.PermissionClaimTypes.TenantIdClaimType" />.
		/// </remarks>
		string GetTenantId(ClaimsPrincipal principal);

		/// <summary>
		///     Returns the Tenant Name claim value if present otherwise returns null.
		/// </summary>
		/// <param name="principal">The <see cref="T:System.Security.Claims.ClaimsPrincipal" /> instance.</param>
		/// <returns>The Tenant Name claim value, or null if the claim is not present.</returns>
		/// <remarks>
		///     The Tenant Name claim is identified by
		///     <see cref="F:MadEyeMatt.AspNetCore.Authorization.Permissions.PermissionClaimTypes.TenantNameClaimType" />.
		/// </remarks>
		string GetTenantName(ClaimsPrincipal principal);

		/// <summary>
		///     Returns the Tenant DisplayName claim value if present otherwise returns null.
		/// </summary>
		/// <param name="principal">The <see cref="T:System.Security.Claims.ClaimsPrincipal" /> instance.</param>
		/// <returns>The Tenant DisplayName claim value, or null if the claim is not present.</returns>
		/// <remarks>
		///     The Tenant DisplayName claim is identified by
		///     <see cref="F:MadEyeMatt.AspNetCore.Authorization.Permissions.PermissionClaimTypes.TenantDisplayNameClaimType" />.
		/// </remarks>
		string GetTenantDisplayName(ClaimsPrincipal principal);
	}
}
