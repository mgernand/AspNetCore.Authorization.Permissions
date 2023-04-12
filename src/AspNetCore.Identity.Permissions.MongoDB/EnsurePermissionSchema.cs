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
	internal sealed class EnsurePermissionSchema<TPermission, TKey, TContext> : EnsureSchemaBase<TContext>
		where TPermission : MongoIdentityPermission<TKey>
		where TKey : IEquatable<TKey>
		where TContext : MongoDbContext
	{
		private readonly TContext context;

		public EnsurePermissionSchema(TContext context) : base(context)
		{
			this.context = context;
		}

		/// <inheritdoc />
		public override  async Task ExecuteAsync()
		{
            bool exists = await this.CollectionExistsAsync<TPermission>();
			if (!exists)
			{
				string collectionName = this.context.GetCollectionName<TPermission>();
				await this.context.Database.CreateCollectionAsync(collectionName);

				IMongoCollection<TPermission> collection = this.context.GetCollection<TPermission>();

				await collection.Indexes.CreateManyAsync(new List<CreateIndexModel<TPermission>>
				{
					CreateIndexModel(x => x.NormalizedName),
				});
			}
		}

		private static CreateIndexModel<TPermission> CreateIndexModel(Expression<Func<TPermission, object>> field)
		{
			return new CreateIndexModel<TPermission>(
				Builders<TPermission>.IndexKeys.Ascending(field),
				new CreateIndexOptions<TPermission>
				{
					Unique = true,
					Name = $"{field.GetFieldName()}_asc"
				});
		}
	}
}
