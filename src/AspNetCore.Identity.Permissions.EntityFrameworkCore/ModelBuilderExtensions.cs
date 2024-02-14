namespace MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore.Configuration.Identity;
	using MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore.Configuration.Permissions;
	using MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore.Properties;
	using MadEyeMatt.Extensions.Identity.Permissions.Stores;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
		public static ModelBuilder ApplyPermissions(this ModelBuilder builder, DbContext context, Action<PermissionModelBuilderOptions> configureOptions = null)
		{
			return builder.ApplyPermissions<IdentityUser, IdentityRole, IdentityPermission, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>, IdentityRolePermission<string>>(context, configureOptions);
		}

		/// <summary>
		///     Applies the identity tenants, users, roles and permissions entity configurations.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		/// <param name="configureOptions"></param>
		/// <returns></returns>
		public static ModelBuilder ApplyPermissionsWithTenant(this ModelBuilder builder, DbContext context, Action<PermissionModelBuilderOptions> configureOptions = null)
		{
			return builder.ApplyPermissionsWithTenant<IdentityTenant, IdentityTenantUser, IdentityRole, IdentityPermission, string, IdentityTenantRole<string>, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>, IdentityRolePermission<string>>(context, configureOptions);
		}

		/// <summary>
		///     Applies the identity users, roles and permissions entity configurations.
		/// </summary>
		/// <typeparam name="TUser"></typeparam>
		/// <typeparam name="TRole"></typeparam>
		/// <typeparam name="TPermission"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TUserClaim"></typeparam>
		/// <typeparam name="TUserRole"></typeparam>
		/// <typeparam name="TUserLogin"></typeparam>
		/// <typeparam name="TRoleClaim"></typeparam>
		/// <typeparam name="TUserToken"></typeparam>
		/// <typeparam name="TRolePermission"></typeparam>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		/// <param name="configureOptions"></param>
		/// <returns></returns>
		public static ModelBuilder ApplyPermissions<TUser, TRole, TPermission, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken, TRolePermission>(this ModelBuilder builder, DbContext context, Action<PermissionModelBuilderOptions> configureOptions = null)
			where TUser : IdentityUser<TKey>
			where TRole : IdentityRole<TKey>
			where TPermission : IdentityPermission<TKey>
			where TKey : IEquatable<TKey>
			where TUserClaim : IdentityUserClaim<TKey>
			where TUserRole : IdentityUserRole<TKey>
			where TUserLogin : IdentityUserLogin<TKey>
			where TRoleClaim : IdentityRoleClaim<TKey>
			where TUserToken : IdentityUserToken<TKey>
			where TRolePermission : IdentityRolePermission<TKey>
		{
			builder.ApplyIdentityUser<TUser, TKey, TUserClaim, TUserLogin, TUserToken>(context, configureOptions);
			builder.ApplyIdentityUserRoles<TUser, TRole, TKey, TUserRole, TRoleClaim>(context, configureOptions);
			builder.ApplyIdentityPermissions<TPermission, TKey, TRolePermission>(context, configureOptions);

			return builder;
		}

		/// <summary>
		///     Applies the identity users, roles and permissions entity configurations.
		/// </summary>
		/// <typeparam name="TTenant"></typeparam>
		/// <typeparam name="TUser"></typeparam>
		/// <typeparam name="TRole"></typeparam>
		/// <typeparam name="TPermission"></typeparam>
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
		public static ModelBuilder ApplyPermissionsWithTenant<TTenant, TUser, TRole, TPermission, TKey, TTenantRole, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken, TRolePermission>(this ModelBuilder builder, DbContext context, Action<PermissionModelBuilderOptions> configureOptions = null)
			where TTenant : IdentityTenant<TKey>
			where TUser : IdentityTenantUser<TKey>
			where TRole : IdentityRole<TKey>
			where TPermission : IdentityPermission<TKey>
			where TKey : IEquatable<TKey>
			where TTenantRole : IdentityTenantRole<TKey>
			where TUserClaim : IdentityUserClaim<TKey>
			where TUserRole : IdentityUserRole<TKey>
			where TUserLogin : IdentityUserLogin<TKey>
			where TRoleClaim : IdentityRoleClaim<TKey>
			where TUserToken : IdentityUserToken<TKey>
			where TRolePermission : IdentityRolePermission<TKey>
		{
			builder.ApplyIdentityUser<TUser, TKey, TUserClaim, TUserLogin, TUserToken>(context, configureOptions);
			builder.ApplyIdentityUserRoles<TUser, TRole, TKey, TUserRole, TRoleClaim>(context, configureOptions);
			builder.ApplyIdentityPermissions<TPermission, TKey, TRolePermission>(context, configureOptions);
			builder.ApplyIdentityTenant<TTenant, TTenantRole, TKey, TUser>(context, configureOptions);

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
			int maxKeyLength = storeOptions.MaxLengthForKeys();

			bool encryptPersonalData = storeOptions.EncryptPersonalData();
			PersonalDataConverter converter = null;

			if(encryptPersonalData)
			{
				converter = new PersonalDataConverter(context.GetService<IPersonalDataProtector>());
			}

			builder.ApplyConfiguration(new UserConfiguration<TUser, TUserClaim, TUserLogin, TUserToken, TKey>
			{
				Table = options.UserTable,
				MaxKeyLength = maxKeyLength,
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

		/// <summary>
		///     Applies the user with roles entity configurations.
		/// </summary>
		/// <typeparam name="TUser"></typeparam>
		/// <typeparam name="TRole"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TUserRole"></typeparam>
		/// <typeparam name="TRoleClaim"></typeparam>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		/// <param name="configureOptions"></param>
		/// <returns></returns>
		public static ModelBuilder ApplyIdentityUserRoles<TUser, TRole, TKey, TUserRole, TRoleClaim>(this ModelBuilder builder, DbContext context, Action<PermissionModelBuilderOptions> configureOptions = null)
			where TUser : IdentityUser<TKey>
			where TRole : IdentityRole<TKey>
			where TKey : IEquatable<TKey>
			where TUserRole : IdentityUserRole<TKey>
			where TRoleClaim : IdentityRoleClaim<TKey>
		{
			PermissionModelBuilderOptions options = new PermissionModelBuilderOptions();
			configureOptions?.Invoke(options);

			StoreOptions storeOptions = context.GetStoreOptions();
			int maxKeyLength = storeOptions.MaxLengthForKeys();

            builder.ApplyConfiguration(new UserHasRolesConfiguration<TUser, TUserRole, TKey>());

			builder.ApplyConfiguration(new RoleConfiguration<TRole, TUserRole, TRoleClaim, TKey>
			{
				Table = options.RolesTable,
				MaxKeyLength = maxKeyLength
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
		///     Applies the permissions entity configurations.
		/// </summary>
		/// <typeparam name="TPermission"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TRolePermission"></typeparam>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		/// <param name="configureOptions"></param>
		/// <returns></returns>
		public static ModelBuilder ApplyIdentityPermissions<TPermission, TKey, TRolePermission>(this ModelBuilder builder, DbContext context, Action<PermissionModelBuilderOptions> configureOptions = null)
			where TPermission : IdentityPermission<TKey>
			where TKey : IEquatable<TKey>
			where TRolePermission : IdentityRolePermission<TKey>
		{
			PermissionModelBuilderOptions options = new PermissionModelBuilderOptions();
			configureOptions?.Invoke(options);

			StoreOptions storeOptions = context.GetStoreOptions();
			int maxKeyLength = storeOptions.MaxLengthForKeys();

			builder.ApplyConfiguration(new PermissionConfiguration<TPermission, TRolePermission, TKey>
			{
				Table = options.PermissionsTable,
				MaxKeyLength = maxKeyLength
			});

			builder.ApplyConfiguration(new RolePermissionConfiguration<TRolePermission, TKey>
			{
				Table = options.RolePermissionsTable
			});

			return builder;
		}

		/// <summary>
		///     Applies the tenants entity configurations.
		/// </summary>
		/// <typeparam name="TTenant"></typeparam>
		/// <typeparam name="TTenantRole"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TUser"></typeparam>
		/// <param name="builder"></param>
		/// <param name="context"></param>
		/// <param name="configureOptions"></param>
		/// <returns></returns>
		public static ModelBuilder ApplyIdentityTenant<TTenant, TTenantRole, TKey, TUser>(this ModelBuilder builder, DbContext context, Action<PermissionModelBuilderOptions> configureOptions = null)
			where TTenant : IdentityTenant<TKey>
			where TTenantRole : IdentityTenantRole<TKey>
			where TKey : IEquatable<TKey>
			where TUser : IdentityTenantUser<TKey>
		{
			PermissionModelBuilderOptions options = new PermissionModelBuilderOptions();
			configureOptions?.Invoke(options);

			StoreOptions storeOptions = context.GetStoreOptions();
			int maxKeyLength = storeOptions.MaxLengthForKeys();

            bool encryptPersonalData = storeOptions?.ProtectPersonalData ?? false;
			PersonalDataConverter converter = null;

			if(encryptPersonalData)
			{
				converter = new PersonalDataConverter(context.GetService<IPersonalDataProtector>());
			}

			builder.ApplyConfiguration(new TenantUserConfiguration<TUser, TTenant, TKey>());

			builder.ApplyConfiguration(new TenantConfiguration<TTenant, TKey>
			{
				Table = options.TenantsTable,
				MaxKeyLength = maxKeyLength,
				PersonalDataConverter = converter
			});

			builder.ApplyConfiguration(new TenantRoleConfiguration<TTenantRole, TKey>
			{
				Table = options.TenantRolesTable
			});

			return builder;
		}

		internal static void ApplyProtectedPersonalDataConverter<TEntity>(this EntityTypeBuilder<TEntity> builder, ValueConverter<string, string> converter) where TEntity : class
		{
			IEnumerable<PropertyInfo> personalDataProperties = typeof(TEntity)
				.GetProperties()
				.Where(prop => prop.IsDefined(typeof(ProtectedPersonalDataAttribute)));

			foreach (PropertyInfo p in personalDataProperties)
			{
				if (p.PropertyType != typeof(string))
				{
					throw new InvalidOperationException(Resources.CanOnlyProtectStrings);
				}

				builder.Property(typeof(string), p.Name).HasConversion(converter);
			}
        }

        private static StoreOptions GetStoreOptions(this DbContext context)
		{
			return context.GetService<IDbContextOptions>()
				.Extensions.OfType<CoreOptionsExtension>()
				.FirstOrDefault()?.ApplicationServiceProvider
				?.GetService<IOptions<IdentityOptions>>()
				?.Value.Stores;
		}

		private static int MaxLengthForKeys(this StoreOptions storeOptions, int fallbackMaxLength = 128)
		{
			int maxKeyLength = storeOptions?.MaxLengthForKeys ?? 0;
			if (maxKeyLength == 0)
			{
				maxKeyLength = fallbackMaxLength;
			}

			return maxKeyLength;
		}

		private static bool EncryptPersonalData(this StoreOptions storeOptions)
		{
			bool encryptPersonalData = storeOptions?.ProtectPersonalData ?? false;
			return encryptPersonalData;
		}

        private sealed class PersonalDataConverter : ValueConverter<string, string>
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
