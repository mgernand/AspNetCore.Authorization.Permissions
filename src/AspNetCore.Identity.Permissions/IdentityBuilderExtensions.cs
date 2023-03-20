namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
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
		public static IdentityBuilder AddIdentityClaimsProvider<TUser, TPermission>(this IdentityBuilder builder)
			where TUser : class
			where TPermission : class
		{
			Type identityClaimsProviderType = typeof(IdentityClaimsProvider<,>)
				.MakeGenericType(typeof(TUser), typeof(TPermission));

			builder.Services.AddClaimsProvider(identityClaimsProviderType);

			return builder;
        }

		/// <summary>
		///     Adds the claims provider for the identity library.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IdentityBuilder AddIdentityClaimsProvider<TTenant, TUser, TPermission>(this IdentityBuilder builder)
			where TTenant : class
            where TUser : class
			where TPermission : class
		{
			Type identityClaimsProviderType = typeof(IdentityClaimsProvider<,,>)
				.MakeGenericType(typeof(TTenant), typeof(TUser), typeof(TPermission));

			builder.Services.AddClaimsProvider(identityClaimsProviderType);

			return builder;
		}
	}
}
