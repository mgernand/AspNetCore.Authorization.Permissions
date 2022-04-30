namespace AspNetCore.Authorization.Permissions.Identity
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     Extension methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Add the identity and permissions services for the configured entity types.
		/// </summary>
		/// <typeparam name="TUser"></typeparam>
		/// <typeparam name="TRole"></typeparam>
		/// <typeparam name="TPermission"></typeparam>
		/// <typeparam name="TTenant"></typeparam>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static PermissionsIdentityBuilder AddPermissionsIdentity<TUser, TRole, TPermission, TTenant>(this IServiceCollection services, Action<IdentityOptions> setupAction = null)
			where TUser : class, ITenantUser
			where TRole : class
			where TPermission : class
			where TTenant : class
		{
			IdentityBuilder builder = services
				.AddIdentity<TUser, TRole>(setupAction)
				.AddIdentityClaimsProvider()
				.AddClaimsPrincipalFactory<PermissionUserClaimsPrincipalFactory<TUser>>()
				.AddUserManager<TenantUserManager<TUser>>();

			PermissionsIdentityBuilder permissionsBuilder = new PermissionsIdentityBuilder(builder, typeof(TPermission), typeof(TTenant))
				.AddPermissionManager<PermissionManager<TPermission>>()
				.AddPermissionValidator<PermissionValidator<TPermission>>()
				.AddTenantManager<TenantManager<TTenant>>()
				.AddTenantValidator<TenantValidator<TTenant>>();

			return permissionsBuilder;
		}

		/// <summary>
		///     Adds the identity and permissions services for the identity default types,
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static IdentityBuilder AddPermissionsIdentity(this IServiceCollection services, Action<IdentityOptions> setupAction = null)
		{
			return services.AddPermissionsIdentity<IdentityTenantUser, IdentityRole, IdentityPermission, IdentityTenant>(setupAction);
		}
	}
}
