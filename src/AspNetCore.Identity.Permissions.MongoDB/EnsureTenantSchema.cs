namespace MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading.Tasks;
	using global::MongoDB.Driver;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.MongoDB.Initialization;
	using MadEyeMatt.MongoDB.DbContext;
	using MadEyeMatt.MongoDB.DbContext.Initialization;

	[UsedImplicitly]
	internal sealed class EnsureTenantSchema<TTenant, TKey, TContext> : EnsureSchemaBase<TContext>
		where TTenant : MongoIdentityTenant<TKey>
		where TKey : IEquatable<TKey>
		where TContext : MongoDbContext
	{
		private readonly TContext context;

		public EnsureTenantSchema(TContext context) : base(context)
		{
			this.context = context;
		}

		/// <inheritdoc />
		public override async Task ExecuteAsync()
		{
			bool exists = await this.CollectionExistsAsync<TTenant>();
			if (!exists)
			{
				string collectionName = this.context.GetCollectionName<TTenant>();
				await this.context.Database.CreateCollectionAsync(collectionName);

				IMongoCollection<TTenant> collection = this.context.GetCollection<TTenant>();

				await collection.Indexes.CreateManyAsync(new List<CreateIndexModel<TTenant>>
				{
					CreateIndexModel(x => x.NormalizedName),
				});
			}
		}

		private static CreateIndexModel<TTenant> CreateIndexModel(Expression<Func<TTenant, object>> field)
		{
			return new CreateIndexModel<TTenant>(
				Builders<TTenant>.IndexKeys.Ascending(field),
				new CreateIndexOptions<TTenant>
				{
					Unique = true,
					Name = $"{field.GetFieldName()}_asc"
                });
		}
	}
}
