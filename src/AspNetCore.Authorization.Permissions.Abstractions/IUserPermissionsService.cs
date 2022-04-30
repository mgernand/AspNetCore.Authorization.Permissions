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
		///     Gets the permissions from the given user.
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		IReadOnlyCollection<string> GetPermissionsFrom(ClaimsPrincipal user);

		/// <summary>
		///     Gets the permission from the given user claims.
		/// </summary>
		/// <param name="claims"></param>
		/// <returns></returns>
		IReadOnlyCollection<string> GetPermissionsFrom(IEnumerable<Claim> claims);

		/// <summary>
		///     Checks if the user has the given permission.
		/// </summary>
		/// <param name="user"></param>
		/// <param name="permission"></param>
		/// <returns></returns>
		bool HasPermission(ClaimsPrincipal user, string permission);

		/// <summary>
		///     Checks if the user claims has the given permission.
		/// </summary>
		/// <param name="claims"></param>
		/// <param name="permission"></param>
		/// <returns></returns>
		bool HasPermission(IEnumerable<Claim> claims, string permission);
	}
}
