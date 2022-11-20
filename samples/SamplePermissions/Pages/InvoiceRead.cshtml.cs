namespace SamplePermissions.Pages
{
    using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;
    using ClaimsPrincipalExtensions = MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.ClaimsPrincipalExtensions;

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
			if(!ClaimsPrincipalExtensions.HasPermission(this.User, "Invoice.Read"))
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
