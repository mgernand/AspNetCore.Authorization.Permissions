namespace MongoSampleTenant.Pages
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	[Authorize]
	public class TenantModel : PageModel
	{
		private readonly ILogger<TenantModel> logger;

		public TenantModel(ILogger<TenantModel> logger)
		{
			this.logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
