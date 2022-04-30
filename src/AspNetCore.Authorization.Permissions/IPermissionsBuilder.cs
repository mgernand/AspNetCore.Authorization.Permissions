namespace AspNetCore.Authorization.Permissions
{
	using System;
	using AspNetCore.Authorization.Permissions.Abstractions;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IPermissionsBuilder
	{
		IPermissionsBuilder AddClaimsProvider<TProvider>() where TProvider : class, IClaimsProvider;

		IPermissionsBuilder AddClaimsProvider(Type providerType);

		IPermissionsBuilder AddClaimsProvider<TProvider>(Func<IServiceProvider, TProvider> factoryFunc) where TProvider : class, IClaimsProvider;
	}
}
