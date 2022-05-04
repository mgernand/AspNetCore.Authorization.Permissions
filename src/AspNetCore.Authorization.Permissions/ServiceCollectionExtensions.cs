namespace AspNetCore.Authorization.Permissions
{
	using System;
	using AspNetCore.Authorization.Permissions.Abstractions;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Guards;
	using Fluxera.Utilities.Extensions;
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
		public static IServiceCollection AddPermissionsAuthorization(this IServiceCollection services, Action<PermissionsAuthenticationOptions> configureAction = null)
		{
			services.AddAuthorization();
			services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
			services.AddSingleton<IAuthorizationHandler, PermissionPolicyHandler>();
			services.AddTransient<IUserPermissionsService, UserPermissionsService>();
			services.AddTransient<IPermissionLookupNormalizer, UpperInvariantPermissionLookupNormalizer>();

			PermissionsAuthenticationOptions options = new PermissionsAuthenticationOptions();
			configureAction?.Invoke(options);

			return services;
		}

		/// <summary>
		///     Adds the given claims provider type.
		/// </summary>
		/// <param name="services"></param>
		/// <typeparam name="TProvider"></typeparam>
		/// <returns></returns>
		public static IServiceCollection AddClaimsProvider<TProvider>(this IServiceCollection services)
			where TProvider : class, IClaimsProvider
		{
			return services
				.AddScoped<IClaimsProvider, TProvider>()
				.AddClaimsProviderDecorator();
		}

		/// <summary>
		///     Adds the given claims provider type.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="claimsProviderType"></param>
		/// <returns></returns>
		public static IServiceCollection AddClaimsProvider(this IServiceCollection services, Type claimsProviderType)
		{
			Guard.Against.False(claimsProviderType.Implements<IClaimsProvider>(), nameof(claimsProviderType),
				"The claims provider type must implement the IClaimsProvider contract.");

			return services
				.AddScoped(typeof(IClaimsProvider), claimsProviderType)
				.AddClaimsProviderDecorator();
		}

		private static IServiceCollection AddClaimsProviderDecorator(this IServiceCollection services)
		{
			// Decorate the registered claims provider with an internal one
			// that checks the provided claims for correctness.
			services
				.Decorate<IClaimsProvider>()
				.With<EnsureCorrectClaimsProvider>();

			return services;
		}
	}
}
