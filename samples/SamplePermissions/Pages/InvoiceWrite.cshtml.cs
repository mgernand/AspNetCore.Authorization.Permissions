namespace SamplePermissions.Pages
{
	using AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;

	[HasPermission("Invoice.Write")]
	public class InvoiceWriteModel : PageModel
	{
		private readonly ILogger<InvoiceWriteModel> _logger;

		public InvoiceWriteModel(ILogger<InvoiceWriteModel> logger)
		{
			this._logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
