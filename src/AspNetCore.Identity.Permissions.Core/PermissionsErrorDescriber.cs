namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.Permissions.Properties;
	using Microsoft.AspNetCore.Identity;

    /// <summary>
    ///     Service to enable localization for application facing identity errors.
    ///     https://stackoverflow.com/a/53587825
    ///     https://github.com/aspnet/AspNetIdentity/blob/master/src/Microsoft.AspNet.Identity.Core/IdentityErrorMessages.resx
    /// </summary>
    /// <seealso cref="IdentityErrorDescriber" />
    /// <remarks>
    ///     These errors are returned to controllers and are generally used as display messages to end users.
    /// </remarks>
	[PublicAPI]
	public class PermissionsErrorDescriber : IdentityErrorDescriber
	{
        /// <summary>
        ///     Returns an <see cref="IdentityError" /> indicating the specified <paramref name="permissionName" /> name is invalid.
        /// </summary>
		/// <param name="permissionName">The invalid permission.</param>
        /// <returns>An <see cref="IdentityError" /> indicating the specific role <paramref name="permissionName" /> name is invalid.</returns>
        public virtual IdentityError InvalidPermissionName(string permissionName)
        {
            return new IdentityError
            {
                Code = nameof(InvalidPermissionName),
                Description = string.Format(Resources.InvalidPermissionName, permissionName)
            };
        }

        /// <summary>
        ///     Returns an <see cref="IdentityError" /> indicating the specified <paramref name="permissionName" /> already
        ///     exists.
        /// </summary>
        /// <param name="permissionName">The duplicate permission name.</param>
        /// <returns>
        ///     An <see cref="IdentityError" /> indicating the specific role <paramref name="permissionName" /> already
        ///     exists.
        /// </returns>
        public virtual IdentityError DuplicatePermissionName(string permissionName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicatePermissionName),
				Description = string.Format(Resources.DuplicatePermissionName, permissionName)
			};
        }

        /// <summary>
        ///     Returns an <see cref="IdentityError" /> indicating the specified <paramref name="tenantName" /> already
        ///     exists.
        /// </summary>
        /// <param name="tenantName">The duplicate tenant name.</param>
        /// <returns>
        ///     An <see cref="IdentityError" /> indicating the specific role <paramref name="tenantName" /> already
        ///     exists.
        /// </returns>
        public virtual IdentityError DuplicateTenantName(string tenantName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateTenantName),
				Description = string.Format(Resources.DuplicateTenantName, tenantName)
			};
        }

        /// <summary>
        ///     Returns an <see cref="IdentityError" /> indicating the specified tenant <paramref name="tenantName" /> is invalid.
        /// </summary>
        /// <param name="tenantName">The tenant name that is invalid.</param>
        /// <returns>An <see cref="IdentityError" /> indicating the specified user <paramref name="tenantName" /> is invalid.</returns>
        public virtual IdentityError InvalidTenantName(string tenantName)
        {
            return new IdentityError
            {
                Code = nameof(InvalidTenantName),
				Description = string.Format(Resources.InvalidTenantName, tenantName)
			};
        }
    }
}
