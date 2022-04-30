namespace AspNetCore.Authorization.Permissions
{
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     A contact for a claims provider.
	/// </summary>
	[PublicAPI]
	public interface IClaimsProviderAdapter
	{
		/// <summary>
		///     Gets the permission claims for the user with the given ID.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId);
	}
}
