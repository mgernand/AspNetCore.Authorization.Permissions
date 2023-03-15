namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Extensions for the <see cref="IdentityErrorDescriber" /> type.
	/// </summary>
	public static class IdentityErrorDescriberExtensions
	{
		/// <summary>
		///     Returns an <see cref="IdentityError" /> indicating the specified <paramref name="permissionName" /> name is invalid.
		/// </summary>
		/// <param name="describer"></param>
		/// <param name="permissionName">The invalid permission.</param>
		/// <returns>An <see cref="IdentityError" /> indicating the specific role <paramref name="permissionName" /> name is invalid.</returns>
		public static IdentityError InvalidPermissionName(this IdentityErrorDescriber describer, string permissionName)
		{
			return new IdentityError
			{
				Code = nameof(InvalidPermissionName),
				Description = $"Invalid permission name '{permissionName}'." //Resources.FormatInvalidRoleName(role)
			};
		}

		/// <summary>
		///     Returns an <see cref="IdentityError" /> indicating the specified <paramref name="permissionName" /> already
		///     exists.
		/// </summary>
		/// <param name="describer"></param>
		/// <param name="permissionName">The duplicate permission name.</param>
		/// <returns>
		///     An <see cref="IdentityError" /> indicating the specific role <paramref name="permissionName" /> already
		///     exists.
		/// </returns>
		public static IdentityError DuplicatePermissionName(this IdentityErrorDescriber describer, string permissionName)
		{
			return new IdentityError
			{
				Code = nameof(DuplicatePermissionName),
				Description = $"Duplicate permission name '{permissionName}'." //Resources.FormatDuplicatePermissionName(role)
			};
		}

		/// <summary>
		///     Returns an <see cref="IdentityError" /> indicating the specified <paramref name="tenantName" /> already
		///     exists.
		/// </summary>
		/// <param name="describer"></param>
		/// <param name="tenantName">The duplicate tenant name.</param>
		/// <returns>
		///     An <see cref="IdentityError" /> indicating the specific role <paramref name="tenantName" /> already
		///     exists.
		/// </returns>
		public static IdentityError DuplicateTenantName(this IdentityErrorDescriber describer, string tenantName)
		{
			return new IdentityError
			{
				Code = nameof(DuplicateTenantName),
				Description = $"Duplicate tenant name '{tenantName}'." //Resources.FormatDuplicatePermissionName(role)
			};
		}

		/// <summary>
		///     Returns an <see cref="IdentityError" /> indicating the specified tenant <paramref name="tenantName" /> is invalid.
		/// </summary>
		/// <param name="describer"></param>
		/// <param name="tenantName">The tenant name that is invalid.</param>
		/// <returns>An <see cref="IdentityError" /> indicating the specified user <paramref name="tenantName" /> is invalid.</returns>
		public static IdentityError InvalidTenantName(this IdentityErrorDescriber describer, string tenantName)
		{
			return new IdentityError
			{
				Code = nameof(InvalidTenantName),
				Description = $"Invalid tenant name '{tenantName}'." //Resources.FormatInvalidUserName(userName)
			};
		}
	}
}
