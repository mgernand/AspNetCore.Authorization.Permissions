namespace MadEyeMatt.AspNetCore.Authorization.Permissions
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Options;

    /// <summary>
	///     See: https://www.jerriepelser.com/blog/creating-dynamic-authorization-policies-aspnet-core/
	///     See: https://github.com/JonPSmith/PermissionAccessControl/issues/3
	/// </summary>
	internal sealed class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
	{
		/// <summary>
		///     Creates a new instance of the <see cref="AuthorizationPolicyProvider" /> type.
		/// </summary>
		/// <param name="options"></param>
		public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
		{
		}

		/// <summary>
		///     This gets the PermissionRequirement for the given policyName
		/// </summary>
		/// <param name="policyName"></param>
		/// <returns></returns>
		public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
		{
			return await base.GetPolicyAsync(policyName)
				?? new AuthorizationPolicyBuilder()
					.AddRequirements(new PermissionRequirement(policyName))
					.Build();
		}
	}
}
