namespace MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading.Tasks;
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
		/// Gets the name for the users collection.</summary>
		protected virtual string TenantsCollectionName => "AspNetTenants";

		/// <summary>
		/// Gets the name for the roles collection.</summary>
		protected virtual string PermissionsCollectionName => "AspNetPermissions";

        ///  <summary>
        /// 		Makes sure the collections and indexes exist in the database.
        ///  </summary>
        ///  <typeparam name="TUser"></typeparam>
        ///  <typeparam name="TRole"></typeparam>
		///  <typeparam name="TTenant"></typeparam>
        ///  <typeparam name="TPermission"></typeparam>
        ///  <typeparam name="TKey"></typeparam>
        ///  <returns></returns>	
        public async Task EnsureSchema<TUser, TRole, TTenant, TPermission, TKey>()
			where TUser : MongoIdentityUser<TKey>
			where TRole : MongoIdentityRole<TKey>
			where TTenant : MongoIdentityTenant<TKey>
            where TPermission : MongoIdentityPermission<TKey>
            where TKey : IEquatable<TKey>
		{
			await this.EnsureSchema<TUser, TRole, TKey>();

			bool existsTenantsCollection = await this.Database.CollectionExistsAsync(this.TenantsCollectionName);
			if (!existsTenantsCollection)
			{
				await this.Database.CreateCollectionAsync(this.TenantsCollectionName);

				IMongoCollection<TTenant> collection = this.GetCollection<TTenant>();

				await collection.Indexes.CreateManyAsync(new List<CreateIndexModel<TTenant>>
				{
					CreateTenantIndexModel<TTenant, TKey>(x => x.NormalizedName),
				});
			}

			bool existsPermissionsCollection = await this.Database.CollectionExistsAsync(this.PermissionsCollectionName);
			if (!existsPermissionsCollection)
			{
				await this.Database.CreateCollectionAsync(this.PermissionsCollectionName);

				IMongoCollection<TPermission> collection = this.GetCollection<TPermission>();

				await collection.Indexes.CreateManyAsync(new List<CreateIndexModel<TPermission>>
				{
					CreatePermissionIndexModel<TPermission, TKey>(x => x.NormalizedName),
				});
			}
        }

		private CreateIndexModel<TTenant> CreateTenantIndexModel<TTenant, TKey>(Expression<Func<TTenant, object>> field)
			where TTenant : MongoIdentityTenant<TKey>
			where TKey : IEquatable<TKey>
		{
			return new CreateIndexModel<TTenant>(
				Builders<TTenant>.IndexKeys.Ascending(field),
				new CreateIndexOptions<TTenant>
				{
					Unique = true
				});
		}

		private CreateIndexModel<TPermission> CreatePermissionIndexModel<TPermission, TKey>(Expression<Func<TPermission, object>> field)
			where TPermission : MongoIdentityPermission<TKey>
			where TKey : IEquatable<TKey>
		{
			return new CreateIndexModel<TPermission>(
				Builders<TPermission>.IndexKeys.Ascending(field),
				new CreateIndexOptions<TPermission>
				{
					Unique = true
				});
		}

        /// <inheritdoc />
        protected override string GetCollectionName<TDocument>()
		{
			string collectionName = base.GetCollectionName<TDocument>();

			Type currentType = typeof(TDocument);
			if(IsGenericBaseType(currentType, typeof(IdentityTenant<>)))
			{
				collectionName = this.UsersCollectionName;
			}
			else if(IsGenericBaseType(currentType, typeof(IdentityPermission<>)))
			{
				collectionName = this.RolesCollectionName;
			}

			return collectionName;
		}
	}
}
