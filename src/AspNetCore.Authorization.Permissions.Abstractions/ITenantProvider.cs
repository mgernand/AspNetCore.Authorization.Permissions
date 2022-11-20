namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for services that provide the tenant ID.
	/// </summary>
	[PublicAPI]
	public interface ITenantProvider
	{
		/// <summary>
		///     Gets the tenant ID.
		/// </summary>
		string TenantId { get; }
	}
}
