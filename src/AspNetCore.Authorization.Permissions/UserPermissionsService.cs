namespace AspNetCore.Authorization.Permissions
{
	using System.Collections.Generic;
	using System.Security.Claims;
	using AspNetCore.Authorization.Permissions.Abstractions;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class UserPermissionsService : IUserPermissionsService
	{
		private readonly IPermissionLookupNormalizer permissionLookupNormalizer;

		public UserPermissionsService(IPermissionLookupNormalizer permissionLookupNormalizer)
		{
			this.permissionLookupNormalizer = permissionLookupNormalizer;
		}

		/// <inheritdoc />
		public IReadOnlyCollection<string> GetPermissionsFor(ClaimsPrincipal user)
		{
			return user.GetPermissions();
		}

		/// <inheritdoc />
		public bool HasPermission(ClaimsPrincipal user, string permission)
		{
			string normalizedName = this.permissionLookupNormalizer.NormalizeName(permission);
			return user.HasPermission(normalizedName);
		}
	}
}
