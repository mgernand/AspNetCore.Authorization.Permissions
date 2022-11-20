namespace SampleTenant.Pages
{
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	[MadEyeMatt.AspNetCore.Authorization.Permissions.HasPermissionAttribute("Invoice.Payment")]
	public class InvoicePaymentModel : PageModel
	{
		private readonly ILogger<InvoicePaymentModel> logger;

		public InvoicePaymentModel(ILogger<InvoicePaymentModel> logger)
		{
			this.logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
