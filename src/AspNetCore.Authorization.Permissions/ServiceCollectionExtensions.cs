namespace AspNetCore.Authorization.Permissions
{
	using System;
	using AspNetCore.Authorization.Permissions.Abstractions;
	using Fluxera.Extensions.DependencyInjection;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     Extensions methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Add the permissions services.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configureAction"></param>
		/// <returns></returns>
		public static IServiceCollection AddPermissionsAuthorization(this IServiceCollection services, Action<PermissionsAuthenticationOptions> configureAction)
		{
			services.AddAuthorization();
			services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
			services.AddSingleton<IAuthorizationHandler, PermissionPolicyHandler>();
			services.AddTransient<IUserPermissionsService, UserPermissionsService>();
			services.AddTransient<IPermissionLookupNormalizer, UpperInvariantPermissionLookupNormalizer>();

			PermissionsAuthenticationOptions options = new PermissionsAuthenticationOptions(services);
			configureAction?.Invoke(options);

			// Decorate the registered claims provider with an internal one
			// that checks the provided claims for correctness.
			services
				.Decorate<IClaimsProvider>()
				.With<EnsureCorrectClaimsProvider>();

			return services;
		}
	}
}
