namespace AspNetCore.Authorization.Permissions.Identity
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Provides an abstraction for tenant validation.
	/// </summary>
	/// <typeparam name="TTenant">The type encapsulating a tenant.</typeparam>
	[PublicAPI]
	public interface ITenantValidator<TTenant> where TTenant : class
	{
		/// <summary>
		///     Validates the specified <paramref name="tenant" /> as an asynchronous operation.
		/// </summary>
		/// <param name="manager">The <see cref="TenantManager{TTenant}" /> that can be used to retrieve tenant properties.</param>
		/// <param name="tenant">The user to validate.</param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the
		///     <see cref="IdentityResult" /> of the validation operation.
		/// </returns>
		Task<IdentityResult> ValidateAsync(TenantManager<TTenant> manager, TTenant tenant);
	}
}
