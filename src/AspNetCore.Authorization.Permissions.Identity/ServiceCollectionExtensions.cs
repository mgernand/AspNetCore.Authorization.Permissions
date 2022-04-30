namespace AspNetCore.Authorization.Permissions.Identity
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		public static IdentityBuilder AddPermissionsIdentity<TUser, TRole, TPermission, TTenant>(this IServiceCollection services, Action<IdentityOptions> setupAction = null)
			where TUser : class, ITenantUser
			where TRole : class
			where TPermission : class, IPermission
			where TTenant : class, ITenant
		{
			services.TryAddScoped<PermissionManager<TPermission>>();
			services.TryAddScoped<IPermissionValidator<TPermission>, PermissionValidator<TPermission>>();

			services.TryAddScoped<TenantManager<TTenant>>();
			services.TryAddScoped<ITenantValidator<TTenant>, TenantValidator<TTenant>>();

			services.AddScoped<IUserClaimsPrincipalFactory<TUser>, PermissionUserClaimsPrincipalFactory<TUser>>();

			return services
				.AddIdentity<TUser, TRole>(setupAction)
				.AddUserManager<TenantUserManager<TUser>>();
		}

		public static IdentityBuilder AddPermissionsIdentity(this IServiceCollection services, Action<IdentityOptions> setupAction = null)
		{
			return services.AddPermissionsIdentity<IdentityTenantUser, IdentityRole, IdentityPermission, IdentityTenant>(setupAction);
		}
	}
}
