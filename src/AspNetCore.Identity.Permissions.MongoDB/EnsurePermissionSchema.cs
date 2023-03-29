namespace MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading.Tasks;
	using global::MongoDB.Driver;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.MongoDB;

	[UsedImplicitly]
	internal sealed class EnsurePermissionSchema<TPermission, TKey> : IEnsureSchema
		where TPermission : MongoIdentityPermission<TKey>
		where TKey : IEquatable<TKey>
	{
		private readonly MongoDbContext context;

		public EnsurePermissionSchema(MongoDbContext context)
		{
			this.context = context;
		}

		/// <inheritdoc />
		public async Task ExecuteAsync()
		{
            bool exists = await this.context.CollectionExistsAsync<TPermission>();
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
