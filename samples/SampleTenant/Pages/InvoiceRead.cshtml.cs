namespace SampleTenant.Pages
{
	using AspNetCore.Authorization.Permissions.Abstractions;
	using Fluxera.Utilities.Extensions;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;

	// [HasPermission("Invoice.Read")]
	public class InvoiceReadModel : PageModel
	{
		private readonly ILogger<InvoiceReadModel> _logger;

		public InvoiceReadModel(ILogger<InvoiceReadModel> logger, ApplicationDbContext context)
		{
			this._logger = logger;
			this.Context = context;
		}

		public ApplicationDbContext Context { get; }

		public IActionResult OnGet()
		{
			if(!this.User.HasPermission("Invoice.Read"))
			{
				if(this.User.IsAuthenticated())
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
