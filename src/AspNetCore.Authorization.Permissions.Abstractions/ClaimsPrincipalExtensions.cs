﻿namespace AspNetCore.Authorization.Permissions.Abstractions
{
	using System.Collections.Generic;
	using System.Security.Claims;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class ClaimsPrincipalExtensions
	{
		/// <summary>
		///     Gets the permission from the given user.
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public static IReadOnlyCollection<string> GetPermissions(this ClaimsPrincipal user)
		{
			return user.Claims.GetPermissions();
		}

		/// <summary>
		///     Checks if the user has the given permission.
		/// </summary>
		/// <param name="user"></param>
		/// <param name="permission"></param>
		/// <returns></returns>
		public static bool HasPermission(this ClaimsPrincipal user, string permission)
		{
			return user.Claims.HasPermission(permission);
		}

		/// <summary>
		///     Gets the user ID from the given user.
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public static string GetUserId(this ClaimsPrincipal user)
		{
			return user.Claims.GetUserId();
		}

		/// <summary>
		///     Gets the tenant name from the given user.
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public static string GetTenantName(this ClaimsPrincipal user)
		{
			return user.Claims.GetTenantName();
		}
	}
}