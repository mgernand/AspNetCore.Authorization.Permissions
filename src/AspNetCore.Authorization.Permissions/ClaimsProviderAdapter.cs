namespace AspNetCore.Authorization.Permissions
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using AspNetCore.Authorization.Permissions.Abstractions;
	using Fluxera.Utilities.Extensions;

	internal sealed class ClaimsProviderAdapter : IClaimsProviderAdapter
	{
		private readonly IClaimsProvider claimsProvider;

		public ClaimsProviderAdapter(IClaimsProvider claimsProvider)
		{
			this.claimsProvider = claimsProvider;
		}

		/// <inheritdoc />
		public async Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
		{
			IReadOnlyCollection<Claim> claims = await this.claimsProvider.GetPermissionClaimsForUserAsync(userId);

			IList<Claim> permissionClaims = new List<Claim>();

			if(claims is not null && claims.Any())
			{
				foreach(Claim claim in claims)
				{
					// Ignore duplicate permissions.
					if(permissionClaims.Any(x => x.Value == claim.Value))
					{
						continue;
					}

					// Only add claim type we know.
					if(claim.Type is PermissionClaimTypes.PermissionClaimType or PermissionClaimTypes.TenantNameClaimType)
					{
						permissionClaims.Add(claim);
					}
				}
			}

			return permissionClaims.AsReadOnly();
		}
	}
}
