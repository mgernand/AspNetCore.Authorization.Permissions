namespace SampleTenant.Pages
{
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	[HasPermission("Invoice.Send")]
	public class InvoiceSendModel : PageModel
	{
		private readonly ILogger<InvoiceSendModel> logger;

		public InvoiceSendModel(ILogger<InvoiceSendModel> logger)
		{
			this.logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
