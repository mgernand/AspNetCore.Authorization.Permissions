namespace SampleTenant.Pages
{
	using System;
	using System.Linq;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;
	using SampleTenant.Model;

	[MadEyeMatt.AspNetCore.Authorization.Permissions.HasPermissionAttribute("Invoice.Delete")]
	public class InvoiceDeleteModel : PageModel
	{
		private readonly ILogger<InvoiceReadModel> logger;

		public InvoiceDeleteModel(ILogger<InvoiceReadModel> logger, InvoicesContext context)
		{
			this.logger = logger;
			this.Context = context;
		}

		public InvoicesContext Context { get; }

		public void OnGet()
		{
		}

		public void OnPostDelete(Guid id)
		{
			Invoice invoice = this.Context.Set<Invoice>().FirstOrDefault(x => x.Id == id);
			if(invoice != null)
			{
				this.Context.Set<Invoice>().Remove(invoice);
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
