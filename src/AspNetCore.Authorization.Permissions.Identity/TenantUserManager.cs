namespace AspNetCore.Authorization.Permissions.Identity
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;

	/// <inheritdoc />
	[PublicAPI]
	public class TenantUserManager<TUser> : UserManager<TUser>
		where TUser : class, ITenantUser
	{
		private readonly ITenantUserStore<TUser> tenantUserStore;

		/// <inheritdoc />
		public TenantUserManager(
			ITenantUserStore<TUser> tenantUserStore,
			IUserStore<TUser> userStore,
			IOptions<IdentityOptions> optionsAccessor,
			IPasswordHasher<TUser> passwordHasher,
			IEnumerable<IUserValidator<TUser>> userValidators,
			IEnumerable<IPasswordValidator<TUser>> passwordValidators,
			ILookupNormalizer keyNormalizer,
			IdentityErrorDescriber errors,
			IServiceProvider services,
			ILogger<TenantUserManager<TUser>> logger)
			: base(userStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
		{
			this.tenantUserStore = tenantUserStore;
		}

		/// <summary>
		///     Gets the tenant name for the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user whose tenant name should be retrieved.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the name for the specified
		///     <paramref name="user" />.
		/// </returns>
		public virtual async Task<string> GetTenantIdAsync(TUser user)
		{
			this.ThrowIfDisposed();
			if(user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			return await this.tenantUserStore.GetTenantIdAsync(user, this.CancellationToken);
		}
	}
}
