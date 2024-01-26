namespace MongoSampleTenant.Pages
{
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	[RequirePermission("Invoice.Statistics")]
	public class InvoiceStatisticsModel : PageModel
	{
		private readonly ILogger<InvoiceStatisticsModel> logger;

		public InvoiceStatisticsModel(ILogger<InvoiceStatisticsModel> logger)
		{
			this.logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
