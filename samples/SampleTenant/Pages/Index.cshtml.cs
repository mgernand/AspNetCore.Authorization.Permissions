namespace SampleTenant.Pages
{
	using Microsoft.AspNetCore.Mvc.RazorPages;

	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			this._logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
