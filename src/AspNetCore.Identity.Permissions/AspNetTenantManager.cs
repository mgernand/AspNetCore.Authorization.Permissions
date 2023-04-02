namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using System.Collections.Generic;
	using System.Threading;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     Provides the APIs for managing tenants in a persistence store.
	/// </summary>
	/// <typeparam name="TTenant">The type encapsulating a tenant.</typeparam>
	[PublicAPI]
	public class AspNetTenantManager<TTenant> : TenantManager<TTenant>, IDisposable
		where TTenant : class
	{
		/// <inheritdoc />
		public AspNetTenantManager(
			ITenantStore<TTenant> store,
			IEnumerable<ITenantValidator<TTenant>> tenantValidators,
			ILookupNormalizer keyNormalizer,
			PermissionIdentityErrorDescriber errors,
			ILogger<TenantManager<TTenant>> logger,
			IHttpContextAccessor httpContextAccessor)
			: base(store, tenantValidators, keyNormalizer, errors, logger)
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
