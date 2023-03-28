namespace MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB
{
	using System;
	using System.Collections.Generic;
    using System.Linq.Expressions;
	using System.Threading.Tasks;
	using global::MongoDB.Bson;
	using global::MongoDB.Driver;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.MongoDB;

	[UsedImplicitly]
	internal sealed class EnsureTenantUserSchema<TUser, TKey> : IEnsureSchema
		where TUser : MongoIdentityTenantUser<TKey>
		where TKey : IEquatable<TKey>
	{
		private readonly MongoDbContext context;

		public EnsureTenantUserSchema(MongoDbContext context)
		{
			this.context = context;
		}

		/// <inheritdoc />
		public async Task ExecuteAsync()
		{
			IMongoCollection<TUser> collection = this.context.GetCollection<TUser>();

			List<BsonDocument> indexes = await (await collection.Indexes.ListAsync()).ToListAsync();
			if(!indexes.Exists(x => x["name"] == "User_TenantID_Index"))
			{
				await collection.Indexes.CreateManyAsync(new List<CreateIndexModel<TUser>>
				{
					CreateIndexModel(x => x.TenantID, "User_TenantID_Index"),
				});
            }
		}

		private static CreateIndexModel<TUser> CreateIndexModel(Expression<Func<TUser, object>> field, string name)
		{
			return new CreateIndexModel<TUser>(
				Builders<TUser>.IndexKeys.Ascending(field),
				new CreateIndexOptions<TUser>
				{
					Unique = false,
					Name = name
				});
		}
	}
}
