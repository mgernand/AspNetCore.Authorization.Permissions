namespace AspNetCore.Authorization.Permissions.Identity
{
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using AspNetCore.Authorization.Permissions.Abstractions;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     Adds permission and tenant claims to the user's claims.
	/// </summary>
	/// <remarks>
	///     See:https://korzh.com/blogs/net-tricks/aspnet-identity-store-user-data-in-claims
	/// </remarks>
	[PublicAPI]
	public class PermissionUserClaimsPrincipalFactory<TUser> : UserClaimsPrincipalFactory<TUser>
		where TUser : class, IUser
	{
		private readonly IClaimsProvider claimsProvider;

		/// <inheritdoc />
		public PermissionUserClaimsPrincipalFactory(
			PermissionsUserManager<TUser> userManager,
			IOptions<IdentityOptions> optionsAccessor,
			IClaimsProvider claimsProvider)
			: base(userManager, optionsAccessor)
		{
			this.claimsProvider = claimsProvider;
		}

		/// <inheritdoc />
		protected override async Task<ClaimsIdentity> GenerateClaimsAsync(TUser user)
		{
			ClaimsIdentity identity = await base.GenerateClaimsAsync(user);
			string userId = identity.Claims.GetUserId();
			IReadOnlyCollection<Claim> claims = await this.claimsProvider.GetPermissionClaimsForUserAsync(userId);
			identity.AddClaims(claims);

			return identity;
		}
	}
}
