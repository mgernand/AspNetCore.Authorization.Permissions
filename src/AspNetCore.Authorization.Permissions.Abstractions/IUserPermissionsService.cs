namespace AspNetCore.Authorization.Permissions.Abstractions
{
	using System.Collections.Generic;
	using System.Security.Claims;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a service that provides the permissions of a user.
	/// </summary>
	[PublicAPI]
	public interface IUserPermissionsService
	{
		/// <summary>
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		IReadOnlyCollection<string> GetPermissionsFor(ClaimsPrincipal user);

		/// <summary>
		/// </summary>
		/// <param name="user"></param>
		/// <param name="permission"></param>
		/// <returns></returns>
		bool HasPermission(ClaimsPrincipal user, string permission);
	}
}
