namespace SampleTenant.Pages
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	[Authorize]
	public class InvoicesModel : PageModel
	{
		private readonly ILogger<InvoicesModel> logger;

		public InvoicesModel(ILogger<InvoicesModel> logger)
		{
			this.logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
