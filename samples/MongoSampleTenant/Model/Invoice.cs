namespace MongoSampleTenant.Model
{
	using MadEyeMatt.AspNetCore.Authorization.Permissions;

	public class Invoice : ITenantObject
	{
		public string Id { get; set; }

		public decimal Total { get; set; }

		public string Note { get; set; }

		/// <inheritdoc />
		public string TenantID { get; set; }
	}
}
