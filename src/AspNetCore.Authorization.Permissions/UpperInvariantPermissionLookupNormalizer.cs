namespace MadEyeMatt.AspNetCore.Authorization.Permissions
{
	/// <summary>
	///     Implements <see cref="Abstractions.IPermissionLookupNormalizer" /> by converting names to their upper cased
	///     invariant culture
	///     representation.
	/// </summary>
	internal sealed class UpperInvariantPermissionLookupNormalizer : MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IPermissionLookupNormalizer
	{
		/// <inheritdoc />
		public string NormalizeName(string permissionName)
		{
			return permissionName?.Normalize().ToUpperInvariant();
		}
	}
}
