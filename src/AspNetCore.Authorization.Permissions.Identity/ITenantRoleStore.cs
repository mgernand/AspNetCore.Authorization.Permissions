namespace AspNetCore.Authorization.Permissions.Identity
{
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     Provides an abstraction for a store which maps tenants to roles.
	/// </summary>
	/// <typeparam name="TTenant">The type encapsulating a tenant.</typeparam>
	[PublicAPI]
	public interface ITenantRoleStore<TTenant> : ITenantStore<TTenant>
		where TTenant : class, ITenant
	{
		/// <summary>
		///     Add the specified <paramref name="tenant" /> to the named role.
		/// </summary>
		/// <param name="tenant">The tenant to add to the named role.</param>
		/// <param name="roleName">The name of the role to add the user to.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
		Task AddToRoleAsync(TTenant tenant, string roleName, CancellationToken cancellationToken);

		/// <summary>
		///     Remove the specified <paramref name="tenant" /> from the named role.
		/// </summary>
		/// <param name="tenant">The tenant to remove the named role from.</param>
		/// <param name="roleName">The name of the role to remove.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
		Task RemoveFromRoleAsync(TTenant tenant, string roleName, CancellationToken cancellationToken);

		/// <summary>
		///     Gets a list of role names the specified <paramref name="tenant" /> belongs to.
		/// </summary>
		/// <param name="tenant">The tenant whose role names to retrieve.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation, containing a list of role names.</returns>
		Task<IList<string>> GetRolesAsync(TTenant tenant, CancellationToken cancellationToken);

		/// <summary>
		///     Returns a flag indicating whether the specified <paramref name="tenant" /> is a member of the given named role.
		/// </summary>
		/// <param name="tenant">The tenant whose role membership should be checked.</param>
		/// <param name="roleName">The name of the role to be checked.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing a flag indicating whether the
		///     specified <paramref name="user" /> is
		///     a member of the named role.
		/// </returns>
		Task<bool> IsInRoleAsync(TTenant tenant, string roleName, CancellationToken cancellationToken);

		/// <summary>
		///     Returns a list of Users who are members of the named role.
		/// </summary>
		/// <param name="roleName">The name of the role whose membership should be returned.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing a list of users who are in the named
		///     role.
		/// </returns>
		Task<IList<TTenant>> GetTenantsInRoleAsync(string roleName, CancellationToken cancellationToken);
	}
}
