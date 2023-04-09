namespace MongoSampleTenant
{
	using System;
	using global::MongoDB.Driver;
	using MadEyeMatt.AspNetCore.Identity.MongoDB;
	using MongoSampleTenant.Model;
	using MadEyeMatt.AspNetCore.Identity.Permissions;
	using Microsoft.AspNetCore.Identity;

	public class InvoicesContext : MongoDbContext
    {
		/// <inheritdoc />
		public InvoicesContext(IMongoDatabase database) 
			: base(database)
		{
		}

		private static string UsersCollectionName => "Users";

		private static string RolesCollectionName => "Roles";

		private static string TenantsCollectionName => "Tenants";

		private static string PermissionsCollectionName => "Permissions";

		/// <inheritdoc />
		public override string GetCollectionName<TDocument>()
		{
			string collectionName = base.GetCollectionName<TDocument>();

			Type type = typeof(TDocument);

			if (IsGenericBaseType(type, typeof(IdentityUser<>)))
			{
				collectionName = UsersCollectionName;
			}
			else if (IsGenericBaseType(type, typeof(IdentityRole<>)))
			{
				collectionName = RolesCollectionName;
			}
			else if (IsGenericBaseType(type, typeof(IdentityTenant<>)))
			{
				collectionName = TenantsCollectionName;
			}
			else if (IsGenericBaseType(type, typeof(IdentityPermission<>)))
			{
				collectionName = PermissionsCollectionName;
			}
			else if (type == typeof(Invoice))
			{
				collectionName = "Invoices";
			}

			return collectionName;
		}
	}
}
