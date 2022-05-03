namespace AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore
{
	using System;
	using AspNetCore.Authorization.Permissions.Abstractions;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     Extension methods for the <see cref="EntityFrameworkCore.IdentityBuilderExtensions" /> type.
	/// </summary>
	[PublicAPI]
	public static class IdentityBuilderExtensions
	{
		/// <summary>
		///     Adds the EF Core store implementations for Identity and the permissions library.
		/// </summary>
		/// <typeparam name="TContext"></typeparam>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IdentityBuilder AddPermissionsEntityFrameworkStores<TContext>(this IdentityBuilder builder)
			where TContext : DbContext
		{
			builder.AddEntityFrameworkStores<TContext>();
			AddStores(builder.Services, builder.UserType, typeof(IdentityPermission), typeof(IdentityTenant), builder.RoleType, typeof(TContext));
			return builder;
		}

		/// <summary>
		///     Adds the EF Core store implementations for Identity and the permissions library.
		/// </summary>
		/// <typeparam name="TContext"></typeparam>
		/// <typeparam name="TTenantAccessor"></typeparam>
		/// <param name="builder"></param>
		/// <returns></returns>
		public static IdentityBuilder AddPermissionsEntityFrameworkStores<TContext, TTenantAccessor>(this IdentityBuilder builder)
			where TContext : DbContext
			where TTenantAccessor : class, ITenantProvider
		{
			builder.AddPermissionsEntityFrameworkStores<TContext>();
			builder.Services.AddTransient<ITenantProvider, TTenantAccessor>();
			return builder;
		}

		private static void AddStores(IServiceCollection services, Type userType, Type permissionType, Type tenantType, Type roleType, Type contextType)
		{
			Type identityUserType = FindGenericBaseType(userType, typeof(IdentityTenantUser<>));
			if(identityUserType == null)
			{
				throw new InvalidOperationException("The given type is not an identity tenant user type.");
			}

			Type identityPermissionType = FindGenericBaseType(permissionType, typeof(IdentityPermission<>));
			if(identityPermissionType == null)
			{
				throw new InvalidOperationException("The given type is not an identity permission type.");
			}

			if(roleType == null)
			{
				throw new InvalidOperationException("The permissions extension needs a role type to work.");
			}

			Type identityTenantType = FindGenericBaseType(tenantType, typeof(IdentityTenant<>));
			if(identityTenantType == null)
			{
				throw new InvalidOperationException("The given type is not an identity tenant type.");
			}

			Type keyType = identityPermissionType.GenericTypeArguments[0];

			Type identityRoleType = FindGenericBaseType(roleType, typeof(IdentityRole<>));
			if(identityRoleType == null)
			{
				throw new InvalidOperationException("The given type is not an identity role type.");
			}

			Type permissionStoreType;
			Type tenantsStoreType;
			Type tenantUsersStoreType;

			Type identityContext = FindGenericBaseType(contextType, typeof(IdentityPermissionsDbContext<,,,,,,,,,,,>));
			if(identityContext == null)
			{
				// If its a custom DbContext, we can only add the default POCOs
				permissionStoreType = typeof(PermissionStore<,,,>).MakeGenericType(permissionType, roleType, contextType, keyType);
				tenantsStoreType = typeof(TenantStore<,,>).MakeGenericType(permissionType, contextType, keyType);
				tenantUsersStoreType = typeof(TenantUserStore<,,>).MakeGenericType(userType, contextType, keyType);
			}
			else
			{
				permissionStoreType = typeof(PermissionStore<,,,,>).MakeGenericType(permissionType, roleType, contextType,
					identityContext.GenericTypeArguments[4],
					identityContext.GenericTypeArguments[10]);

				tenantsStoreType = typeof(TenantStore<,,,,>).MakeGenericType(tenantType, roleType, contextType,
					identityContext.GenericTypeArguments[4],
					identityContext.GenericTypeArguments[11]);

				tenantUsersStoreType = typeof(TenantUserStore<,,>).MakeGenericType(userType, contextType,
					identityContext.GenericTypeArguments[4]);
			}

			services.TryAddScoped(typeof(IPermissionStore<>).MakeGenericType(permissionType), permissionStoreType);
			services.TryAddScoped(typeof(ITenantStore<>).MakeGenericType(tenantType), tenantsStoreType);
			services.TryAddScoped(typeof(ITenantUserStore<>).MakeGenericType(userType), tenantUsersStoreType);
		}

		private static Type FindGenericBaseType(Type currentType, Type genericBaseType)
		{
			Type type = currentType;
			while(type != null)
			{
				Type genericType = type.IsGenericType ? type.GetGenericTypeDefinition() : null;
				if(genericType != null && genericType == genericBaseType)
				{
					return type;
				}

				type = type.BaseType;
			}

			return null;
		}
	}
}
