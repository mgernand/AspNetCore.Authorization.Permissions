namespace MongoSampleTenant.Pages
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	[Authorize]
	public class PermissionsModel : PageModel
	{
		private readonly ILogger<PermissionsModel> logger;

		public PermissionsModel(ILogger<PermissionsModel> logger)
		{
			this.logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
