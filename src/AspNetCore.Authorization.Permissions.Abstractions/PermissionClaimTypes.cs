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
		///     The claim type for the permissions.
		/// </summary>
		public const string PermissionClaimType = "permission";

		/// <summary>
		///     The claim type for the tenant ID.
		/// </summary>
		public const string TenantIdClaimType = "tenant-id";

		/// <summary>
		///     The claim type for the tenant name.
		/// </summary>
		public const string TenantNameClaimType = "tenant-name";

		/// <summary>
		///     The claim type for the tenant display name.
		/// </summary>
		public const string TenantDisplayNameClaimType = "tenant-display-name";
	}
}
