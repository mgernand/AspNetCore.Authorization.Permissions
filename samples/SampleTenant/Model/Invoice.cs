namespace SampleTenant.Model
{
	public class Invoice : ITenantObject
	{
		public Guid Id { get; set; }

		public decimal Total { get; set; }

		public string Note { get; set; }

		/// <inheritdoc />
		public string TenantId { get; set; }
	}
}
