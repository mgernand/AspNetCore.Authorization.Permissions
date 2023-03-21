namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     Provides the APIs for managing users in a persistence store.
	/// </summary>
	/// <typeparam name="TUser">The type encapsulating a user.</typeparam>
	[PublicAPI]
	public class AspNetTenantUserManager<TUser> : TenantUserManager<TUser>, IDisposable
		where TUser : class
	{
		/// <inheritdoc />
		public AspNetTenantUserManager(
			IUserStore<TUser> userStore,
			IOptions<IdentityOptions> optionsAccessor,
			IPasswordHasher<TUser> passwordHasher,
			IEnumerable<IUserValidator<TUser>> userValidators,
			IEnumerable<IPasswordValidator<TUser>> passwordValidators,
			ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
			IServiceProvider services,
			ILogger<TenantUserManager<TUser>> logger,
			IHttpContextAccessor httpContextAccessor)
			: base(userStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
		{
			this.CancellationToken = httpContextAccessor?.HttpContext?.RequestAborted ?? CancellationToken.None;
		}

		/// <summary>
		///     The cancellation token associated with the current HttpContext.RequestAborted or CancellationToken.None if
		///     unavailable.
		/// </summary>
		protected override CancellationToken CancellationToken { get; }
	}
}
