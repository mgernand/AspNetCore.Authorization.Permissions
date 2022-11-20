namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.Model
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for an implementation that represents a role in the identity system.
	/// </summary>
	[PublicAPI]
	public interface IRole
	{
		/// <summary>
		///     Gets or sets the name for this role.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		///     Gets or sets the normalized name for this role.
		/// </summary>
		string NormalizedName { get; set; }

		/// <summary>
		///     A random value that should change whenever a role is persisted to the store.
		/// </summary>
		string ConcurrencyStamp { get; set; }
	}
}
