namespace SamplePermissions.Pages
{
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
		}
	}
}
