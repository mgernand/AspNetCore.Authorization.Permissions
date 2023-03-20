namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;

	[UsedImplicitly]
	internal sealed class IdentityClaimsProvider<TUser, TPermission> : IdentityClaimsProviderBase<TPermission>
		where TUser : class
		where TPermission : class
	{
		private readonly TenantUserManager<TUser> userManager;

		/// <summary>
		///     Initializes a new instance of the <see cref="IdentityClaimsProvider{TUser, TPermission}" /> type:
		/// </summary>
		/// <param name="userManager"></param>
		/// <param name="permissionManager"></param>
		public IdentityClaimsProvider(
			TenantUserManager<TUser> userManager,
			PermissionManager<TPermission> permissionManager) : base(permissionManager)
		{
			this.userManager = userManager;
		}

		/// <inheritdoc />
		public override async Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
		{
			List<Claim> claims = new List<Claim>();

			// Add the permission claims that com from user roles.
			TUser user = await this.userManager.FindByIdAsync(userId);
			claims.AddRange(await this.GetUserPermissions(user));

			return claims.AsReadOnly();
		}

		private async Task<IList<Claim>> GetUserPermissions(TUser user)
		{
			IList<string> roleNames = await this.userManager.GetRolesAsync(user);
			return await this.GetPermissionClaims(roleNames);
		}
	}

	[UsedImplicitly]
	internal sealed class IdentityClaimsProvider<TTenant, TUser, TPermission> : IdentityClaimsProviderBase<TPermission>
		where TTenant : class
        where TUser : class
		where TPermission : class
	{
		private readonly TenantUserManager<TUser> userManager;
		private readonly TenantManager<TTenant> tenantManager;

		/// <summary>
		///     Initializes a new instance of the <see cref="IdentityClaimsProvider{TUser, TPermission, TTenant}" /> type:
		/// </summary>
		/// <param name="userManager"></param>
		/// <param name="tenantManager"></param>
		/// <param name="permissionManager"></param>
		public IdentityClaimsProvider(
			TenantManager<TTenant> tenantManager,
            TenantUserManager<TUser> userManager,
			PermissionManager<TPermission> permissionManager) : base(permissionManager)
		{
			this.userManager = userManager;
			this.tenantManager = tenantManager;
		}

		/// <inheritdoc />
		public override async Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
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
			return await this.GetPermissionClaims(roleNames);
		}

		private async Task<IList<Claim>> GetTenantPermissions(TTenant tenant)
		{
			IList<string> roleNames = await this.tenantManager.GetRolesAsync(tenant);
			return await this.GetPermissionClaims(roleNames);
		}
	}
}
