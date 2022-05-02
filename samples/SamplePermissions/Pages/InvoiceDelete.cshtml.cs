namespace SamplePermissions.Pages
{
	using AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;

	[HasPermission("Invoice.Delete")]
	public class InvoiceDeleteModel : PageModel
	{
		private readonly ILogger<InvoiceReadModel> _logger;

		public InvoiceDeleteModel(ILogger<InvoiceReadModel> logger, ApplicationDbContext context)
		{
			this._logger = logger;
			this.Context = context;
		}

		public ApplicationDbContext Context { get; }

		public void OnGet()
		{
		}

		public void OnPostDelete(string id)
		{
		}
	}
}
