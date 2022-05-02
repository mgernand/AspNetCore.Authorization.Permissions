namespace SampleTenant.Pages
{
	using AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;

	[HasPermission("Invoice.Send")]
	public class InvoiceSendModel : PageModel
	{
		private readonly ILogger<InvoiceSendModel> _logger;

		public InvoiceSendModel(ILogger<InvoiceSendModel> logger)
		{
			this._logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
