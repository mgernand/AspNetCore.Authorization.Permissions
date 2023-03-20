// ReSharper disable once CheckNamespace

using Microsoft.AspNetCore.Identity;

namespace MadEyeMatt.Extensions.Identity.Permissions
{
    /// <summary>
    ///     Represents the link between a user and a role that uses a string as type for the keys.
    /// </summary>
    public class IdentityUserRole : IdentityUserRole<string>
    {
    }
}
