namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore
{
	using JetBrains.Annotations;

	/// <summary>
	///     Provides the options for the entity configurations when using the extension methods
	///     to configure the entities.
	/// </summary>
	[PublicAPI]
	public sealed class PermissionModelBuilderOptions
	{
		/// <summary>
		///     Gets or sets the name of the users table.
		/// </summary>
		public string UserTable { get; set; } = "AspNetUsers";

		/// <summary>
		///     Gets or sets the name of the user claims table.
		/// </summary>
		public string UserClaimsTable { get; set; } = "AspNetUserClaims";

		/// <summary>
		///     Gets or sets the name of the user logins table.
		/// </summary>
		public string UserLoginsTable { get; set; } = "AspNetUserLogins";

		/// <summary>
		///     Gets or sets the name of the user tokens table.
		/// </summary>
		public string UserTokensTable { get; set; } = "AspNetUserTokens";

		/// <summary>
		///     Gets or sets the name of the roles table.
		/// </summary>
		public string RolesTable { get; set; } = "AspNetRoles";

		/// <summary>
		///     Gets or sets the name of the role claims table.
		/// </summary>
		public string RoleClaimsTable { get; set; } = "AspNetRoleClaims";

		/// <summary>
		///     Gets or sets the name of the user roles table.
		/// </summary>
		public string UserRolesTable { get; set; } = "AspNetUserRoles";

		/// <summary>
		///     Gets or sets the name of the permissions table.
		/// </summary>
		public string PermissionsTable { get; set; } = "AspNetPermissions";

		/// <summary>
		///     Gets or sets the name of the role permissions table.
		/// </summary>
		public string RolePermissionsTable { get; set; } = "AspNetRolePermissions";

		/// <summary>
		///     Gets or sets the name of the tenants table.
		/// </summary>
		public string TenantsTable { get; set; } = "AspNetTenants";

		/// <summary>
		///     Gets or sets the name of the tenant roles table.
		/// </summary>
		public string TenantRolesTable { get; set; } = "AspNetTenantRoles";
	}
}
