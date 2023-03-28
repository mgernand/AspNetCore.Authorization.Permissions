namespace MongoSamplePermissions
{
	using MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB;
	using MongoDB.Driver;

	public class InvoicesContext : PermissionsIdentityMongoDbContext
    {
		/// <inheritdoc />
		public InvoicesContext(IMongoDatabase database) 
			: base(database)
		{
		}

		protected override string UsersCollectionName => "Users";

		protected override string RolesCollectionName => "Roles";

        protected override string TenantsCollectionName => "Tenants";

		protected override string PermissionsCollectionName => "Permissions";
	}
}
