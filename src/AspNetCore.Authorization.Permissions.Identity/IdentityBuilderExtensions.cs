namespace AspNetCore.Authorization.Permissions.Identity
{
	using System;
	using AspNetCore.Authorization.Permissions.Abstractions;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.DependencyInjection;

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
			Type identityClaimsProviderType = typeof(IdentityClaimsProvider<,,>)
				.MakeGenericType(typeof(IdentityTenantUser), typeof(IdentityPermission), typeof(IdentityTenant));

			builder.Services.AddScoped(typeof(IClaimsProvider), identityClaimsProviderType);
			return builder;
		}
	}
}
