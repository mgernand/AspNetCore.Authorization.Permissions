namespace SampleTenant.Model
{
	using System;

    public class Invoice : MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.ITenantObject
	{
		public Guid Id { get; set; }

		public decimal Total { get; set; }

		public string Note { get; set; }

		/// <inheritdoc />
		public string TenantId { get; set; }
	}
}
