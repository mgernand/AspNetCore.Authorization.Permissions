namespace AspNetCore.Authorization.Permissions
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using AspNetCore.Authorization.Permissions.Abstractions;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class EnsureCorrectClaimsProvider : IClaimsProvider
	{
		private readonly IClaimsProvider innerClaimsProvider;

		public EnsureCorrectClaimsProvider(IClaimsProvider innerClaimsProvider)
		{
			this.innerClaimsProvider = innerClaimsProvider;
		}

		/// <inheritdoc />
		public async Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
		{
			IReadOnlyCollection<Claim> claims = await this.innerClaimsProvider.GetPermissionClaimsForUserAsync(userId);

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

					// Only add claim types we know.
					if(claim.Type is
					   PermissionClaimTypes.PermissionClaimType or
					   PermissionClaimTypes.TenantIdClaimType or
					   PermissionClaimTypes.TenantNameClaimType or
					   PermissionClaimTypes.TenantDisplayNameClaimType)
					{
						permissionClaims.Add(claim);
					}
				}
			}

			return permissionClaims.AsReadOnly();
		}
	}
}
