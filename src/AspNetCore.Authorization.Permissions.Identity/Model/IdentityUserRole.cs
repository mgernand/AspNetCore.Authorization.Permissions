namespace MadEyeMatt.AspNetCore.Identity.Permissions.Model
{
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Represents the link between a user and a role that uses a string as type for the keys.
	/// </summary>
	public class IdentityUserRole : IdentityUserRole<string>
	{
	}
}
