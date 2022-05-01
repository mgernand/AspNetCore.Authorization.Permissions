namespace AspNetCore.Authorization.Permissions
{
	using System;
	using AspNetCore.Authorization.Permissions.Abstractions;
	using Fluxera.Guards;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     The options for the permission authorization.
	/// </summary>
	[PublicAPI]
	public sealed class PermissionsAuthenticationOptions
	{
		private readonly IServiceCollection services;

		/// <summary>
		///     Creates a new instance of the <see cref="PermissionsAuthenticationOptions" /> type.
		/// </summary>
		/// <param name="services"></param>
		public PermissionsAuthenticationOptions(IServiceCollection services)
		{
			this.services = services;
		}

		/// <summary>
		///     Adds the given claims provider type.
		/// </summary>
		/// <typeparam name="TProvider"></typeparam>
		/// <returns></returns>
		public void AddClaimsProvider<TProvider>() where TProvider : class, IClaimsProvider
		{
			this.services.AddScoped<IClaimsProvider, TProvider>();
		}

		/// <summary>
		///     Adds the given claims provider type.
		/// </summary>
		/// <param name="claimsProviderType"></param>
		/// <returns></returns>
		public void AddClaimsProvider(Type claimsProviderType)
		{
			Guard.Against.False(claimsProviderType.Implements<IClaimsProvider>(), nameof(claimsProviderType),
				"The claims provider type must implement the IClaimsProvider contract.");

			this.services.AddScoped(typeof(IClaimsProvider), claimsProviderType);
		}
	}
}
