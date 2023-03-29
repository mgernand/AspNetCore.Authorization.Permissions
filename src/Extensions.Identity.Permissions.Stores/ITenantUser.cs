namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///		A contract for users that are assigned to a tenant.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public interface ITenantUser<TKey> where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the primary key of the tenant user is linked to.
		/// </summary>
		TKey TenantId { get; set; }
	}
}
