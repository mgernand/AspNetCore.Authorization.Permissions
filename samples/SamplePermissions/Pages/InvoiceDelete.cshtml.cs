namespace SamplePermissions.Pages
{
	using AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;

	[HasPermission("Invoice.Delete")]
	public class InvoiceDeleteModel : PageModel
	{
		private readonly ILogger<InvoiceDeleteModel> _logger;

		public InvoiceDeleteModel(ILogger<InvoiceDeleteModel> logger)
		{
			this._logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
