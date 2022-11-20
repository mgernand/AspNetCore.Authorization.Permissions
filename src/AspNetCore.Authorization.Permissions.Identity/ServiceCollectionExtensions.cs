namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Identity
{
	using System;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.Model;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     Extension methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds the identity and permissions services for the identity default types,
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static PermissionsIdentityBuilder AddPermissionsIdentity(this IServiceCollection services, Action<IdentityOptions> setupAction = null)
		{
			return services.AddPermissionsIdentity<PermissionsUser, PermissionsRole, PermissionsPermission, PermissionsIdentityTenant>(setupAction);
		}

		/// <summary>
		///     Adds the identity and permissions services for the identity default types,
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static PermissionsIdentityBuilder AddPermissionsIdentity<TUser>(this IServiceCollection services, Action<IdentityOptions> setupAction = null)
			where TUser : class, IUser
		{
			return services.AddPermissionsIdentity<TUser, PermissionsRole, PermissionsPermission, PermissionsIdentityTenant>(setupAction);
		}

		/// <summary>
		///     Adds the identity and permissions services for the identity default types,
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static PermissionsIdentityBuilder AddPermissionsIdentity<TUser, TRole>(this IServiceCollection services, Action<IdentityOptions> setupAction = null)
			where TUser : class, IUser
			where TRole : class, IRole
		{
			return services.AddPermissionsIdentity<TUser, TRole, PermissionsPermission, PermissionsIdentityTenant>(setupAction);
		}

		/// <summary>
		///     Adds the identity and permissions services for the identity default types,
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static PermissionsIdentityBuilder AddPermissionsIdentity<TUser, TRole, TPermission>(this IServiceCollection services, Action<IdentityOptions> setupAction = null)
			where TUser : class, IUser
			where TRole : class, IRole
			where TPermission : class, IPermission
		{
			return services.AddPermissionsIdentity<TUser, TRole, TPermission, PermissionsIdentityTenant>(setupAction);
		}

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
			where TUser : class, IUser
			where TRole : class, IRole
			where TPermission : class, IPermission
			where TTenant : class, ITenant
		{
			IdentityBuilder builder = services
				.AddIdentityCore<TUser>(setupAction)
				.AddRoles<TRole>()
				.AddClaimsPrincipalFactory<PermissionUserClaimsPrincipalFactory<TUser>>()
				.AddUserManager<PermissionsUserManager<TUser>>();

			PermissionsIdentityBuilder permissionsBuilder = new PermissionsIdentityBuilder(builder, typeof(TPermission), typeof(TTenant))
				.AddPermissionManager<PermissionManager<TPermission>>()
				.AddPermissionValidator<PermissionValidator<TPermission>>()
				.AddTenantManager<TenantManager<TTenant>>()
				.AddTenantValidator<TenantValidator<TTenant>>();

			return permissionsBuilder;
		}
	}
}
