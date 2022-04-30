namespace AspNetCore.Authorization.Permissions.Identity
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Provides an abstraction for a validating a permission.
	/// </summary>
	/// <typeparam name="TPermission">The type encapsulating a permission.</typeparam>
	public interface IPermissionValidator<TPermission> where TPermission : class
	{
		/// <summary>
		///     Validates a permission as an asynchronous operation.
		/// </summary>
		/// <param name="manager">The <see cref="PermissionManager{TPermission}" /> managing the permission store.</param>
		/// <param name="permission">The permission to validate.</param>
		/// <returns>
		///     A <see cref="Task{TResult}" /> that represents the <see cref="IdentityResult" /> of the asynchronous
		///     validation.
		/// </returns>
		Task<IdentityResult> ValidateAsync(PermissionManager<TPermission> manager, TPermission permission);
	}
}
