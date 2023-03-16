namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System.Collections.Generic;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Logging;

	/// <summary>
	///     Provides the APIs for managing roles in a persistence store.
	/// </summary>
	/// <typeparam name="TRole">The type encapsulating a role.</typeparam>
	[PublicAPI]
	public class CustomRoleManager<TRole> : RoleManager<TRole>, IRoleManager<TRole>
		where TRole : class
	{
		/// <inheritdoc />
		public CustomRoleManager(
			IRoleStore<TRole> store,
			IEnumerable<IRoleValidator<TRole>> roleValidators,
			ILookupNormalizer keyNormalizer,
			IdentityErrorDescriber errors,
			ILogger<RoleManager<TRole>> logger)
			: base(store, roleValidators, keyNormalizer, errors, logger)
		{
		}
	}
}
