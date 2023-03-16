namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.Permissions.Model;
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
			return services.AddPermissionsIdentity<PermissionsUser, PermissionsRole, PermissionsPermission, PermissionsTenant>(setupAction);
		}

		/// <summary>
		///     Adds the identity and permissions services for the identity default types,
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static PermissionsIdentityBuilder AddPermissionsIdentity<TUser>(this IServiceCollection services, Action<IdentityOptions> setupAction = null)
			where TUser : class
		{
			return services.AddPermissionsIdentity<TUser, PermissionsRole, PermissionsPermission, PermissionsTenant>(setupAction);
		}

		/// <summary>
		///     Adds the identity and permissions services for the identity default types,
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static PermissionsIdentityBuilder AddPermissionsIdentity<TUser, TRole>(this IServiceCollection services, Action<IdentityOptions> setupAction = null)
			where TUser : class
			where TRole : class
		{
			return services.AddPermissionsIdentity<TUser, TRole, PermissionsPermission, PermissionsTenant>(setupAction);
		}

		/// <summary>
		///     Adds the identity and permissions services for the identity default types,
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static PermissionsIdentityBuilder AddPermissionsIdentity<TUser, TRole, TPermission>(this IServiceCollection services, Action<IdentityOptions> setupAction = null)
			where TUser : class
			where TRole : class
			where TPermission : class
		{
			return services.AddPermissionsIdentity<TUser, TRole, TPermission, PermissionsTenant>(setupAction);
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
			where TUser : class
			where TRole : class
			where TPermission : class
			where TTenant : class
		{
			setupAction ??= _ => { };

            IdentityBuilder builder = services
				.AddIdentityCore<TUser>(setupAction)
				.AddRoles<TRole>()
				.AddUserManager<TenantUserManager<TUser>>()
				.AddRoleManager<CustomRoleManager<TRole>>()
				.AddErrorDescriber<PermissionsErrorDescriber>()
				.AddClaimsPrincipalFactory<PermissionUserClaimsPrincipalFactory<TUser>>();

			services.AddScoped<PermissionsErrorDescriber>();
			services.AddScoped<IUserManager<TUser>>(serviceProvider => serviceProvider.GetRequiredService<TenantUserManager<TUser>>());
			services.AddScoped<ITenantUserManager<TUser>>(serviceProvider => serviceProvider.GetRequiredService<TenantUserManager<TUser>>());
			services.AddScoped<IRoleManager<TRole>>(serviceProvider => serviceProvider.GetRequiredService<CustomRoleManager<TRole>>());

            PermissionsIdentityBuilder permissionsBuilder = new PermissionsIdentityBuilder(builder, typeof(TPermission), typeof(TTenant))
				.AddPermissionManager<PermissionManager<TPermission>>()
				.AddTenantManager<TenantManager<TTenant>>()
				.AddTenantValidator<TenantValidator<TTenant>>()
				.AddPermissionValidator<PermissionValidator<TPermission>>();

            return permissionsBuilder;
		}
	}
}
