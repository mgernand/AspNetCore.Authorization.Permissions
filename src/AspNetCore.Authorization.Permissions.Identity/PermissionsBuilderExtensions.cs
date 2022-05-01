namespace AspNetCore.Authorization.Permissions.Identity
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="PermissionsAuthenticationOptions" /> type.
	/// </summary>
	[PublicAPI]
	public static class PermissionsBuilderExtensions
	{
		/// <summary>
		///     Adds the claims provider for the identity library.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public static void AddIdentityClaimsProvider(this PermissionsAuthenticationOptions options)
		{
			Type identityClaimsProviderType = typeof(IdentityClaimsProvider<,,>)
				.MakeGenericType(typeof(IdentityTenantUser), typeof(IdentityPermission), typeof(IdentityTenant));

			options.AddClaimsProvider(identityClaimsProviderType);
		}
	}
}
