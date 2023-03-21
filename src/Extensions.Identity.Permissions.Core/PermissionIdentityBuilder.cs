namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     Helper functions for configuring permissions identity services.
	/// </summary>
	[PublicAPI]
	public class PermissionIdentityBuilder : IdentityBuilder
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="PermissionIdentityBuilder" /> type..
		/// </summary>
		/// <param name="user"></param>
		/// <param name="role"></param>
		/// <param name="permission">The <see cref="Type" /> to use for the permission.</param>
		/// <param name="services">The <see cref="IServiceCollection" /> to attach to.</param>
		public PermissionIdentityBuilder(Type user, Type role, Type permission, IServiceCollection services) : base(user, role, services)
		{
			if(permission.IsValueType)
			{
				throw new ArgumentException(@"Permission type can't be a value type.", nameof(permission));
			}

			this.PermissionType = permission;
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="PermissionIdentityBuilder" /> type..
		/// </summary>
		/// <param name="tenant">>The <see cref="Type" /> to use for the tenant.</param>
		/// <param name="user"></param>
		/// <param name="role"></param>
		/// <param name="permission">The <see cref="Type" /> to use for the permission.</param>
		/// <param name="services">The <see cref="IServiceCollection" /> to attach to.</param>
		public PermissionIdentityBuilder(Type tenant, Type user, Type role, Type permission, IServiceCollection services)
			: this(user, role, permission, services)
		{
			if(tenant.IsValueType)
			{
				throw new ArgumentException(@"Tenant type can't be a value type.", nameof(tenant));
			}

			this.TenantType = tenant;
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
		///     Adds an <see cref="IPermissionValidator{TPermission}" /> for the <see cref="PermissionType" />.
		/// </summary>
		/// <typeparam name="TValidator">The permission validator type.</typeparam>
		public virtual PermissionIdentityBuilder AddPermissionValidator<TValidator>() where TValidator : class
		{
			return this.AddScoped(typeof(IPermissionValidator<>).MakeGenericType(this.PermissionType), typeof(TValidator));
		}

		/// <summary>
		///     Adds an <see cref="ITenantValidator{TPermission}" /> for the <see cref="TenantType" />.
		/// </summary>
		/// <typeparam name="TValidator">The tenant validator type.</typeparam>
		public virtual PermissionIdentityBuilder AddTenantValidator<TValidator>() where TValidator : class
		{
			return this.AddScoped(typeof(ITenantValidator<>).MakeGenericType(this.TenantType), typeof(TValidator));
		}

		/// <summary>
		///     Adds an <see cref="IPermissionStore{TUser}" /> for the <see cref="PermissionType" />.
		/// </summary>
		/// <typeparam name="TStore">The permission store type.</typeparam>
		public virtual PermissionIdentityBuilder AddPermissionStore<TStore>() where TStore : class
		{
			return this.AddScoped(typeof(IPermissionStore<>).MakeGenericType(this.PermissionType), typeof(TStore));
		}

		/// <summary>
		///     Adds an <see cref="ITenantStore{TUser}" /> for the <see cref="TenantType" />.
		/// </summary>
		/// <typeparam name="TStore">The tenant store type.</typeparam>
		public virtual PermissionIdentityBuilder AddTenantStore<TStore>() where TStore : class
		{
			return this.AddScoped(typeof(ITenantStore<>).MakeGenericType(this.TenantType), typeof(TStore));
		}

		/// <summary>
		///     Adds a <see cref="PermissionManager{TPermission}" /> for the <see cref="PermissionType" />.
		/// </summary>
		/// <typeparam name="TPermissionManager">The type of the permission manager to add.</typeparam>
		public virtual PermissionIdentityBuilder AddPermissionManager<TPermissionManager>() where TPermissionManager : class
		{
			Type permissionManagerType = typeof(PermissionManager<>).MakeGenericType(this.PermissionType);
			Type customType = typeof(TPermissionManager);

			if(!permissionManagerType.IsAssignableFrom(customType))
			{
				throw new InvalidOperationException($"Invalid PermissionManager: '{this.PermissionType.Name}'");
			}

			if(permissionManagerType != customType)
			{
				this.Services.AddScoped(customType, services => services.GetRequiredService(permissionManagerType));
			}

			return this.AddScoped(permissionManagerType, customType);
		}

		/// <summary>
		///     Adds a <see cref="TenantManager{TTenant}" /> for the <see cref="TenantType" />.
		/// </summary>
		/// <typeparam name="TTenantManager">The type of the tenant manager to add.</typeparam>
		public virtual PermissionIdentityBuilder AddTenantManager<TTenantManager>() where TTenantManager : class
		{
			if(this.TenantType is null)
			{
				return this;
			}

			Type tenantManagerType = typeof(TenantManager<>).MakeGenericType(this.TenantType);
			Type customType = typeof(TTenantManager);

			if(!tenantManagerType.IsAssignableFrom(customType))
			{
				throw new InvalidOperationException($"Invalid TenantManager: '{this.TenantType.Name}'");
			}

			if(tenantManagerType != customType)
			{
				this.Services.AddScoped(customType, services => services.GetRequiredService(tenantManagerType));
			}

			return this.AddScoped(tenantManagerType, customType);
		}

		private PermissionIdentityBuilder AddScoped(Type serviceType, Type concreteType)
		{
			this.Services.AddScoped(serviceType, concreteType);
			return this;
		}
	}
}
