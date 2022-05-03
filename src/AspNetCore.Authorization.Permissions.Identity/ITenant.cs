namespace AspNetCore.Authorization.Permissions.Identity
{
	using JetBrains.Annotations;

	/// <summary>
	///     A marker interface to restrict the type of tenant that can be provided to be a tenant.
	/// </summary>
	[PublicAPI]
	public interface ITenant
	{
	}
}
