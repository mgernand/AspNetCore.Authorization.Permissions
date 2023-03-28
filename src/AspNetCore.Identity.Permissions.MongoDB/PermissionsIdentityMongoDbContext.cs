namespace MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB
{
	using System;
	using global::MongoDB.Driver;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.MongoDB;

	/// <summary>
	///     An permissions identity database context for MongoDB.
	/// </summary>
	[PublicAPI]
	public class PermissionsIdentityMongoDbContext : IdentityMongoDbContext
	{
		/// <inheritdoc />
		public PermissionsIdentityMongoDbContext(IMongoDatabase database)
			: base(database)
		{
		}

		/// <summary>
		///     Gets the name for the users collection.
		/// </summary>
		protected virtual string TenantsCollectionName => "AspNetTenants";

		/// <summary>
		///     Gets the name for the roles collection.
		/// </summary>
		protected virtual string PermissionsCollectionName => "AspNetPermissions";

		/// <inheritdoc />
		public override string GetCollectionName<TDocument>()
		{
			string collectionName = base.GetCollectionName<TDocument>();

			Type currentType = typeof(TDocument);
			if(IsGenericBaseType(currentType, typeof(IdentityTenant<>)))
			{
				collectionName = this.TenantsCollectionName;
			}
			else if(IsGenericBaseType(currentType, typeof(IdentityPermission<>)))
			{
				collectionName = this.PermissionsCollectionName;
			}

			return collectionName;
		}
	}
}
