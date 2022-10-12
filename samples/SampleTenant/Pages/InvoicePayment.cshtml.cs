namespace SampleTenant.Pages
{
	using AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	[HasPermission("Invoice.Payment")]
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
