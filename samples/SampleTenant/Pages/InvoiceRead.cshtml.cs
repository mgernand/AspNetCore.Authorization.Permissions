namespace SampleTenant.Pages
{
	using AspNetCore.Authorization.Permissions.Abstractions;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	// [HasPermission("Invoice.Read")]
	public class InvoiceReadModel : PageModel
	{
		private readonly ILogger<InvoiceReadModel> logger;

		public InvoiceReadModel(ILogger<InvoiceReadModel> logger, InvoicesDbContext context)
		{
			this.logger = logger;
			this.Context = context;
		}

		public InvoicesDbContext Context { get; }

		public IActionResult OnGet()
		{
			if(!this.User.HasPermission("Invoice.Read"))
			{
				if(this.User.Identity != null && this.User.Identity.IsAuthenticated)
				{
					return this.Forbid();
				}

				return this.Challenge();
			}

			return this.Page();
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
