namespace MadEyeMatt.AspNetCore.Authorization.Permissions
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using JetBrains.Annotations;
    using ClaimsEnumerableExtensions = MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.ClaimsEnumerableExtensions;
    using ClaimsPrincipalExtensions = MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.ClaimsPrincipalExtensions;

    [UsedImplicitly]
	internal sealed class UserPermissionsService : MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService
	{
		private readonly MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IPermissionLookupNormalizer permissionLookupNormalizer;

		public UserPermissionsService(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IPermissionLookupNormalizer permissionLookupNormalizer)
		{
			this.permissionLookupNormalizer = permissionLookupNormalizer;
		}

		/// <inheritdoc />
		public IReadOnlyCollection<string> GetPermissionsFrom(ClaimsPrincipal user)
		{
			return ClaimsPrincipalExtensions.GetPermissions(user);
		}

		/// <inheritdoc />
		public IReadOnlyCollection<string> GetPermissionsFrom(IEnumerable<Claim> claims)
		{
			return ClaimsEnumerableExtensions.GetPermissions(claims);
		}

		/// <inheritdoc />
		public bool HasPermission(ClaimsPrincipal user, string permission)
		{
			string normalizedName = this.permissionLookupNormalizer.NormalizeName(permission);
			return ClaimsPrincipalExtensions.HasPermission(user, normalizedName);
		}

		/// <inheritdoc />
		public bool HasPermission(IEnumerable<Claim> claims, string permission)
		{
			string normalizedName = this.permissionLookupNormalizer.NormalizeName(permission);
			return ClaimsEnumerableExtensions.HasPermission(claims, normalizedName);
		}

		/// <inheritdoc />
		public string GetTenantId(ClaimsPrincipal user)
		{
			return ClaimsPrincipalExtensions.GetTenantId(user);
		}

		/// <inheritdoc />
		public string GetTenantName(ClaimsPrincipal user)
		{
			return ClaimsPrincipalExtensions.GetTenantName(user);
		}

		/// <inheritdoc />
		public string GetTenantDisplayName(ClaimsPrincipal user)
		{
			return ClaimsPrincipalExtensions.GetTenantDisplayName(user);
		}
	}
}
