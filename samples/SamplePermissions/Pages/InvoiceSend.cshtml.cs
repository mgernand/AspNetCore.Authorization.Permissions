namespace SamplePermissions.Pages
{
	using AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;

	[HasPermission("Invoice.Send")]
	public class InvoiceSendModel : PageModel
	{
		private readonly ILogger<InvoiceReadModel> _logger;

		public InvoiceSendModel(ILogger<InvoiceReadModel> logger, ApplicationDbContext context)
		{
			this._logger = logger;
			this.Context = context;
		}

		public ApplicationDbContext Context { get; }

		public void OnGet()
		{
		}
	}
}
