namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Identity
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    [PublicAPI]
	internal sealed class IdentityClaimsProvider<TUser, TPermission, TTenant> : MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider
		where TUser : class, IUser
		where TPermission : class, IPermission
		where TTenant : class, ITenant
	{
		private readonly PermissionManager<TPermission> permissionManager;
		private readonly TenantManager<TTenant> tenantManager;
		private readonly PermissionsUserManager<TUser> userManager;

		/// <summary>
		///     Creates a new instance of the <see cref="IdentityClaimsProvider{TUser, TPermission, TTenant}" /> type:
		/// </summary>
		/// <param name="userManager"></param>
		/// <param name="permissionManager"></param>
		/// <param name="tenantManager"></param>
		public IdentityClaimsProvider(
			PermissionsUserManager<TUser> userManager,
			TenantManager<TTenant> tenantManager,
			PermissionManager<TPermission> permissionManager)
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

				claims.Add(new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.TenantIdClaimType, tenantId));

				string tenantName = await this.tenantManager.GetTenantNameAsync(tenant);
				claims.Add(new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.TenantNameClaimType, tenantName));

				string tenantDisplayName = await this.tenantManager.GetTenantDisplayNameAsync(tenant);
				claims.Add(new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.TenantDisplayNameClaimType, tenantDisplayName));
			}

			return claims.AsReadOnly();
		}

		private async Task<IList<Claim>> GetUserPermissions(TUser user)
		{
			IList<string> rolesNames = await this.userManager.GetRolesAsync(user);

			IList<Claim> claims = new List<Claim>();
			foreach(string roleName in rolesNames)
			{
				IList<TPermission> permissions = await this.permissionManager.GetPermissionsInRoleAsync(roleName);
				foreach(TPermission permission in permissions)
				{
					if(permission is not null)
					{
						claims.Add(new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, permission.ToString()));
					}
				}
			}

			return claims;
		}

		private async Task<IList<Claim>> GetTenantPermissions(TTenant tenant)
		{
			IList<string> rolesNames = await this.tenantManager.GetRolesAsync(tenant);

			IList<Claim> claims = new List<Claim>();
			foreach(string roleName in rolesNames)
			{
				IList<TPermission> permissions = await this.permissionManager.GetPermissionsInRoleAsync(roleName);
				foreach(TPermission permission in permissions)
				{
					if(permission is not null)
					{
						claims.Add(new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, permission.ToString()));
					}
				}
			}

			return claims;
		}
	}
}
