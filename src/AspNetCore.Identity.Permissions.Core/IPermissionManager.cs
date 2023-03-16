namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Provides an abstraction for managing permissions in a persistence store.
	/// </summary>
	/// <typeparam name="TPermission">The type encapsulating a permission.</typeparam>
	[PublicAPI]
	public interface IPermissionManager<TPermission> : IDisposable
		where TPermission : class
	{
		/// <summary>
		///     Creates the specified <paramref name="permission" /> in the persistence store.
		/// </summary>
		/// <param name="permission">The permission to create.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation.
		/// </returns>
		Task<IdentityResult> CreateAsync(TPermission permission);

		/// <summary>
		///     Updates the specified <paramref name="permission" />.
		/// </summary>
		/// <param name="permission">The permission to updated.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
		///     for the update.
		/// </returns>
		Task<IdentityResult> UpdateAsync(TPermission permission);

		/// <summary>
		///     Deletes the specified <paramref name="permission" />.
		/// </summary>
		/// <param name="permission">The permission to delete.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
		///     for the delete.
		/// </returns>
		Task<IdentityResult> DeleteAsync(TPermission permission);

		/// <summary>
		///     Gets a flag indicating whether the specified <paramref name="permissionName" /> exists.
		/// </summary>
		/// <param name="permissionName">The permission name whose existence should be checked.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing true if the permission name exists,
		///     otherwise false.
		/// </returns>
		Task<bool> PermissionExistsAsync(string permissionName);

		/// <summary>
		///     Updates the normalized name for the specified <paramref name="permission" />.
		/// </summary>
		/// <param name="permission">The permission whose normalized name needs to be updated.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation.
		/// </returns>
		Task UpdateNormalizedPermissionNameAsync(TPermission permission);

		/// <summary>
		///     Gets the name of the specified <paramref name="permission" />.
		/// </summary>
		/// <param name="permission">The permission whose name should be retrieved.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the name of the
		///     specified <paramref name="permission" />.
		/// </returns>
		Task<string> GetPermissionNameAsync(TPermission permission);

		/// <summary>
		///     Sets the name of the specified <paramref name="permission" />.
		/// </summary>
		/// <param name="permission">The permission whose name should be set.</param>
		/// <param name="permissionName">The name to set.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
		///     of the operation.
		/// </returns>
		Task<IdentityResult> SetPermissionNameAsync(TPermission permission, string permissionName);

		/// <summary>
		///     Gets the ID of the specified <paramref name="permission" />.
		/// </summary>
		/// <param name="permission">The permission whose ID should be retrieved.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the ID of the
		///     specified <paramref name="permission" />.
		/// </returns>
		Task<string> GetPermissionIdAsync(TPermission permission);

		/// <summary>
		///     Finds the role associated with the specified <paramref name="permissionName" /> if any.
		/// </summary>
		/// <param name="permissionName">The permission ID whose permission should be returned.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the permission
		///     associated with the specified <paramref name="permissionName" />
		/// </returns>
		Task<TPermission> FindByNameAsync(string permissionName);

		/// <summary>
		///     Returns a list of permissions from the permission store who are members of the specified
		///     <paramref name="roleName" />.
		/// </summary>
		/// <param name="roleName">The name of the role whose permission should be returned.</param>
		/// <returns>
		///     A <see cref="Task{TResult}" /> that represents the result of the asynchronous query, a list of
		///     <typeparamref name="TPermission" />s who
		///     are members of the specified role.
		/// </returns>
		Task<IList<TPermission>> GetPermissionsInRoleAsync(string roleName);

		/// <summary>
		///     Returns the users permission claim values if present otherwise returns null.
		/// </summary>
		/// <param name="principal">The <see cref="T:System.Security.Claims.ClaimsPrincipal" /> instance.</param>
		/// <returns>The users permission claim values, or empty list if the claim is not present.</returns>
		/// <remarks>
		///     The users permission claims are identified by
		///     <see cref="F:MadEyeMatt.AspNetCore.Authorization.Permissions.PermissionClaimTypes.PermissionClaimType" />.
		/// </remarks>
		IList<string> GetPermissions(ClaimsPrincipal principal);
	}
}
