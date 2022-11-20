namespace SamplePermissions.Pages
{
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	[MadEyeMatt.AspNetCore.Authorization.Permissions.HasPermissionAttribute("Invoice.Payment")]
	public class InvoicePaymentModel : PageModel
	{
		private readonly ILogger<InvoiceReadModel> logger;

		public InvoicePaymentModel(ILogger<InvoiceReadModel> logger, InvoicesContext context)
		{
			this.logger = logger;
			this.Context = context;
		}

		public InvoicesContext Context { get; }

		public void OnGet()
		{
		}
	}
}
