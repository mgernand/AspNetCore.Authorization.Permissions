namespace AspNetCore.Authorization.Permissions.Identity
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for an implementation that represents a tenant in the identity system.
	/// </summary>
	[PublicAPI]
	public interface ITenant
	{
		/// <summary>
		///     Gets or sets the name of the tenant.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		///     Gets or sets the normalized name of the tenant.
		/// </summary>
		string NormalizedName { get; set; }

		/// <summary>
		///     Gets or sets the display name of the tenant.
		/// </summary>
		string DisplayName { get; set; }

		/// <summary>
		///     A random value that should change whenever a tenant is persisted to the store.
		/// </summary>
		string ConcurrencyStamp { get; set; }

		/// <summary>
		///     Flag, indicating if the tenant is hierarchical.
		/// </summary>
		bool IsHierarchical { get; set; }

		/// <summary>
		///     Flag, indicating if the tenant's data is store in a separatem database.
		/// </summary>
		bool HasSeparateDatabase { get; set; }

		/// <summary>
		///     Gets or sets the name of the database where the tenant's data is stored.
		/// </summary>
		string DatabaseName { get; set; }
	}
}
