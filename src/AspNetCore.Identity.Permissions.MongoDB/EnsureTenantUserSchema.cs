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
			if (!indexes.Exists(x => x["name"] == "tenantId_asc"))
			{
				await collection.Indexes.CreateManyAsync(new List<CreateIndexModel<TUser>>
				{
					CreateIndexModel(x => x.TenantId),
				});
			}
		}

		private static CreateIndexModel<TUser> CreateIndexModel(Expression<Func<TUser, object>> field)
		{
			return new CreateIndexModel<TUser>(
				Builders<TUser>.IndexKeys.Ascending(field),
				new CreateIndexOptions<TUser>
				{
					Unique = false,
					Name = $"{field.GetFieldName()}_asc"
                });
		}
	}
}
