namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Provides an abstraction for managing roles in a persistence store.
	/// </summary>
	/// <remarks>
	///     Interface extracted from <see cref="RoleManager{TRole}" />.
	/// </remarks>
	/// <typeparam name="TRole">The type encapsulating a role.</typeparam>
	[PublicAPI]
	public interface IRoleManager<TRole> : IDisposable
		where TRole : class
	{
		/// <summary>
		///     Creates the specified <paramref name="role" /> in the persistence store.
		/// </summary>
		/// <param name="role">The role to create.</param>
		/// <returns>
		///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
		/// </returns>
		Task<IdentityResult> CreateAsync(TRole role);

		/// <summary>
		///     Updates the specified <paramref name="role" />.
		/// </summary>
		/// <param name="role">The role to updated.</param>
		/// <returns>
		///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the
		///     <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> for the update.
		/// </returns>
		Task<IdentityResult> UpdateAsync(TRole role);

		/// <summary>
		///     Deletes the specified <paramref name="role" />.
		/// </summary>
		/// <param name="role">The role to delete.</param>
		/// <returns>
		///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the
		///     <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> for the delete.
		/// </returns>
		Task<IdentityResult> DeleteAsync(TRole role);

		/// <summary>
		///     Updates the normalized name for the specified <paramref name="role" />.
		/// </summary>
		/// <param name="role">The role whose normalized name needs to be updated.</param>
		/// <returns>
		///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.
		/// </returns>
		Task UpdateNormalizedRoleNameAsync(TRole role);

		/// <summary>
		///     Gets a flag indicating whether the specified <paramref name="roleName" /> exists.
		/// </summary>
		/// <param name="roleName">The role name whose existence should be checked.</param>
		/// <returns>
		///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing true if the
		///     role name exists, otherwise false.
		/// </returns>
		Task<bool> RoleExistsAsync(string roleName);

		/// <summary>
		///     Finds the role associated with the specified <paramref name="roleId" /> if any.
		/// </summary>
		/// <param name="roleId">The role ID whose role should be returned.</param>
		/// <returns>
		///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the role
		///     associated with the specified <paramref name="roleId" />
		/// </returns>
		Task<TRole> FindByIdAsync(string roleId);

		/// <summary>
		///     Gets the name of the specified <paramref name="role" />.
		/// </summary>
		/// <param name="role">The role whose name should be retrieved.</param>
		/// <returns>
		///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the name of
		///     the
		///     specified <paramref name="role" />.
		/// </returns>
		Task<string> GetRoleNameAsync(TRole role);

		/// <summary>
		///     Sets the name of the specified <paramref name="role" />.
		/// </summary>
		/// <param name="role">The role whose name should be set.</param>
		/// <param name="name">The name to set.</param>
		/// <returns>
		///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the
		///     <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
		///     of the operation.
		/// </returns>
		Task<IdentityResult> SetRoleNameAsync(TRole role, string name);

		/// <summary>
		///     Gets the ID of the specified <paramref name="role" />.
		/// </summary>
		/// <param name="role">The role whose ID should be retrieved.</param>
		/// <returns>
		///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the ID of
		///     the
		///     specified <paramref name="role" />.
		/// </returns>
		Task<string> GetRoleIdAsync(TRole role);

		/// <summary>
		///     Finds the role associated with the specified <paramref name="roleName" /> if any.
		/// </summary>
		/// <param name="roleName">The name of the role to be returned.</param>
		/// <returns>
		///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the role
		///     associated with the specified <paramref name="roleName" />
		/// </returns>
		Task<TRole> FindByNameAsync(string roleName);

		/// <summary>Adds a claim to a role.</summary>
		/// <param name="role">The role to add the claim to.</param>
		/// <param name="claim">The claim to add.</param>
		/// <returns>
		///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the
		///     <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
		///     of the operation.
		/// </returns>
		Task<IdentityResult> AddClaimAsync(TRole role, Claim claim);

		/// <summary>Removes a claim from a role.</summary>
		/// <param name="role">The role to remove the claim from.</param>
		/// <param name="claim">The claim to remove.</param>
		/// <returns>
		///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the
		///     <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" />
		///     of the operation.
		/// </returns>
		Task<IdentityResult> RemoveClaimAsync(TRole role, Claim claim);

		/// <summary>
		///     Gets a list of claims associated with the specified <paramref name="role" />.
		/// </summary>
		/// <param name="role">The role whose claims should be returned.</param>
		/// <returns>
		///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the list of
		///     <see cref="T:System.Security.Claims.Claim" />s
		///     associated with the specified <paramref name="role" />.
		/// </returns>
		Task<IList<Claim>> GetClaimsAsync(TRole role);
	}
}
