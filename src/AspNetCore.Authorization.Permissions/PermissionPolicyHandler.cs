namespace MadEyeMatt.AspNetCore.Authorization.Permissions
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using ClaimsPrincipalExtensions = MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.ClaimsPrincipalExtensions;

    /// <summary>
	///     See: https://www.jerriepelser.com/blog/creating-dynamic-authorization-policies-aspnet-core/
	/// </summary>
	internal sealed class PermissionPolicyHandler : AuthorizationHandler<PermissionRequirement>
	{
		private readonly MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IPermissionLookupNormalizer permissionLookupNormalizer;

		public PermissionPolicyHandler(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IPermissionLookupNormalizer permissionLookupNormalizer)
		{
			this.permissionLookupNormalizer = permissionLookupNormalizer;
		}

		/// <inheritdoc />
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
		{
			string normalizedName = this.permissionLookupNormalizer.NormalizeName(requirement.Permission);
			bool hasPermission = ClaimsPrincipalExtensions.HasPermission(context.User, normalizedName);

			if(hasPermission)
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
