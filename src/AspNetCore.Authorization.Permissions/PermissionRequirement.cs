namespace MadEyeMatt.AspNetCore.Authorization.Permissions
{
	using System;
	using Microsoft.AspNetCore.Authorization;

	internal sealed class PermissionRequirement : IAuthorizationRequirement
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="PermissionRequirement" /> type.
		/// </summary>
		/// <param name="permission"></param>
		public PermissionRequirement(string permission)
		{
			if(string.IsNullOrWhiteSpace(permission))
			{
				throw new ArgumentNullException(nameof(permission));
			}

			this.Permission = permission;
		}

		/// <summary>
		///     The name of the permission to check.
		/// </summary>
		public string Permission { get; }
	}
}
