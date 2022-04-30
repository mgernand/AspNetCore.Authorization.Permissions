namespace AspNetCore.Authorization.Permissions.Identity
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Provides validation services for tenant classes.
	/// </summary>
	/// <typeparam name="TTenant">The type encapsulating a tenant.</typeparam>
	[PublicAPI]
	public class TenantValidator<TTenant> : ITenantValidator<TTenant> where TTenant : class, ITenant
	{
		/// <summary>
		///     Creates a new instance of <see cref="TenantValidator{TTenant}" />.
		/// </summary>
		/// <param name="errors">The <see cref="IdentityErrorDescriber" /> used to provider error messages.</param>
		public TenantValidator(IdentityErrorDescriber errors = null)
		{
			this.Describer = errors ?? new IdentityErrorDescriber();
		}

		/// <summary>
		///     Gets the <see cref="IdentityErrorDescriber" /> used to provider error messages for the current
		///     <see cref="UserValidator{TUser}" />.
		/// </summary>
		/// <value>
		///     The <see cref="IdentityErrorDescriber" /> used to provider error messages for the current
		///     <see cref="UserValidator{TUser}" />.
		/// </value>
		public IdentityErrorDescriber Describer { get; }

		/// <inheritdoc />
		public async Task<IdentityResult> ValidateAsync(TenantManager<TTenant> manager, TTenant tenant)
		{
			if(manager == null)
			{
				throw new ArgumentNullException(nameof(manager));
			}

			if(tenant == null)
			{
				throw new ArgumentNullException(nameof(tenant));
			}

			IList<IdentityError> errors = new List<IdentityError>();
			await this.ValidateTenantName(manager, tenant, errors);
			return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
		}

		private async Task ValidateTenantName(TenantManager<TTenant> manager, TTenant tenant, ICollection<IdentityError> errors)
		{
			string tenantName = await manager.GetTenantNameAsync(tenant);
			if(string.IsNullOrWhiteSpace(tenantName))
			{
				errors.Add(this.Describer.InvalidTenantName(tenantName));
			}
			else
			{
				TTenant owner = await manager.FindByNameAsync(tenantName);
				if((owner != null) &&
				   !string.Equals(await manager.GetTenantIdAsync(owner), await manager.GetTenantIdAsync(tenant)))
				{
					errors.Add(this.Describer.DuplicateTenantName(tenantName));
				}
			}
		}
	}
}
