namespace MadEyeMatt.Extensions.Identity.Permissions
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Provides an abstraction for a store which manages tenants.
	/// </summary>
	/// <typeparam name="TTenant">The type encapsulating a tenant.</typeparam>
	[PublicAPI]
	public interface ITenantStore<TTenant> : IDisposable
		where TTenant : class
	{
		/// <summary>
		///     Creates the specified <paramref name="tenant" /> in the permission store.
		/// </summary>
		/// <param name="tenant">The tenant to create.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the
		///     <see cref="IdentityResult" /> of the creation operation.
		/// </returns>
		Task<IdentityResult> CreateAsync(TTenant tenant, CancellationToken cancellationToken);

		/// <summary>
		///     Updates the specified <paramref name="tenant" /> in the permission store.
		/// </summary>
		/// <param name="tenant">The tenant to update.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the
		///     <see cref="IdentityResult" /> of the update operation.
		/// </returns>
		Task<IdentityResult> UpdateAsync(TTenant tenant, CancellationToken cancellationToken);

		/// <summary>
		///     Deletes the specified <paramref name="tenant" /> from the permission store.
		/// </summary>
		/// <param name="tenant">The tenant to delete.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the
		///     <see cref="IdentityResult" /> of the delete operation.
		/// </returns>
		Task<IdentityResult> DeleteAsync(TTenant tenant, CancellationToken cancellationToken);

		/// <summary>
		///     Gets the tenant identifier for the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant whose identifier should be retrieved.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the identifier for the
		///     specified <paramref name="tenant" />.
		/// </returns>
		Task<string> GetTenantIdAsync(TTenant tenant, CancellationToken cancellationToken);

		/// <summary>
		///     Gets the tenant name for the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant whose name should be retrieved.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the name for the specified
		///     <paramref name="tenant" />.
		/// </returns>
		Task<string> GetTenantNameAsync(TTenant tenant, CancellationToken cancellationToken);

		/// <summary>
		///     Gets the normalized tenant name for the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The user whose normalized name should be retrieved.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the normalized tenant name for
		///     the specified <paramref name="tenant" />.
		/// </returns>
		Task<string> GetNormalizedTenantNameAsync(TTenant tenant, CancellationToken cancellationToken);

		/// <summary>
		///     Gets the tenant display name for the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant whose display name should be retrieved.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the name for the specified
		///     <paramref name="tenant" />.
		/// </returns>
		Task<string> GetTenantDisplayNameAsync(TTenant tenant, CancellationToken cancellationToken);

		/// <summary>
		///     Sets the given <paramref name="tenantName" /> for the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant whose name should be set.</param>
		/// <param name="tenantName">The tenant name to set.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
		Task SetTenantNameAsync(TTenant tenant, string tenantName, CancellationToken cancellationToken);

		/// <summary>
		///     Sets the given normalized name for the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant whose name should be set.</param>
		/// <param name="normalizedTenantName">The normalized name to set.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
		Task SetNormalizedTenantNameAsync(TTenant tenant, string normalizedTenantName, CancellationToken cancellationToken);

		/// <summary>
		///     Sets the given <paramref name="tenantDisplayName" /> for the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant whose display name should be set.</param>
		/// <param name="tenantDisplayName">The tenant display name to set.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
		Task SetTenantDisplayNameAsync(TTenant tenant, string tenantDisplayName, CancellationToken cancellationToken);

		/// <summary>
		///     Finds and returns a tenant, if any, who has the specified <paramref name="tenantId" />.
		/// </summary>
		/// <param name="tenantId">The tenant ID to search for.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the user matching the specified
		///     <paramref name="tenantId" /> if it exists.
		/// </returns>
		Task<TTenant> FindByIdAsync(string tenantId, CancellationToken cancellationToken);

		/// <summary>
		///     Finds and returns a tenant, if any, who has the specified normalized tenant name.
		/// </summary>
		/// <param name="normalizedTenantName">The normalized tenant name to search for.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the tenant matching the specified
		///     <paramref name="normalizedTenantName" /> if it exists.
		/// </returns>
		Task<TTenant> FindByNameAsync(string normalizedTenantName, CancellationToken cancellationToken);
	}
}
