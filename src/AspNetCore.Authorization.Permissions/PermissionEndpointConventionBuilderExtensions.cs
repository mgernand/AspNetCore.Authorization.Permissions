﻿#if NET7_0_OR_GREATER
namespace MadEyeMatt.AspNetCore.Authorization.Permissions
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Builder;

	/// <summary>
	///		Authorization extension methods for <see cref="IEndpointConventionBuilder"/>.
	/// </summary>
	[PublicAPI]
	public static class PermissionEndpointConventionBuilderExtensions
	{
		/// <summary>
		///		Adds a permission policy with the specified name to the endpoint(s).
		/// </summary>
		/// <param name="builder">The endpoint convention builder.</param>
		/// <param name="permissionName">A permission name of a permission that is required.</param>
		/// <returns>The original convention builder parameter.</returns>
		public static TBuilder RequirePermission<TBuilder>(this TBuilder builder, string permissionName)
			where TBuilder : IEndpointConventionBuilder
		{
			if (builder is null)
			{
				throw new ArgumentNullException(nameof(builder));
			}

			if (string.IsNullOrWhiteSpace(permissionName))
			{
				throw new ArgumentNullException(nameof(permissionName));
			}

			return builder.RequireAuthorization(permissionName);
		}
	}
}
#endif