namespace SampleTenant
{
	using System;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;

	[PublicAPI]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
	public class RequireInvoiceDeletePermissionAttribute : RequirePermissionAttribute
	{
		/// <inheritdoc />
		public RequireInvoiceDeletePermissionAttribute() 
			: base("Invoice.Delete")
		{
		}
	}
}