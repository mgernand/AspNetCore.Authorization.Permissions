namespace MadEyeMatt.AspNetCore.Authorization.Permissions
{
    using System;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

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
			services.AddTransient<MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService, UserPermissionsService>();
			services.AddTransient<MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IPermissionLookupNormalizer, UpperInvariantPermissionLookupNormalizer>();

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
			where TProvider : class, MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider
		{
			return services.AddClaimsProvider(typeof(TProvider));
		}

		/// <summary>
		///     Adds the given claims provider type.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="claimsProviderType"></param>
		/// <returns></returns>
		public static IServiceCollection AddClaimsProvider(this IServiceCollection services, Type claimsProviderType)
		{
			if(!claimsProviderType.IsAssignableTo(typeof(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider)))
			{
				throw new ArgumentException(
					"The claims provider type must implement the IClaimsProvider contract.",
					nameof(claimsProviderType));
			}

			services.TryAddScoped(claimsProviderType);
			services.TryAddScoped<MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider>(sp =>
			{
				// Decorate the registered claims provider with an internal one
				// that checks the provided claims for correctness.
				MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider claimsProvider = (MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider)sp.GetRequiredService(claimsProviderType);
				return new EnsureCorrectClaimsProvider(claimsProvider);
			});

			return services;
		}
	}
}
