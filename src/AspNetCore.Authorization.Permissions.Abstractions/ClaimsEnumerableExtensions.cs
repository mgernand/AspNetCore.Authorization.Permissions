namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for <see cref="IEnumerable{T}" /> where T is <see cref="Claim" />.
	/// </summary>
	[PublicAPI]
	public static class ClaimsEnumerableExtensions
	{
		/// <summary>
		///     Gets the permission from the given user claims.
		/// </summary>
		/// <param name="claims"></param>
		/// <returns></returns>
		public static IReadOnlyCollection<string> GetPermissions(this IEnumerable<Claim> claims)
		{
			return claims
				.Where(x => x.Type == PermissionClaimTypes.PermissionClaimType)
				.Select(x => x.Value)
				.ToList()
				.AsReadOnly();
		}

		/// <summary>
		///     Checks if the user claims has the given permission.
		/// </summary>
		/// <param name="claims"></param>
		/// <param name="permission"></param>
		/// <returns></returns>
		public static bool HasPermission(this IEnumerable<Claim> claims, string permission)
		{
			return claims
				.Where(x => x.Type == PermissionClaimTypes.PermissionClaimType)
				.Any(x => x.Value.Equals(permission.Normalize(), StringComparison.InvariantCultureIgnoreCase));
		}

		/// <summary>
		///     Gets the user ID from the given user claims.
		/// </summary>
		/// <param name="claims"></param>
		/// <returns></returns>
		public static string GetUserId(this IEnumerable<Claim> claims)
		{
			return claims?.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
		}

		/// <summary>
		///     Gets the tenant ID from the given user claims.
		/// </summary>
		/// <param name="claims"></param>
		/// <returns></returns>
		public static string GetTenantId(this IEnumerable<Claim> claims)
		{
			return claims?.SingleOrDefault(x => x.Type == PermissionClaimTypes.TenantIdClaimType)?.Value;
		}

		/// <summary>
		///     Gets the tenant name from the given user claims.
		/// </summary>
		/// <param name="claims"></param>
		/// <returns></returns>
		public static string GetTenantName(this IEnumerable<Claim> claims)
		{
			return claims?.SingleOrDefault(x => x.Type == PermissionClaimTypes.TenantNameClaimType)?.Value;
		}

		/// <summary>
		///     Gets the tenant display name from the given user claims.
		/// </summary>
		/// <param name="claims"></param>
		/// <returns></returns>
		public static string GetTenantDisplayName(this IEnumerable<Claim> claims)
		{
			return claims?.SingleOrDefault(x => x.Type == PermissionClaimTypes.TenantDisplayNameClaimType)?.Value;
		}
	}
}
