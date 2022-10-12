namespace SampleTenant.Model
{
	using System;
	using AspNetCore.Authorization.Permissions.Abstractions;

	public class Invoice : ITenantObject
	{
		public Guid Id { get; set; }

		public decimal Total { get; set; }

		public string Note { get; set; }

		/// <inheritdoc />
		public string TenantId { get; set; }
	}
}
