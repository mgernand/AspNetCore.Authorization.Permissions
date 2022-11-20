﻿namespace SamplePermissions.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	[MadEyeMatt.AspNetCore.Authorization.Permissions.HasPermissionAttribute("Invoice.Send")]
	public class InvoiceSendModel : PageModel
	{
		private readonly ILogger<InvoiceReadModel> logger;

		public InvoiceSendModel(ILogger<InvoiceReadModel> logger, ApplicationDbContext context)
		{
			this.logger = logger;
			this.Context = context;
		}

		public ApplicationDbContext Context { get; }

		public void OnGet()
		{
		}
	}
}
