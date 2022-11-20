namespace SamplePermissions.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	[MadEyeMatt.AspNetCore.Authorization.Permissions.HasPermissionAttribute("Invoice.Payment")]
	public class InvoicePaymentModel : PageModel
	{
		private readonly ILogger<InvoiceReadModel> logger;

		public InvoicePaymentModel(ILogger<InvoiceReadModel> logger, ApplicationDbContext context)
		{
			this.logger = logger;
			this.Context = context;
		}

		public ApplicationDbContext Context { get; }

		public void OnGet()
		{
		}
	}
}
