namespace MongoSamplePermissions.Pages
{
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;
	using MongoSamplePermissions;

	// [HasPermission("Invoice.Read")]
	public class InvoiceReadModel : PageModel
	{
		private readonly ILogger<InvoiceReadModel> logger;

		public InvoiceReadModel(ILogger<InvoiceReadModel> logger, InvoicesContext context)
		{
			this.logger = logger;
			this.Context = context;
		}

		public InvoicesContext Context { get; }

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
	}
}
