namespace SampleTenant.Pages
{
	using AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;

	[HasPermission("Invoice.Statistics")]
	public class InvoiceStatisticsModel : PageModel
	{
		private readonly ILogger<InvoiceStatisticsModel> _logger;

		public InvoiceStatisticsModel(ILogger<InvoiceStatisticsModel> logger)
		{
			this._logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
