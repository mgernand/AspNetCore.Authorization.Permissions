namespace MadEyeMatt.AspNetCore.Identity.Permissions
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
		///     Adds the identity and permissions services for the identity default types,
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static PermissionIdentityBuilder AddPermissionsIdentity<TUser, TRole, TPermission>(this IServiceCollection services, Action<IdentityOptions> setupAction = null)
			where TUser : class
			where TRole : class
			where TPermission : class
		{
			setupAction ??= _ =>
			{
			};

			IdentityBuilder builder = services
				.AddIdentityCore<TUser>(setupAction)
				.AddRoles<TRole>()
				.AddUserManager<TenantUserManager<TUser>>()
				.AddRoleManager<RoleManager<TRole>>()
				.AddErrorDescriber<PermissionsErrorDescriber>()
				.AddClaimsPrincipalFactory<PermissionUserClaimsPrincipalFactory<TUser>>();

			services.AddScoped<PermissionsErrorDescriber>();

			PermissionIdentityBuilder permissionsBuilder = new PermissionIdentityBuilder(builder, typeof(TPermission))
				.AddPermissionManager<PermissionManager<TPermission>>()
				.AddPermissionValidator<PermissionValidator<TPermission>>();

			return permissionsBuilder;
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
		public static PermissionIdentityBuilder AddPermissionsIdentity<TTenant, TUser, TRole, TPermission>(this IServiceCollection services, Action<IdentityOptions> setupAction = null)
			where TTenant : class
            where TUser : class
			where TRole : class
			where TPermission : class
		{
			setupAction ??= _ =>
			{
			};

			IdentityBuilder builder = services
				.AddIdentityCore<TUser>(setupAction)
				.AddRoles<TRole>()
				.AddUserManager<TenantUserManager<TUser>>()
				.AddRoleManager<RoleManager<TRole>>()
				.AddErrorDescriber<PermissionsErrorDescriber>()
				.AddClaimsPrincipalFactory<PermissionUserClaimsPrincipalFactory<TUser>>();

			services.AddScoped<PermissionsErrorDescriber>();

			PermissionIdentityBuilder permissionsBuilder = new PermissionIdentityBuilder(builder, typeof(TTenant), typeof(TPermission))
				.AddPermissionManager<PermissionManager<TPermission>>()
				.AddPermissionValidator<PermissionValidator<TPermission>>()
                .AddTenantManager<TenantManager<TTenant>>()
				.AddTenantValidator<TenantValidator<TTenant>>();

			return permissionsBuilder;
		}
	}
}
