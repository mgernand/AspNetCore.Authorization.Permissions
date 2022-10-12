﻿namespace SampleTenant.Pages
{
	using Microsoft.AspNetCore.Mvc.RazorPages;
	using Microsoft.Extensions.Logging;

	public class PrivacyModel : PageModel
	{
		private readonly ILogger<PrivacyModel> logger;

		public PrivacyModel(ILogger<PrivacyModel> logger)
		{
			this.logger = logger;
		}

		public void OnGet()
		{
		}
	}
}
