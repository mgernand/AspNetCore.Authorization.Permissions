namespace SampleTenant.Pages
{
	using AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;

	[HasPermission("Invoice.TaxExport")]
	public class InvoiceTaxExportModel : PageModel
	{
		private readonly ILogger<InvoiceTaxExportModel> _logger;

		public InvoiceTaxExportModel(ILogger<InvoiceTaxExportModel> logger)
		{
			this._logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
