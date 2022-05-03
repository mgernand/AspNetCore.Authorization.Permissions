namespace AspNetCore.Authorization.Permissions.Identity
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for an implementation that represents a permission in the identity system.
	/// </summary>
	[PublicAPI]
	public interface IPermission
	{
		/// <summary>
		///     Gets or sets the name of the permission.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		///     Gets or sets the normalized name of the permission.
		/// </summary>
		string NormalizedName { get; set; }

		/// <summary>
		///     A random value that should change whenever a permission is persisted to the store.
		/// </summary>
		string ConcurrencyStamp { get; set; }
	}
}
