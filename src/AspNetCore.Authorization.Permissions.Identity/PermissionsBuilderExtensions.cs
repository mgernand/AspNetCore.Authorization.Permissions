namespace AspNetCore.Authorization.Permissions.Identity
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;

	[PublicAPI]
	public static class PermissionsBuilderExtensions
	{
		public static IPermissionsBuilder AddIdentityClaimsProvider<TUser, TPermission, TTenant>(this IPermissionsBuilder builder)
			where TUser : class
			where TPermission : class
			where TTenant : class
		{
			Type identityClaimsProviderType = typeof(IdentityClaimsProvider<,,>)
				.MakeGenericType(typeof(TUser), typeof(TPermission), typeof(TTenant));

			builder.AddClaimsProvider(identityClaimsProviderType);
			return builder;
		}

		public static IPermissionsBuilder AddIdentityClaimsProvider<TPermission, TTenant>(this IPermissionsBuilder builder)
			where TPermission : class
			where TTenant : class
		{
			return builder.AddIdentityClaimsProvider<IdentityUser, TPermission, TTenant>();
		}

		public static IPermissionsBuilder AddIdentityClaimsProvider(this IPermissionsBuilder builder)
		{
			return builder.AddIdentityClaimsProvider<IdentityTenantUser, IdentityPermission, IdentityTenant>();
		}
	}
}
