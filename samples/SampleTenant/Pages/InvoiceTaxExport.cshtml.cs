﻿namespace SampleTenant.Pages
{
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	[MadEyeMatt.AspNetCore.Authorization.Permissions.HasPermissionAttribute("Invoice.TaxExport")]
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
