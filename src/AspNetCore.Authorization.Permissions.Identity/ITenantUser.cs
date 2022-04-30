namespace AspNetCore.Authorization.Permissions.Identity
{
	using JetBrains.Annotations;

	/// <summary>
	///     A marker interface to restrict the type of user that can be provided to be a tenant user.
	/// </summary>
	[PublicAPI]
	public interface ITenantUser
	{
	}
}
