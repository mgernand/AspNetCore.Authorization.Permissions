namespace SampleTenant.Pages
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc.RazorPages;

	[Authorize]
	public class TenantModel : PageModel
	{
		private readonly ILogger<TenantModel> _logger;

		public TenantModel(ILogger<TenantModel> logger)
		{
			this._logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
