namespace SampleTenant.Pages
{
	using AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;

	[HasPermission("Invoice.Payment")]
	public class InvoicePaymentModel : PageModel
	{
		private readonly ILogger<InvoicePaymentModel> _logger;

		public InvoicePaymentModel(ILogger<InvoicePaymentModel> logger)
		{
			this._logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
