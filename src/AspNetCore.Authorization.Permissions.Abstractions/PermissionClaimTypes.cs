namespace AspNetCore.Authorization.Permissions.Abstractions
{
	using JetBrains.Annotations;

	/// <summary>
	///     The available user claim types.
	/// </summary>
	[PublicAPI]
	public static class PermissionClaimTypes
	{
		/// <summary>
		///     The claim name of the permissions.
		/// </summary>
		public const string PermissionClaimType = "permission";

		/// <summary>
		///     The claim name of the tenant name.
		/// </summary>
		public const string TenantNameClaimType = "tenant-name";
	}
}
