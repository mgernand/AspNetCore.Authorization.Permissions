namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;

	[UsedImplicitly]
	internal sealed class IdentityClaimsProvider<TUser, TPermission, TTenant> : IClaimsProvider
		where TUser : class
		where TPermission : class
		where TTenant : class
	{
		private readonly ITenantUserManager<TUser> userManager;
		private readonly ITenantManager<TTenant> tenantManager;
		private readonly IPermissionManager<TPermission> permissionManager;

		/// <summary>
		///     Initializes a new instance of the <see cref="IdentityClaimsProvider{TUser, TPermission, TTenant}" /> type:
		/// </summary>
		/// <param name="userManager"></param>
		/// <param name="permissionManager"></param>
		/// <param name="tenantManager"></param>
		public IdentityClaimsProvider(
			ITenantUserManager<TUser> userManager,
			ITenantManager<TTenant> tenantManager,
			IPermissionManager<TPermission> permissionManager)
		{
			this.userManager = userManager;
			this.tenantManager = tenantManager;
			this.permissionManager = permissionManager;
		}

		/// <inheritdoc />
		public async Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
		{
			List<Claim> claims = new List<Claim>();

			// Add the permission claims that com from user roles.
			TUser user = await this.userManager.FindByIdAsync(userId);
			claims.AddRange(await this.GetUserPermissions(user));

			// Add the (optional) permission claims that come from tenant roles.
			string tenantId = await this.userManager.GetTenantIdAsync(user);
			if(!string.IsNullOrWhiteSpace(tenantId))
			{
				TTenant tenant = await this.tenantManager.FindByIdAsync(tenantId);
				claims.AddRange(await this.GetTenantPermissions(tenant));

				claims.Add(new Claim(PermissionClaimTypes.TenantIdClaimType, tenantId));

				string tenantName = await this.tenantManager.GetTenantNameAsync(tenant);
				claims.Add(new Claim(PermissionClaimTypes.TenantNameClaimType, tenantName));

				string tenantDisplayName = await this.tenantManager.GetTenantDisplayNameAsync(tenant);
				claims.Add(new Claim(PermissionClaimTypes.TenantDisplayNameClaimType, tenantDisplayName));
			}

			return claims.AsReadOnly();
		}

		private async Task<IList<Claim>> GetUserPermissions(TUser user)
		{
			IList<string> roleNames = await this.userManager.GetRolesAsync(user);
			return await this.GetPermissions(roleNames);
		}

		private async Task<IList<Claim>> GetTenantPermissions(TTenant tenant)
		{
			IList<string> roleNames = await this.tenantManager.GetRolesAsync(tenant);
			return await this.GetPermissions(roleNames);
		}

		private async Task<IList<Claim>> GetPermissions(IEnumerable<string> roleNames)
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
