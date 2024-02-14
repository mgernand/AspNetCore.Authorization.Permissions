namespace MongoSampleTenant
{
	using System;
	using MongoSampleTenant.Model;
	using MadEyeMatt.Extensions.Identity.Permissions.Stores;
	using MadEyeMatt.MongoDB.DbContext;
	using Microsoft.AspNetCore.Identity;

	public class InvoicesContext : MongoDbContext
    {
		/// <inheritdoc />
		public InvoicesContext(MongoDbContextOptions options)
			: base(options)
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

		/// <summary>
		///		Checks if the current type has the given base type.
		/// </summary>
		private static bool IsGenericBaseType(Type currentType, Type genericBaseType)
		{
			if (currentType == genericBaseType)
			{
				return true;
			}

			Type type = currentType;
			while (type != null)
			{
				Type genericType = type.IsGenericType ? type.GetGenericTypeDefinition() : null;
				if (genericType != null && genericType == genericBaseType)
				{
					return true;
				}

				type = type.BaseType;
			}

			return false;
		}
	}
}
