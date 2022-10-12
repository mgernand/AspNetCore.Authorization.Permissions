namespace SamplePermissions.Pages
{
	using AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;
	using SamplePermissions.Model;

	[HasPermission("Invoice.Write")]
	public class InvoiceWriteModel : PageModel
	{
		private readonly ILogger<InvoiceReadModel> logger;

		public InvoiceWriteModel(ILogger<InvoiceReadModel> logger, InvoicesDbContext context)
		{
			this.logger = logger;
			this.Context = context;
		}

		public InvoicesDbContext Context { get; }

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
