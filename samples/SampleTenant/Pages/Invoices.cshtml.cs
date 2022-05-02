namespace SampleTenant.Pages
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc.RazorPages;

	[Authorize]
	public class InvoicesModel : PageModel
	{
		private readonly ILogger<InvoicesModel> _logger;

		public InvoicesModel(ILogger<InvoicesModel> logger)
		{
			this._logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
