namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     A default tenant implementation that used a string as type for the ID.
	/// </summary>
	[PublicAPI]
	public class IdentityTenant : IdentityTenant<string>
	{
	}

	/// <summary>
	///     The default tenant implementation.
	/// </summary>
	/// <typeparam name="TKey">The type of the ID.</typeparam>
	[PublicAPI]
	public class IdentityTenant<TKey> where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the primary key for this user.
		/// </summary>
		[PersonalData]
		public virtual TKey Id { get; set; }

		/// <summary>
		///     Gets or sets the name of the tenant.
		/// </summary>
		[ProtectedPersonalData]
		public virtual string Name { get; set; }

		/// <summary>
		///     Gets or sets the normalized name of the tenant.
		/// </summary>
		public virtual string NormalizedName { get; set; }

		/// <summary>
		///     Gets or sets the display name of the tenant.
		/// </summary>
		[ProtectedPersonalData]
		public virtual string DisplayName { get; set; }

		/// <summary>
		///     A random value that should change whenever a tenant is persisted to the store
		/// </summary>
		public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

		/// <summary>
		///     Flag, indicating if the tenant is hierarchical.
		/// </summary>
		public bool IsHierarchical { get; set; }

		/// <summary>
		///     Flag, indicating if the tenant's data is store in a separate database.
		/// </summary>
		[PersonalData]
		public bool HasSeparateDatabase { get; set; }

		/// <summary>
		///     Gets or sets the name of the database where the tenant's data is stored.
		/// </summary>
		[ProtectedPersonalData]
		public string DatabaseName { get; set; }

		/// <summary>
		///     Returns the name of the tenant.
		/// </summary>
		/// <returns>The name of the tenant.</returns>
		public override string ToString()
		{
			return this.Name ?? string.Empty;
		}
	}
}
