namespace SampleTenant.Pages
{
	using Microsoft.AspNetCore.Mvc.RazorPages;

	public class PrivacyModel : PageModel
	{
		private readonly ILogger<PrivacyModel> _logger;

		public PrivacyModel(ILogger<PrivacyModel> logger)
		{
			this._logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
