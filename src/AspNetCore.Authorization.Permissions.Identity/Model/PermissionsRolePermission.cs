namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.Model
{
	using System;
	using JetBrains.Annotations;


	/// <summary>
	///     Represents the link between a role and a permission that uses a string as type for the keys.
	/// </summary>
	[PublicAPI]
	public class PermissionsRolePermission : PermissionsRolePermission<string>
	{
	}

	/// <summary>
	///     Represents the link between a role and a permission.
	/// </summary>
	/// <typeparam name="TKey">The type of the primary key used for roles and permissions.</typeparam>
	[PublicAPI]
	public class PermissionsRolePermission<TKey> where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the primary key of the role that is linked to the permission.
		/// </summary>
		public virtual TKey RoleId { get; set; }

		/// <summary>
		///     Gets or sets the primary key of the permission that is linked to the role.
		/// </summary>
		public virtual TKey PermissionId { get; set; }
	}
}
