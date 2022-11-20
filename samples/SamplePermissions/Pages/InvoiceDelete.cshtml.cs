namespace SamplePermissions.Pages
{
	using System;
	using System.Linq;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;
	using SamplePermissions.Model;

	[MadEyeMatt.AspNetCore.Authorization.Permissions.HasPermissionAttribute("Invoice.Delete")]
	public class InvoiceDeleteModel : PageModel
	{
		private readonly ILogger<InvoiceReadModel> logger;

		public InvoiceDeleteModel(ILogger<InvoiceReadModel> logger, InvoicesDbContext context)
		{
			this.logger = logger;
			this.Context = context;
		}

		public InvoicesDbContext Context { get; }

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
