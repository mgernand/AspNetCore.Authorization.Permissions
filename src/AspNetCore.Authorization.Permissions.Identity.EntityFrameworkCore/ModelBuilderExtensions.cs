namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore
{
	using System;
	using System.Linq;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore.Configuration.Identity;
	using MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore.Configuration.Permissions;
	using MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.Model;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Infrastructure;
	using Microsoft.EntityFrameworkCore.Metadata.Internal;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     Extensions methods for the <see cref="ModelBuilder" /> type.
	/// </summary>
	[PublicAPI]
	public static class ModelBuilderExtensions
	{
		/// <summary>
		///     Applies the identity users, roles and permissions entity configurations.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		/// <param name="configureOptions"></param>
		/// <returns></returns>
		public static ModelBuilder ApplyPermissionsWithIdentity(this ModelBuilder builder, DbContext context, Action<PermissionModelBuilderOptions> configureOptions = null)
		{
			return builder.ApplyPermissionsWithIdentity<PermissionsUser, PermissionsRole, PermissionsPermission, PermissionsTenant, string>(context, configureOptions);
		}

		/// <summary>
		///     Applies the identity users, roles and permissions entity configurations.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		/// <typeparam name="TPermission"></typeparam>
		/// <typeparam name="TTenant"></typeparam>
		/// <returns></returns>
		public static ModelBuilder ApplyPermissionsWithIdentity<TPermission, TTenant>(this ModelBuilder builder, DbContext context)
			where TPermission : PermissionsPermission
			where TTenant : PermissionsTenant
		{
			return builder.ApplyPermissionsWithIdentity<PermissionsUser, PermissionsRole, TPermission, TTenant, string>(context);
		}

		/// <summary>
		///     Applies the identity users, roles and permissions entity configurations.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		/// <typeparam name="TUser"></typeparam>
		/// <typeparam name="TPermission"></typeparam>
		/// <typeparam name="TTenant"></typeparam>
		/// <returns></returns>
		public static ModelBuilder ApplyPermissionsWithIdentity<TUser, TPermission, TTenant>(this ModelBuilder builder, DbContext context)
			where TUser : PermissionsUser
			where TPermission : PermissionsPermission
			where TTenant : PermissionsTenant
		{
			return builder.ApplyPermissionsWithIdentity<TUser, PermissionsRole, TPermission, TTenant, string>(context);
		}

		/// <summary>
		///     Applies the identity users, roles and permissions entity configurations.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		/// <param name="configureOptions"></param>
		/// <typeparam name="TUser"></typeparam>
		/// <typeparam name="TRole"></typeparam>
		/// <typeparam name="TPermission"></typeparam>
		/// <typeparam name="TTenant"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <returns></returns>
		public static ModelBuilder ApplyPermissionsWithIdentity<TUser, TRole, TPermission, TTenant, TKey>(this ModelBuilder builder, DbContext context, Action<PermissionModelBuilderOptions> configureOptions = null)
			where TUser : PermissionsUser<TKey>
			where TRole : PermissionsRole<TKey>
			where TPermission : PermissionsPermission<TKey>
			where TTenant : PermissionsTenant<TKey>
			where TKey : IEquatable<TKey>
		{
			return builder.ApplyPermissionsWithIdentity<TUser, TRole, TPermission, TTenant, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>, PermissionsRolePermission<TKey>, PermissionsTenantRole<TKey>>(context, configureOptions);
		}

		/// <summary>
		///     Applies the identity users, roles and permissions entity configurations.
		/// </summary>
		/// <typeparam name="TUser"></typeparam>
		/// <typeparam name="TRole"></typeparam>
		/// <typeparam name="TPermission"></typeparam>
		/// <typeparam name="TTenant"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TUserClaim"></typeparam>
		/// <typeparam name="TUserRole"></typeparam>
		/// <typeparam name="TUserLogin"></typeparam>
		/// <typeparam name="TRoleClaim"></typeparam>
		/// <typeparam name="TUserToken"></typeparam>
		/// <typeparam name="TRolePermission"></typeparam>
		/// <typeparam name="TTenantRole"></typeparam>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		/// <param name="configureOptions"></param>
		/// <returns></returns>
		public static ModelBuilder ApplyPermissionsWithIdentity<TUser, TRole, TPermission, TTenant, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken, TRolePermission, TTenantRole>(this ModelBuilder builder, DbContext context, Action<PermissionModelBuilderOptions> configureOptions = null)
			where TUser : PermissionsUser<TKey>
			where TRole : PermissionsRole<TKey>
			where TPermission : PermissionsPermission<TKey>
			where TTenant : PermissionsTenant<TKey>
			where TKey : IEquatable<TKey>
			where TUserClaim : IdentityUserClaim<TKey>
			where TUserRole : IdentityUserRole<TKey>
			where TUserLogin : IdentityUserLogin<TKey>
			where TRoleClaim : IdentityRoleClaim<TKey>
			where TUserToken : IdentityUserToken<TKey>
			where TRolePermission : PermissionsRolePermission<TKey>
			where TTenantRole : PermissionsTenantRole<TKey>
		{
			builder.ApplyIdentityUser<TUser, TKey, TUserClaim, TUserLogin, TUserToken>(context, configureOptions);
			builder.ApplyIdentityUserRoles<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>(context, configureOptions);
			builder.ApplyIdentityPermissions<TUser, TRole, TPermission, TTenant, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken, TRolePermission, TTenantRole>(context, configureOptions);

			return builder;
		}

		/// <summary>
		///     Applies the permissions entity configurations.
		/// </summary>
		/// <typeparam name="TUser"></typeparam>
		/// <typeparam name="TRole"></typeparam>
		/// <typeparam name="TPermission"></typeparam>
		/// <typeparam name="TTenant"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TUserClaim"></typeparam>
		/// <typeparam name="TUserRole"></typeparam>
		/// <typeparam name="TUserLogin"></typeparam>
		/// <typeparam name="TRoleClaim"></typeparam>
		/// <typeparam name="TUserToken"></typeparam>
		/// <typeparam name="TRolePermission"></typeparam>
		/// <typeparam name="TTenantRole"></typeparam>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static ModelBuilder ApplyIdentityPermissions<TUser, TRole, TPermission, TTenant, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken, TRolePermission, TTenantRole>(this ModelBuilder builder, DbContext context, Action<PermissionModelBuilderOptions> configureOptions = null)
			where TUser : PermissionsUser<TKey>
			where TRole : PermissionsRole<TKey>
			where TPermission : PermissionsPermission<TKey>
			where TTenant : PermissionsTenant<TKey>
			where TKey : IEquatable<TKey>
			where TUserClaim : IdentityUserClaim<TKey>
			where TUserRole : IdentityUserRole<TKey>
			where TUserLogin : IdentityUserLogin<TKey>
			where TRoleClaim : IdentityRoleClaim<TKey>
			where TUserToken : IdentityUserToken<TKey>
			where TRolePermission : PermissionsRolePermission<TKey>
			where TTenantRole : PermissionsTenantRole<TKey>
		{
			PermissionModelBuilderOptions options = new PermissionModelBuilderOptions();
			configureOptions?.Invoke(options);

			StoreOptions storeOptions = context.GetStoreOptions();
			int maxKeyLength = storeOptions?.MaxLengthForKeys ?? 0;
			bool encryptPersonalData = storeOptions?.ProtectPersonalData ?? false;
			PersonalDataConverter converter = null;

			if(encryptPersonalData)
			{
				converter = new PersonalDataConverter(context.GetService<IPersonalDataProtector>());
			}

			builder.ApplyConfiguration(new PermissionConfiguration<TPermission, TRolePermission, TKey>
			{
				Table = options.PermissionsTable,
				MaxKeyLength = maxKeyLength
			});
			builder.ApplyConfiguration(new RolePermissionConfiguration<TRolePermission, TKey>
			{
				Table = options.RolePermissionsTable
			});
			builder.ApplyConfiguration(new TenantUserConfiguration<TUser, TTenant, TKey>());
			builder.ApplyConfiguration(new TenantConfiguration<TTenant, TKey>
			{
				Table = options.TenantsTable,
				PersonalDataConverter = converter
			});
			builder.ApplyConfiguration(new TenantRoleConfiguration<TTenantRole, TKey>
			{
				Table = options.TenantRolesTable
			});

			return builder;
		}

		/// <summary>
		///     Applies the user with roles entity configurations.
		/// </summary>
		/// <typeparam name="TUser"></typeparam>
		/// <typeparam name="TRole"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TUserClaim"></typeparam>
		/// <typeparam name="TUserRole"></typeparam>
		/// <typeparam name="TUserLogin"></typeparam>
		/// <typeparam name="TRoleClaim"></typeparam>
		/// <typeparam name="TUserToken"></typeparam>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public static ModelBuilder ApplyIdentityUserRoles<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>(this ModelBuilder builder, DbContext context, Action<PermissionModelBuilderOptions> configureOptions = null)
			where TUser : IdentityUser<TKey>
			where TRole : IdentityRole<TKey>
			where TKey : IEquatable<TKey>
			where TUserClaim : IdentityUserClaim<TKey>
			where TUserRole : IdentityUserRole<TKey>
			where TUserLogin : IdentityUserLogin<TKey>
			where TRoleClaim : IdentityRoleClaim<TKey>
			where TUserToken : IdentityUserToken<TKey>
		{
			PermissionModelBuilderOptions options = new PermissionModelBuilderOptions();
			configureOptions?.Invoke(options);

			builder.ApplyConfiguration(new UserHasRolesConfiguration<TUser, TUserRole, TKey>());
			builder.ApplyConfiguration(new RoleConfiguration<TRole, TUserRole, TRoleClaim, TKey>
			{
				Table = options.RolesTable
			});
			builder.ApplyConfiguration(new RoleClaimConfiguration<TRoleClaim, TKey>
			{
				Table = options.RoleClaimsTable
			});
			builder.ApplyConfiguration(new UserRoleConfiguration<TUserRole, TKey>
			{
				Table = options.UserRolesTable
			});

			return builder;
		}

		/// <summary>
		///     Applies the user entity configurations.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		/// <param name="configureOptions"></param>
		/// <returns></returns>
		public static ModelBuilder ApplyIdentityUser<TUser, TKey, TUserClaim, TUserLogin, TUserToken>(this ModelBuilder builder, DbContext context, Action<PermissionModelBuilderOptions> configureOptions = null)
			where TUser : IdentityUser<TKey>
			where TKey : IEquatable<TKey>
			where TUserClaim : IdentityUserClaim<TKey>
			where TUserLogin : IdentityUserLogin<TKey>
			where TUserToken : IdentityUserToken<TKey>
		{
			PermissionModelBuilderOptions options = new PermissionModelBuilderOptions();
			configureOptions?.Invoke(options);

			StoreOptions storeOptions = context.GetStoreOptions();
			int maxKeyLength = storeOptions?.MaxLengthForKeys ?? 0;
			bool encryptPersonalData = storeOptions?.ProtectPersonalData ?? false;
			PersonalDataConverter converter = null;

			if(encryptPersonalData)
			{
				converter = new PersonalDataConverter(context.GetService<IPersonalDataProtector>());
			}

			builder.ApplyConfiguration(new UserConfiguration<TUser, TUserClaim, TUserLogin, TUserToken, TKey>
			{
				Table = options.UserTable,
				PersonalDataConverter = converter
			});
			builder.ApplyConfiguration(new UserClaimConfiguration<TUserClaim, TKey>
			{
				Table = options.UserClaimsTable
			});
			builder.ApplyConfiguration(new UserLoginConfiguration<TUserLogin, TKey>
			{
				Table = options.UserLoginsTable,
				MaxKeyLength = maxKeyLength
			});
			builder.ApplyConfiguration(new UserTokenConfiguration<TUserToken, TKey>
			{
				Table = options.UserTokensTable,
				MaxKeyLength = maxKeyLength,
				PersonalDataConverter = converter
			});

			return builder;
		}

		private static StoreOptions GetStoreOptions(this DbContext context)
		{
			return context.GetService<IDbContextOptions>()
				.Extensions.OfType<CoreOptionsExtension>()
				.FirstOrDefault()?.ApplicationServiceProvider
				?.GetService<IOptions<IdentityOptions>>()
				?.Value?.Stores;
		}

		private class PersonalDataConverter : ValueConverter<string, string>
		{
			public PersonalDataConverter(IPersonalDataProtector protector)
				: base(
					s => protector.Protect(s),
					s => protector.Unprotect(s))
			{
			}
		}
	}
}
