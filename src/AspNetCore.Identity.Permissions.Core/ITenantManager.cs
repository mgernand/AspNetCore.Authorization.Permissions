namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Provides an abstraction for managing tenants in a persistence store.
	/// </summary>
	/// <typeparam name="TTenant">The type encapsulating a tenants.</typeparam>
	[PublicAPI]
	public interface ITenantManager<TTenant> : IDisposable
		where TTenant : class
	{
		/// <summary>
		///     Creates the specified <paramref name="tenant" /> in the persistence store.
		/// </summary>
		/// <param name="tenant">The tenant to create.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation.
		/// </returns>
		Task<IdentityResult> CreateAsync(TTenant tenant);

		/// <summary>
		///     Updates the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant to updated.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
		///     for the update.
		/// </returns>
		Task<IdentityResult> UpdateAsync(TTenant tenant);

		/// <summary>
		///     Deletes the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant to delete.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
		///     for the delete.
		/// </returns>
		Task<IdentityResult> DeleteAsync(TTenant tenant);

		/// <summary>
		///     Gets a flag indicating whether the specified <paramref name="tenantName" /> exists.
		/// </summary>
		/// <param name="tenantName">The tenant name whose existence should be checked.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing true if the permission name exists,
		///     otherwise false.
		/// </returns>
		Task<bool> TenantExistsAsync(string tenantName);

		/// <summary>
		///     Finds the role associated with the specified <paramref name="tenantId" /> if any.
		/// </summary>
		/// <param name="tenantId">The tenant ID whose tenant should be returned.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the permission
		///     associated with the specified <paramref name="tenantId" />
		/// </returns>
		Task<TTenant> FindByIdAsync(string tenantId);

		/// <summary>
		///     Finds the role associated with the specified <paramref name="tenantName" /> if any.
		/// </summary>
		/// <param name="tenantName">The tenant ID whose tenant should be returned.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the permission
		///     associated with the specified <paramref name="tenantName" />
		/// </returns>
		Task<TTenant> FindByNameAsync(string tenantName);

		/// <summary>
		///     Updates the normalized name for the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The permission whose normalized name needs to be updated.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation.
		/// </returns>
		Task UpdateNormalizedTenantNameAsync(TTenant tenant);

		/// <summary>
		///     Gets the ID of the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant whose ID should be retrieved.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the ID of the
		///     specified <paramref name="tenant" />.
		/// </returns>
		Task<string> GetTenantIdAsync(TTenant tenant);

		/// <summary>
		///     Gets the name of the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant whose name should be retrieved.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the ID of the
		///     specified <paramref name="tenant" />.
		/// </returns>
		Task<string> GetTenantNameAsync(TTenant tenant);

		/// <summary>
		///     Gets the display name of the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant whose display name should be retrieved.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the ID of the
		///     specified <paramref name="tenant" />.
		/// </returns>
		Task<string> GetTenantDisplayNameAsync(TTenant tenant);

		/// <summary>
		///     Sets the name of the specified <paramref name="tenant" />.
		/// </summary>
		/// <param name="tenant">The tenant whose name should be set.</param>
		/// <param name="tenantName">The name to set.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
		///     of the operation.
		/// </returns>
		Task<IdentityResult> SetTenantNameAsync(TTenant tenant, string tenantName);

		/// <summary>
		///     Gets a list of role names the specified <paramref name="tenant" /> belongs to.
		/// </summary>
		/// <param name="tenant">The tenant whose role names to retrieve.</param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation, containing a list of role names.</returns>
		Task<IList<string>> GetRolesAsync(TTenant tenant);
	}
}
