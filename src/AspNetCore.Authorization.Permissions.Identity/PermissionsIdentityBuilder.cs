namespace AspNetCore.Authorization.Permissions.Identity
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.DependencyInjection;

	/// <inheritdoc />
	[PublicAPI]
	public class PermissionsIdentityBuilder : IdentityBuilder
	{
		/// <inheritdoc />
		public PermissionsIdentityBuilder(IdentityBuilder builder, Type permissionType, Type tenantType)
			: base(builder.UserType, builder.RoleType, builder.Services)
		{
			this.PermissionType = permissionType;
			this.TenantType = tenantType;
		}

		/// <summary>
		///     Gets the configured permission type.
		/// </summary>
		public Type PermissionType { get; }

		/// <summary>
		///     Gets the configured tenant type.
		/// </summary>
		public Type TenantType { get; }

		/// <summary>
		///     Adds a <see cref="PermissionManager{TUser}" /> for the <see cref="PermissionType" />.
		/// </summary>
		/// <typeparam name="TPermissionManager">The type of the permission manager to add.</typeparam>
		public virtual PermissionsIdentityBuilder AddPermissionManager<TPermissionManager>() where TPermissionManager : class
		{
			Type userManagerType = typeof(PermissionManager<>).MakeGenericType(this.PermissionType);
			Type customType = typeof(TPermissionManager);
			if(!userManagerType.IsAssignableFrom(customType))
			{
				throw new InvalidOperationException($"Invalid PermissionManager: '{this.PermissionType.Name}'");
			}

			if(userManagerType != customType)
			{
				this.Services.AddScoped(customType, services => services.GetRequiredService(userManagerType));
			}

			return this.AddScoped(userManagerType, customType);
		}

		/// <summary>
		///     Adds a <see cref="TenantManager{TTenant}" /> for the <see cref="TenantType" />.
		/// </summary>
		/// <typeparam name="TTenantManager">The type of the tenant manager to add.</typeparam>
		public virtual PermissionsIdentityBuilder AddTenantManager<TTenantManager>() where TTenantManager : class
		{
			Type userManagerType = typeof(TenantManager<>).MakeGenericType(this.TenantType);
			Type customType = typeof(TTenantManager);
			if(!userManagerType.IsAssignableFrom(customType))
			{
				throw new InvalidOperationException($"Invalid TenantManager: '{this.TenantType.Name}'");
			}

			if(userManagerType != customType)
			{
				this.Services.AddScoped(customType, services => services.GetRequiredService(userManagerType));
			}

			return this.AddScoped(userManagerType, customType);
		}

		/// <summary>
		///     Adds an <see cref="IPermissionValidator{TPermission}" /> for the <see cref="PermissionType" />.
		/// </summary>
		/// <typeparam name="TValidator">The permission validator type.</typeparam>
		public virtual PermissionsIdentityBuilder AddPermissionValidator<TValidator>()
			where TValidator : class
		{
			return this.AddScoped(typeof(IPermissionValidator<>).MakeGenericType(this.PermissionType), typeof(TValidator));
		}

		/// <summary>
		///     Adds an <see cref="ITenantValidator{TPermission}" /> for the <see cref="TenantType" />.
		/// </summary>
		/// <typeparam name="TValidator">The tenant validator type.</typeparam>
		public virtual PermissionsIdentityBuilder AddTenantValidator<TValidator>()
			where TValidator : class
		{
			return this.AddScoped(typeof(ITenantValidator<>).MakeGenericType(this.TenantType), typeof(TValidator));
		}

		private PermissionsIdentityBuilder AddScoped(Type serviceType, Type concreteType)
		{
			this.Services.AddScoped(serviceType, concreteType);
			return this;
		}
	}
}
