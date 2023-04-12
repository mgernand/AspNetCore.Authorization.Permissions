namespace MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB
{
	using System;
	using System.Collections.Generic;
    using System.Linq.Expressions;
	using System.Threading.Tasks;
	using global::MongoDB.Bson;
	using global::MongoDB.Driver;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.MongoDB.Initialization;
	using MadEyeMatt.MongoDB.DbContext;
	using MadEyeMatt.MongoDB.DbContext.Initialization;

	[UsedImplicitly]
	internal sealed class EnsureTenantUserSchema<TUser, TKey, TContext> : EnsureSchemaBase<TContext>
		where TUser : MongoIdentityTenantUser<TKey>
		where TKey : IEquatable<TKey>
		where TContext : MongoDbContext
	{
		private readonly TContext context;

		public EnsureTenantUserSchema(TContext context) : base(context) 
		{
			this.context = context;
		}

		/// <inheritdoc />
		public override async Task ExecuteAsync()
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
