namespace SampleTenant.Pages
{
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;
	using SampleTenant.Model;

	[HasPermission("Invoice.Write")]
	public class InvoiceWriteModel : PageModel
	{
		private readonly ILogger<InvoiceReadModel> logger;

		public InvoiceWriteModel(ILogger<InvoiceReadModel> logger, InvoicesContext context)
		{
			this.logger = logger;
			this.Context = context;
		}

		public InvoicesContext Context { get; }

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

			this.Context.Set<Invoice>().Add(invoice);
			this.Context.SaveChanges();
		}
	}
}
