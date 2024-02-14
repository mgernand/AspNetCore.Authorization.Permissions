namespace MadEyeMatt.Extensions.Identity.Permissions
{
	using System.Collections.Generic;
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     Provides an abstraction for a store which maps permissions to roles.
	/// </summary>
	/// <typeparam name="TPermission">The type encapsulating a permission.</typeparam>
	[PublicAPI]
	public interface IRolePermissionStore<TPermission> : IPermissionStore<TPermission>
		where TPermission : class
	{
		/// <summary>
		///     Add the specified <paramref name="permission" /> to the named role.
		/// </summary>
		/// <param name="permission">The permission to add to the named role.</param>
		/// <param name="normalizedRoleName">The name of the role to add the permission to.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
		Task AddToRoleAsync(TPermission permission, string normalizedRoleName, CancellationToken cancellationToken = default);

        /// <summary>
        ///     Remove the specified <paramref name="permission" /> from the named role.
        /// </summary>
        /// <param name="permission">The permission to remove the named role from.</param>
        /// <param name="normalizedRoleName">The name of the role to remove.</param>
        /// <param name="cancellationToken">
        ///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
        ///     should be canceled.
        /// </param>
        /// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
        Task RemoveFromRoleAsync(TPermission permission, string normalizedRoleName, CancellationToken cancellationToken);

		/// <summary>
		///     Gets a list of permission names the specified <paramref name="permission" /> belongs to.
		/// </summary>
		/// <param name="permission">The permission whose role names to retrieve.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation, containing a list of permission names.</returns>
		Task<IList<string>> GetRolesAsync(TPermission permission, CancellationToken cancellationToken);

		/// <summary>
		///     Gets a list of permission names the specified <paramref name="permission" /> belongs to.
		/// </summary>
		/// <param name="permission">The permission whose role names to retrieve.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>The <see cref="Task" /> that represents the asynchronous operation, containing a list of permission names.</returns>
		Task<IList<string>> GetRoleIdsAsync(TPermission permission, CancellationToken cancellationToken);

        /// <summary>
        ///     Returns a flag indicating whether the specified <paramref name="permission" /> is a member of the given named role.
        /// </summary>
        /// <param name="permission">The permission whose role membership should be checked.</param>
        /// <param name="normalizedRoleName">The name of the role to be checked.</param>
        /// <param name="cancellationToken">
        ///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
        ///     should be canceled.
        /// </param>
        /// <returns>
        ///     The <see cref="Task" /> that represents the asynchronous operation, containing a flag indicating whether the
        ///     specified <paramref name="permission" /> is
        ///     a member of the named role.
        /// </returns>
        Task<bool> IsInRoleAsync(TPermission permission, string normalizedRoleName, CancellationToken cancellationToken);

		/// <summary>
		///     Returns a list of permissions who are members of the named role.
		/// </summary>
		/// <param name="normalizedRoleName">The name of the role whose permissions should be returned.</param>
		/// <param name="cancellationToken">
		///     The <see cref="CancellationToken" /> used to propagate notifications that the operation
		///     should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing a list of users who are in the named
		///     role.
		/// </returns>
		Task<IList<TPermission>> GetPermissionsInRoleAsync(string normalizedRoleName, CancellationToken cancellationToken = default);
	}
}
