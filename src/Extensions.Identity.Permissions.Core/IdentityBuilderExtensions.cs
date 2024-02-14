namespace MadEyeMatt.Extensions.Identity.Permissions
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Extension methods for the <see cref="IdentityBuilder" /> type.
	/// </summary>
	[PublicAPI]
	public static class IdentityBuilderExtensions
	{
		/// <summary>
		///     Adds a <see cref="PermissionManager{TUser}" /> for the configured permission type.
		/// </summary>
		/// <typeparam name="TPermissionManager">The type of the permission manager to add.</typeparam>
		public static IdentityBuilder AddPermissionManager<TPermissionManager>(this IdentityBuilder builder) where TPermissionManager : class
		{
			if(builder is PermissionIdentityBuilder permissionBuilder)
			{
				return permissionBuilder.AddPermissionManager<TPermissionManager>();
			}

			throw new InvalidOperationException($"The builder was not of {nameof(PermissionIdentityBuilder)} type.");
		}

		/// <summary>
		///     Adds a <see cref="TenantManager{TTenant}" />  for the configured tenant type.
		/// </summary>
		/// <typeparam name="TTenantManager">The type of the tenant manager to add.</typeparam>
		public static IdentityBuilder AddTenantManager<TTenantManager>(this IdentityBuilder builder) where TTenantManager : class
		{
			if(builder is PermissionIdentityBuilder permissionBuilder)
			{
				return permissionBuilder.AddTenantManager<TTenantManager>();
			}

			throw new InvalidOperationException($"The builder was not of {nameof(PermissionIdentityBuilder)} type.");
		}
	}
}
