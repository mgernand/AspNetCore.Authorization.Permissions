namespace AspNetCore.Authorization.Permissions
{
	using AspNetCore.Authorization.Permissions.Abstractions;
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
		/// <returns></returns>
		public static IServiceCollection AddPermissions(this IServiceCollection services)
		{
			services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
			services.AddSingleton<IAuthorizationHandler, PermissionPolicyHandler>();
			services.AddTransient<IUserPermissionsService, UserPermissionsService>();
			services.AddTransient<IPermissionLookupNormalizer, UpperInvariantPermissionLookupNormalizer>();
			services.AddTransient<IClaimsProviderAdapter, ClaimsProviderAdapter>();

			return services;
		}
	}
}
