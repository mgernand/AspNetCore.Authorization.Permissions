namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using MadEyeMatt.AspNetCore.Identity.Permissions.Model;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Extension methods for the <see cref="IdentityBuilderExtensions" /> type.
	/// </summary>
	[PublicAPI]
	public static class IdentityBuilderExtensions
	{
		/// <summary>
		///     Adds the claims provider for the identity library.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IdentityBuilder AddIdentityClaimsProvider(this IdentityBuilder builder)
		{
			return builder.AddIdentityClaimsProvider<IdentityTenantUser, IdentityPermission, IdentityTenant>();
		}

		/// <summary>
		///     Adds the claims provider for the identity library.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IdentityBuilder AddIdentityClaimsProvider<TUser>(this IdentityBuilder builder)
			where TUser : class
		{
			return builder.AddIdentityClaimsProvider<TUser, IdentityPermission, IdentityTenant>();
		}

		/// <summary>
		///     Adds the claims provider for the identity library.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IdentityBuilder AddIdentityClaimsProvider<TUser, TPermission>(this IdentityBuilder builder)
			where TUser : class
			where TPermission : class
		{
			return builder.AddIdentityClaimsProvider<TUser, TPermission, IdentityTenant>();
		}

		/// <summary>
		///     Adds the claims provider for the identity library.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IdentityBuilder AddIdentityClaimsProvider<TUser, TPermission, TTenant>(this IdentityBuilder builder)
			where TUser : class
			where TPermission : class
			where TTenant : class
		{
			Type identityClaimsProviderType = typeof(IdentityClaimsProvider<,,>)
				.MakeGenericType(typeof(TUser), typeof(TPermission), typeof(TTenant));

			builder.Services.AddClaimsProvider(identityClaimsProviderType);

			return builder;
		}
	}
}
