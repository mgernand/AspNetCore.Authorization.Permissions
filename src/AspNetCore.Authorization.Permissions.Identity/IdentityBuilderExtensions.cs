namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Identity
{
	using System;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.Model;
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
			return builder.AddIdentityClaimsProvider<PermissionsUser, PermissionsIdentityPermission, PermissionsIdentityTenant>();
		}

		/// <summary>
		///     Adds the claims provider for the identity library.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IdentityBuilder AddIdentityClaimsProvider<TUser>(this IdentityBuilder builder)
			where TUser : class, IUser
		{
			return builder.AddIdentityClaimsProvider<TUser, PermissionsIdentityPermission, PermissionsIdentityTenant>();
		}

		/// <summary>
		///     Adds the claims provider for the identity library.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IdentityBuilder AddIdentityClaimsProvider<TUser, TPermission>(this IdentityBuilder builder)
			where TUser : class, IUser
			where TPermission : class, IPermission
		{
			return builder.AddIdentityClaimsProvider<TUser, TPermission, PermissionsIdentityTenant>();
		}

		/// <summary>
		///     Adds the claims provider for the identity library.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IdentityBuilder AddIdentityClaimsProvider<TUser, TPermission, TTenant>(this IdentityBuilder builder)
			where TUser : class, IUser
			where TPermission : class, IPermission
			where TTenant : class, ITenant
		{
			Type identityClaimsProviderType = typeof(IdentityClaimsProvider<,,>)
				.MakeGenericType(typeof(TUser), typeof(TPermission), typeof(TTenant));

			builder.Services.AddClaimsProvider(identityClaimsProviderType);

			return builder;
		}
	}
}
