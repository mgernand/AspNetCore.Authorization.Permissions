﻿namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using JetBrains.Annotations;
	using MadEyeMatt.Extensions.Identity.Permissions;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.DependencyInjection;

	/// <inheritdoc />
	[PublicAPI]
	public class PermissionIdentityBuilder : IdentityBuilder
	{
		/// <inheritdoc />
		public PermissionIdentityBuilder(IdentityBuilder builder, Type tenantType, Type permissionType)
			: this(builder, permissionType)
		{
			this.TenantType = tenantType;
		}

		/// <inheritdoc />
        public PermissionIdentityBuilder(IdentityBuilder builder, Type permissionType)
			: base(builder.UserType, builder.RoleType, builder.Services)
        {
			this.PermissionType = permissionType;
        }

		/// <summary>
		///     Gets the configured tenant type.
		/// </summary>
		public Type TenantType { get; }

        /// <summary>
        ///     Gets the configured permission type.
        /// </summary>
        public Type PermissionType { get; }

		/// <summary>
		///     Adds a <see cref="PermissionManager{TUser}" /> for the <see cref="PermissionType" />.
		/// </summary>
		/// <typeparam name="TPermissionManager">The type of the permission manager to add.</typeparam>
		public virtual PermissionIdentityBuilder AddPermissionManager<TPermissionManager>() where TPermissionManager : class
		{
			Type permissionManagerType = typeof(PermissionManager<>).MakeGenericType(this.PermissionType);
			Type permissionManagerInterfaceType = typeof(PermissionManager<>).MakeGenericType(this.PermissionType);

			Type customType = typeof(TPermissionManager);
			if(!permissionManagerType.IsAssignableFrom(customType))
			{
				throw new InvalidOperationException($"Invalid PermissionManager: '{this.PermissionType.Name}'");
			}

			if(permissionManagerType != customType)
			{
				this.Services.AddScoped(customType, services => services.GetRequiredService(permissionManagerType));
			}

			this.Services.AddScoped(permissionManagerInterfaceType, serviceProvider => serviceProvider.GetRequiredService(permissionManagerType));

			return this.AddScoped(permissionManagerType, customType);
		}

		/// <summary>
		///     Adds a <see cref="TenantManager{TTenant}" /> for the <see cref="TenantType" />.
		/// </summary>
		/// <typeparam name="TTenantManager">The type of the tenant manager to add.</typeparam>
		public virtual PermissionIdentityBuilder AddTenantManager<TTenantManager>() where TTenantManager : class
		{
			Type tenantManagerType = typeof(TenantManager<>).MakeGenericType(this.TenantType);
			Type tenantManagerInterfaceType = typeof(TenantManager<>).MakeGenericType(this.TenantType);

			Type customType = typeof(TTenantManager);
			if(!tenantManagerType.IsAssignableFrom(customType))
			{
				throw new InvalidOperationException($"Invalid TenantManager: '{this.TenantType.Name}'");
			}

			if(tenantManagerType != customType)
			{
				this.Services.AddScoped(customType, services => services.GetRequiredService(tenantManagerType));
			}

			this.Services.AddScoped(tenantManagerInterfaceType, serviceProvider => serviceProvider.GetRequiredService(tenantManagerType));

			return this.AddScoped(tenantManagerType, customType);
		}

		/// <summary>
		///     Adds an <see cref="IPermissionValidator{TPermission}" /> for the <see cref="PermissionType" />.
		/// </summary>
		/// <typeparam name="TValidator">The permission validator type.</typeparam>
		public virtual PermissionIdentityBuilder AddPermissionValidator<TValidator>()
			where TValidator : class
		{
			return this.AddScoped(typeof(IPermissionValidator<>).MakeGenericType(this.PermissionType), typeof(TValidator));
		}

		/// <summary>
		///     Adds an <see cref="ITenantValidator{TPermission}" /> for the <see cref="TenantType" />.
		/// </summary>
		/// <typeparam name="TValidator">The tenant validator type.</typeparam>
		public virtual PermissionIdentityBuilder AddTenantValidator<TValidator>()
			where TValidator : class
		{
			return this.AddScoped(typeof(ITenantValidator<>).MakeGenericType(this.TenantType), typeof(TValidator));
		}

		private PermissionIdentityBuilder AddScoped(Type serviceType, Type concreteType)
		{
			this.Services.AddScoped(serviceType, concreteType);
			return this;
		}
	}
}
