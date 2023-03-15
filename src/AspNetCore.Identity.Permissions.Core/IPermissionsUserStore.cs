namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     Provides an abstraction for a store which manages tenant user accounts.
	/// </summary>
	/// <typeparam name="TUser">The type encapsulating a tenant user.</typeparam>
	[PublicAPI]
	public interface IPermissionsUserStore<in TUser> : IDisposable
		where TUser : class
	{
		/// <summary>
		///     Gets the tenant ID for the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user whose tenant ID should be retrieved.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the name for the specified
		///     <paramref name="user" />.
		/// </returns>
		Task<string> GetTenantIdAsync(TUser user, CancellationToken cancellationToken);
	}
}
