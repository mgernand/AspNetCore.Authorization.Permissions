namespace MadEyeMatt.Extensions.Identity.Permissions
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     Extension methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds the identity and permissions services for the identity types.
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static PermissionIdentityBuilder AddPermissionsIdentityCore<TUser, TRole, TPermission>(this IServiceCollection services)
			where TUser : class
			where TRole : class
			where TPermission : class
		{
			return services.AddPermissionsIdentityCore<TUser, TRole, TPermission>(_ =>
			{
			});
		}

		/// <summary>
		///     Adds the identity and permissions services for the identity types.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static PermissionIdentityBuilder AddPermissionsIdentityCore<TUser, TRole, TPermission>(this IServiceCollection services, Action<IdentityOptions> setupAction)
			where TUser : class
			where TRole : class
			where TPermission : class
		{
			// Services permissions identity depends on.
			services.AddOptions().AddLogging();

			// Services used by permissions identity.
			services.TryAddScoped<PermissionIdentityErrorDescriber>();
			services.TryAddScoped<PermissionManager<TPermission>>();
			services.TryAddScoped<IPermissionValidator<TPermission>, PermissionValidator<TPermission>>();

			// Configure identity with roles.
			IdentityBuilder builder = services
				.AddIdentityCore<TUser>(setupAction)
				.AddRoles<TRole>()
				.AddErrorDescriber<PermissionIdentityErrorDescriber>()
				.AddClaimsPrincipalFactory<PermissionUserClaimsPrincipalFactory<TUser>>();

			return new PermissionIdentityBuilder(typeof(TUser), typeof(TRole), typeof(TPermission), builder.Services);
		}

		/// <summary>
		///     Adds the identity and permissions services for the identity types.
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static PermissionIdentityBuilder AddPermissionsIdentityCore<TTenant, TUser, TRole, TPermission>(this IServiceCollection services)
			where TTenant : class
			where TUser : class
			where TRole : class
			where TPermission : class
		{
			return services.AddPermissionsIdentityCore<TTenant, TUser, TRole, TPermission>(_ =>
			{
			});
		}

		/// <summary>
		///     Adds the identity and permissions services for the identity types.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="setupAction"></param>
		/// <returns></returns>
		public static PermissionIdentityBuilder AddPermissionsIdentityCore<TTenant, TUser, TRole, TPermission>(this IServiceCollection services, Action<IdentityOptions> setupAction)
			where TTenant : class
			where TUser : class
			where TRole : class
			where TPermission : class
		{
			// Services permissions identity depends on.
			services.AddOptions().AddLogging();

			// Services used by permissions identity.
			services.TryAddScoped<PermissionManager<TPermission>>();
			services.TryAddScoped<IPermissionValidator<TPermission>, PermissionValidator<TPermission>>();
			services.TryAddScoped<TenantManager<TTenant>>();
			services.TryAddScoped<ITenantValidator<TTenant>, TenantValidator<TTenant>>();
			services.TryAddScoped<PermissionIdentityErrorDescriber>();

			// Configure identity with roles.
			IdentityBuilder builder = services
				.AddIdentityCore<TUser>(setupAction)
				.AddRoles<TRole>()
				.AddUserManager<UserManager<TUser>>()
				.AddErrorDescriber<PermissionIdentityErrorDescriber>()
				.AddClaimsPrincipalFactory<PermissionUserClaimsPrincipalFactory<TUser>>();

			return new PermissionIdentityBuilder(typeof(TTenant), typeof(TUser), typeof(TRole), typeof(TPermission), builder.Services);
		}
	}
}
