namespace AspNetCore.Authorization.Permissions.Abstractions
{
	using JetBrains.Annotations;

	/// <summary>
	///     Provides a contract for normalizing permission names for lookup purposes.
	/// </summary>
	[PublicAPI]
	public interface IPermissionLookupNormalizer
	{
		/// <summary>
		///     Returns a normalized representation of the specified <paramref name="permissionName" />.
		/// </summary>
		/// <param name="permissionName">The name to normalize.</param>
		/// <returns>A normalized representation of the specified <paramref name="permissionName" />.</returns>
		string NormalizeName(string permissionName);
	}
}
