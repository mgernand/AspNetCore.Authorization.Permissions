namespace MadEyeMatt.Extensions.Identity.Permissions
{
	using System.Linq;
	using JetBrains.Annotations;

	/// <summary>
	///     Provides an abstraction for querying tenants in a tenant store.
	/// </summary>
	/// <typeparam name="TTenant">The type encapsulating a tenant.</typeparam>
	[PublicAPI]
	public interface IQueryableTenantStore<TTenant> : ITenantStore<TTenant>
		where TTenant : class
	{
		/// <summary>
		///     Returns an <see cref="IQueryable" /> collection of tenants.
		/// </summary>
		/// <value>An <see cref="IQueryable{T}" /> collection of tenants.</value>
		IQueryable<TTenant> Tenants { get; }
	}
}
