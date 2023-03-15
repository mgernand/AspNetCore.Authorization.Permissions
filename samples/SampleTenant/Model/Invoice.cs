namespace SampleTenant.Model
{
	using System;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;

	public class Invoice : ITenantObject
	{
		public Guid Id { get; set; }

		public decimal Total { get; set; }

		public string Note { get; set; }

		/// <inheritdoc />
		public string TenantID { get; set; }
	}
}
