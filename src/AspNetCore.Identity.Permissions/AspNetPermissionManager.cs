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
	///     Provides the APIs for managing permissions in a persistence store.
	/// </summary>
	/// <typeparam name="TPermission">The type encapsulating a permission.</typeparam>
	[PublicAPI]
	public class AspNetPermissionManager<TPermission> : PermissionManager<TPermission>, IDisposable
		where TPermission : class
	{
		/// <inheritdoc />
		public AspNetPermissionManager(
			IPermissionStore<TPermission> store,
			IEnumerable<IPermissionValidator<TPermission>> permissionValidators,
			ILookupNormalizer keyNormalizer,
			IdentityErrorDescriber errors,
			ILogger<PermissionManager<TPermission>> logger,
			IHttpContextAccessor httpContextAccessor)
			: base(store, permissionValidators, keyNormalizer, errors, logger)
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
