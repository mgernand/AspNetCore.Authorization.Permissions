namespace SamplePermissions.Pages
{
	using AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	[HasPermission("Invoice.Payment")]
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
