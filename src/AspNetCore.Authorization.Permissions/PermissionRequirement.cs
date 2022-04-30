namespace AspNetCore.Authorization.Permissions
{
	using Fluxera.Guards;
	using Microsoft.AspNetCore.Authorization;

	internal sealed class PermissionRequirement : IAuthorizationRequirement
	{
		/// <summary>
		///     Creates a new instance of the <see cref="PermissionRequirement" /> type.
		/// </summary>
		/// <param name="permission"></param>
		public PermissionRequirement(string permission)
		{
			this.Permission = Guard.Against.NullOrWhiteSpace(permission, nameof(permission));
		}

		/// <summary>
		///     The name of the permission to check.
		/// </summary>
		public string Permission { get; }
	}
}
