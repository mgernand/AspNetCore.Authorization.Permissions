namespace MadEyeMatt.AspNetCore.Authorization.Permissions
{
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Authorization;

	/// <summary>
	///     See: https://www.jerriepelser.com/blog/creating-dynamic-authorization-policies-aspnet-core/
	/// </summary>
	internal sealed class PermissionPolicyHandler : AuthorizationHandler<PermissionRequirement>
	{
		private readonly IPermissionLookupNormalizer permissionLookupNormalizer;

		public PermissionPolicyHandler(IPermissionLookupNormalizer permissionLookupNormalizer)
		{
			this.permissionLookupNormalizer = permissionLookupNormalizer;
		}

		/// <inheritdoc />
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
		{
			string normalizedName = this.permissionLookupNormalizer.NormalizeName(requirement.Permission);
			bool hasPermission = context.User.HasPermission(normalizedName);

			if(hasPermission)
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
