namespace SampleTenant.Pages
{
	using AspNetCore.Authorization.Permissions;
	using AspNetCore.Authorization.Permissions.Abstractions;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using SampleTenant.Model;

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

		public string GetTenantName(string tenantId)
		{
			string id = this.User.GetTenantId();
			if(id == tenantId)
			{
				return this.User.GetTenantName();
			}

			return string.Empty;
		}
	}
}
