namespace MadEyeMatt.AspNetCore.Identity.Permissions.Stores
{
    using System.Linq;
    using MadEyeMatt.AspNetCore.Identity.Permissions.Model;

    /// <summary>
    ///     Provides an abstraction for querying permissions in a permission store.
    /// </summary>
    /// <typeparam name="TPermission">The type encapsulating a permission.</typeparam>
    public interface IQueryablePermissionStore<TPermission> : IPermissionStore<TPermission>
        where TPermission : class, IPermission
    {
        /// <summary>
        ///     Returns an <see cref="IQueryable" /> collection of permissions.
        /// </summary>
        /// <value>An <see cref="IQueryable{T}" /> collection of permissions.</value>
        IQueryable<TPermission> Permissions { get; }
    }
}
