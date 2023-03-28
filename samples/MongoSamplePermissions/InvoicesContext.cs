namespace MongoSamplePermissions
{
	using System;
	using MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB;
	using MongoDB.Driver;
	using MongoSamplePermissions.Model;

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

		/// <inheritdoc />
		public override string GetCollectionName<TDocument>()
		{
			string collectionName = base.GetCollectionName<TDocument>();

			Type type = typeof(TDocument);
			if(type == typeof(Invoice))
			{
				collectionName = "Invoices";
			}

			return collectionName;
		}
	}
}
