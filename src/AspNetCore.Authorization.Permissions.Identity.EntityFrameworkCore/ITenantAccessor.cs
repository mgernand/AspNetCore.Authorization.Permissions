namespace AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for services that access the tenant ID.
	/// </summary>
	[PublicAPI]
	public interface ITenantAccessor
	{
		/// <summary>
		///     Gets the tenant ID.
		/// </summary>
		string TenantId { get; }
	}
}
