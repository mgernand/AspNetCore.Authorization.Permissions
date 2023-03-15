namespace MadEyeMatt.AspNetCore.Authorization.Permissions
{
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for services that provide the permission claims of the user.
	/// </summary>
	[PublicAPI]
	public interface IClaimsProvider
	{
		/// <summary>
		///     Gets the permission claims for the user with the given ID.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId);
	}
}
