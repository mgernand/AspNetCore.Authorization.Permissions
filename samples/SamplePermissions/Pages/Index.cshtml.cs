namespace SamplePermissions.Pages
{
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			this.logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
