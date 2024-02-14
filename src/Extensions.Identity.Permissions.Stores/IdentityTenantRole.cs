namespace MadEyeMatt.Extensions.Identity.Permissions.Stores
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     Represents the link between a tenant and a role.
	/// </summary>
	/// <typeparam name="TKey">The type of the primary key used for tenant and roles.</typeparam>
	[PublicAPI]
	public class IdentityTenantRole<TKey> where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the primary key of the tenant that is linked to a role.
		/// </summary>
		public virtual TKey TenantId { get; set; }

		/// <summary>
		///     Gets or sets the primary key of the role that is linked to the tenant.
		/// </summary>
		public virtual TKey RoleId { get; set; }
	}
}
