namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using MadEyeMatt.Extensions.Identity.Permissions;

	internal abstract class PermissionClaimsProviderBase<TPermission> : IClaimsProvider
		where TPermission : class
	{
		private readonly PermissionManager<TPermission> permissionManager;

		/// <summary>
		///     Initializes a new instance of the <see cref="PermissionClaimsProviderBase{TPermission}" /> type:
		/// </summary>
		/// <param name="permissionManager"></param>
		protected PermissionClaimsProviderBase(
			PermissionManager<TPermission> permissionManager)
		{
			this.permissionManager = permissionManager;
		}

		/// <inheritdoc />
		public abstract Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId);

		protected async Task<IList<Claim>> GetPermissionClaims(IEnumerable<string> roleNames)
		{
			IList<Claim> claims = new List<Claim>();
			foreach(string roleName in roleNames)
			{
				IList<TPermission> permissions = await this.permissionManager.GetPermissionsInRoleAsync(roleName);
				foreach(TPermission permission in permissions)
				{
					if(permission is null)
					{
						continue;
					}

					string permissionName = await this.permissionManager.GetPermissionNameAsync(permission);
					claims.Add(new Claim(PermissionClaimTypes.PermissionClaimType, permissionName));
				}
			}

			return claims;
		}
	}
}
