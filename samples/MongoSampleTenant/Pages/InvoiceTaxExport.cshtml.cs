namespace MongoSampleTenant.Pages
{
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	[HasPermission("Invoice.TaxExport")]
	public class InvoiceTaxExportModel : PageModel
	{
		private readonly ILogger<InvoiceTaxExportModel> logger;

		public InvoiceTaxExportModel(ILogger<InvoiceTaxExportModel> logger)
		{
			this.logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
