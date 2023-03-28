namespace MongoSampleTenant.Pages
{
	using System;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;
	using MongoDB.Driver;
	using MongoSampleTenant;
	using MongoSampleTenant.Model;

	[HasPermission("Invoice.Delete")]
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
			Invoice invoice = this.Context.GetCollection<Invoice>().Find(x => x.Id.Equals(id)).FirstOrDefault();
			if (invoice != null)
			{
				this.Context.GetCollection<Invoice>().DeleteOne(x => x.Id == invoice.Id);
				//this.Context.SaveChanges();
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
