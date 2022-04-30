namespace AspNetCore.Authorization.Permissions
{
	using AspNetCore.Authorization.Permissions.Abstractions;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.Extensions.DependencyInjection;

	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		public static IPermissionsBuilder AddPermissions(this IServiceCollection services)
		{
			services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
			services.AddSingleton<IAuthorizationHandler, PermissionPolicyHandler>();
			services.AddTransient<IUserPermissionsService, UserPermissionsService>();
			services.AddTransient<IPermissionLookupNormalizer, UpperInvariantPermissionLookupNormalizer>();
			services.AddTransient<IClaimsProviderAdapter, ClaimsProviderAdapter>();

			return new PermissionsBuilder(services);
		}
	}
}
