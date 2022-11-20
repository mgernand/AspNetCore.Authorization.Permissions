namespace SampleTenant.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	[MadEyeMatt.AspNetCore.Authorization.Permissions.HasPermissionAttribute("Invoice.Statistics")]
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
