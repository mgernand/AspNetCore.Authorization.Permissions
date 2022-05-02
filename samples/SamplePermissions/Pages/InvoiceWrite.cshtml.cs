namespace SamplePermissions.Pages
{
	using AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using SamplePermissions.Model;

	[HasPermission("Invoice.Write")]
	public class InvoiceWriteModel : PageModel
	{
		private readonly ILogger<InvoiceReadModel> _logger;

		public InvoiceWriteModel(ILogger<InvoiceReadModel> logger, ApplicationDbContext context)
		{
			this._logger = logger;
			this.Context = context;
		}

		public ApplicationDbContext Context { get; }

		public void OnGet()
		{
		}

		public void OnPost(decimal total, string note)
		{
			Invoice invoice = new Invoice
			{
				Total = total,
				Note = note
			};

			this.Context.Invoices.Add(invoice);
			this.Context.SaveChanges();
		}
	}
}
