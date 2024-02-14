namespace MadEyeMatt.Extensions.Identity.Permissions
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Provides an abstraction for a store which manages user permissions.
	/// </summary>
	/// <typeparam name="TPermission">The type encapsulating a permission.</typeparam>
	[PublicAPI]
	public interface IPermissionStore<TPermission> : IDisposable
		where TPermission : class
	{
		/// <summary>
		///     Creates the specified <paramref name="permission" /> in the permission store.
		/// </summary>
		/// <param name="permission">The permission to create.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the
		///     <see cref="IdentityResult" /> of the creation operation.
		/// </returns>
		Task<IdentityResult> CreateAsync(TPermission permission, CancellationToken cancellationToken = default);

		/// <summary>
		///     Updates the specified <paramref name="permission" /> in the permission store.
		/// </summary>
		/// <param name="permission">The permission to update.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the
		///     <see cref="IdentityResult" /> of the update operation.
		/// </returns>
		Task<IdentityResult> UpdateAsync(TPermission permission, CancellationToken cancellationToken = default);

		/// <summary>
		///     Deletes the specified <paramref name="permission" /> from the permission store.
		/// </summary>
		/// <param name="permission">The permission to delete.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the
		///     <see cref="IdentityResult" /> of the delete operation.
		/// </returns>
		Task<IdentityResult> DeleteAsync(TPermission permission, CancellationToken cancellationToken = default);

		/// <summary>
		///     Gets the permission identifier for the specified <paramref name="permission" />.
		/// </summary>
		/// <param name="permission">The permission whose identifier should be retrieved.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		Task<string> GetPermissionIdAsync(TPermission permission, CancellationToken cancellationToken = default);

		/// <summary>
		///     Gets the permission name for the specified <paramref name="permission" />.
		/// </summary>
		/// <param name="permission">The permission whose name should be retrieved.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the name for the specified
		///     <paramref name="permission" />.
		/// </returns>
		Task<string> GetPermissionNameAsync(TPermission permission, CancellationToken cancellationToken = default);

		/// <summary>
		///     Sets the given <paramref name="permissionName" /> for the specified <paramref name="permission" />.
		/// </summary>
		/// <param name="permission">The permission whose name should be set.</param>
		/// <param name="permissionName">The permission name to set.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
		Task SetPermissionNameAsync(TPermission permission, string permissionName, CancellationToken cancellationToken = default);

		/// <summary>
		///     Set a permission's normalized name as an asynchronous operation.
		/// </summary>
		/// <param name="permission">The permission whose normalized name should be set.</param>
		/// <param name="normalizedPermissionName">The normalized name to set</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
		Task SetNormalizedPermissionNameAsync(TPermission permission, string normalizedPermissionName, CancellationToken cancellationToken = default);

		/// <summary>
		///     Gets the normalized permission name for the specified <paramref name="permission" />.
		/// </summary>
		/// <param name="permission">The user whose normalized name should be retrieved.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the normalized user name for
		///     the specified <paramref name="permission" />.
		/// </returns>
		Task<string> GetNormalizedPermissionNameAsync(TPermission permission, CancellationToken cancellationToken = default);

		/// <summary>
		///     Finds and returns a permission, if any, who has the specified <paramref name="permissionId" />.
		/// </summary>
		/// <param name="permissionId">The permission ID to search for.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the user matching the specified
		///     <paramref name="permissionId" /> if it exists.
		/// </returns>
		Task<TPermission> FindByIdAsync(string permissionId, CancellationToken cancellationToken = default);

		/// <summary>
		///     Finds and returns a permission, if any, who has the specified normalized permission name.
		/// </summary>
		/// <param name="normalizedPermissionName">The normalized permission name to search for.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the user matching the specified
		///     <paramref name="normalizedPermissionName" /> if it exists.
		/// </returns>
		Task<TPermission> FindByNameAsync(string normalizedPermissionName, CancellationToken cancellationToken = default);
	}
}
