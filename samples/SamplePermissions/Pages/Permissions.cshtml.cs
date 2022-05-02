namespace SamplePermissions.Pages
{
	using AspNetCore.Authorization.Permissions.Abstractions;
	using Fluxera.Utilities.Extensions;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc.RazorPages;

	[Authorize]
	public class PermissionsModel : PageModel
	{
		private readonly ILogger<PermissionsModel> _logger;

		public PermissionsModel(ILogger<PermissionsModel> logger)
		{
			this._logger = logger;
		}

		public void OnGet()
		{
			if(this.User.IsAuthenticated())
			{
				this.User.GetPermissions();
			}
		}
	}
}
