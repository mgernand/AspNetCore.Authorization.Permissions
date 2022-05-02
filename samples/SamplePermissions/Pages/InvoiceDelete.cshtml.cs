namespace SamplePermissions.Pages
{
	using AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using SamplePermissions.Model;

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

		public void OnPostDelete(Guid id)
		{
			Invoice invoice = this.Context.Invoices.FirstOrDefault(x => x.Id == id);
			if(invoice != null)
			{
				this.Context.Invoices.Remove(invoice);
				this.Context.SaveChanges();
			}
		}
	}
}
