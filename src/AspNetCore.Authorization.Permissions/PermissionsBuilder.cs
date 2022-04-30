namespace AspNetCore.Authorization.Permissions
{
	using System;
	using AspNetCore.Authorization.Permissions.Abstractions;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class PermissionsBuilder : IPermissionsBuilder
	{
		private readonly IServiceCollection services;

		public PermissionsBuilder(IServiceCollection services)
		{
			this.services = services;
		}

		/// <inheritdoc />
		public IPermissionsBuilder AddClaimsProvider<TProvider>() where TProvider : class, IClaimsProvider
		{
			this.services.AddTransient<IClaimsProvider, TProvider>();

			return this;
		}

		/// <inheritdoc />
		public IPermissionsBuilder AddClaimsProvider(Type providerType)
		{
			this.services.AddTransient(typeof(IClaimsProvider), providerType);

			return this;
		}

		/// <inheritdoc />
		public IPermissionsBuilder AddClaimsProvider<TProvider>(Func<IServiceProvider, TProvider> factoryFunc) where TProvider : class, IClaimsProvider
		{
			this.services.AddTransient(factoryFunc);

			return this;
		}
	}
}
