namespace MadEyeMatt.Extensions.Identity.Permissions
{
	using System.Linq;
	using JetBrains.Annotations;

	/// <summary>
	///     Provides an abstraction for querying permissions in a permission store.
	/// </summary>
	/// <typeparam name="TPermission">The type encapsulating a permission.</typeparam>
	[PublicAPI]
	public interface IQueryablePermissionStore<TPermission> : IPermissionStore<TPermission>
		where TPermission : class
	{
		/// <summary>
		///     Returns an <see cref="IQueryable" /> collection of permissions.
		/// </summary>
		/// <value>An <see cref="IQueryable{T}" /> collection of permissions.</value>
		IQueryable<TPermission> Permissions { get; }
	}
}
