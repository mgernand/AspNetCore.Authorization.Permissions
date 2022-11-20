namespace MadEyeMatt.AspNetCore.Authorization.Permissions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    [UsedImplicitly]
	internal sealed class EnsureCorrectClaimsProvider : MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider
	{
		private readonly MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider innerClaimsProvider;

		public EnsureCorrectClaimsProvider(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider innerClaimsProvider)
		{
			this.innerClaimsProvider = innerClaimsProvider;
		}

		/// <inheritdoc />
		public async Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
		{
			IReadOnlyCollection<Claim> claims = await this.innerClaimsProvider.GetPermissionClaimsForUserAsync(userId);

			List<Claim> permissionClaims = new List<Claim>();

			if(claims is not null && claims.Any())
			{
				foreach(Claim claim in claims)
				{
					// Ignore duplicate permissions.
					if(permissionClaims.Any(x => x.Value == claim.Value))
					{
						continue;
					}

					// Only add claim types we know.
					if(claim.Type is
					   MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType or
					   MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.TenantIdClaimType or
					   MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.TenantNameClaimType or
					   MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.TenantDisplayNameClaimType)
					{
						permissionClaims.Add(claim);
					}
				}
			}

			return permissionClaims.AsReadOnly();
		}
	}
}
