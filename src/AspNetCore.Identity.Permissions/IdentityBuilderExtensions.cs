namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Extension methods for the <see cref="IdentityBuilder" /> type.
	/// </summary>
	[PublicAPI]
	public static class IdentityBuilderExtensions
	{
        /// <summary>
        ///		Adds the <see cref="PermissionClaimsProvider{TUser,TPermission}"/> or the <see cref="PermissionClaimsProvider{TTenant,TUser,TPermission}"/>.
        /// </summary>
        /// <returns></returns>
        public static IdentityBuilder AddPermissionClaimsProvider(this IdentityBuilder builder)
		{
			if(builder is PermissionIdentityBuilder permissionBuilder)
			{
				return permissionBuilder.AddPermissionClaimsProvider();
			}

			throw new InvalidOperationException($"The builder was not of {nameof(PermissionIdentityBuilder)} type.");
		}

		/// <summary>
        ///		Adds the <see cref="PermissionClaimsProvider{TUser,TPermission}"/> or the <see cref="PermissionClaimsProvider{TTenant,TUser,TPermission}"/>.
        /// </summary>
        /// <returns></returns>
        public static PermissionIdentityBuilder AddPermissionClaimsProvider(this PermissionIdentityBuilder builder)
		{
			Type identityClaimsProviderType = builder.TenantType is null
				? typeof(PermissionClaimsProvider<,>).MakeGenericType(builder.UserType, builder.PermissionType)
				: typeof(PermissionClaimsProvider<,,>).MakeGenericType(builder.TenantType, builder.UserType, builder.PermissionType);

			builder.Services.AddClaimsProvider(identityClaimsProviderType);

            return builder;
		}
    }
}
