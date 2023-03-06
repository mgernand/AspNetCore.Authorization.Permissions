namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for entities that belong to a tenant.
	/// </summary>
	[PublicAPI]
	public interface ITenantObject
	{
		/// <summary>
		///     Gets or sets the tenant ID.
		/// </summary>
		string TenantID { get; set; }
	}
}
