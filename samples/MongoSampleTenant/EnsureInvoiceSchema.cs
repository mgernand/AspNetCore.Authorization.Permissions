namespace MongoSampleTenant
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Threading.Tasks;
	using MadEyeMatt.AspNetCore.Identity.MongoDB;
	using MongoDB.Driver;
	using MongoSampleTenant.Model;

	public class EnsureInvoiceSchema : IEnsureSchema
	{
		private readonly MongoDbContext context;

		public EnsureInvoiceSchema(MongoDbContext context)
		{
			this.context = context;
		}

        /// <inheritdoc />
        public async Task ExecuteAsync()
		{
			bool exists = await this.context.CollectionExistsAsync<Invoice>();
			if (!exists)
			{
				string collectionName = this.context.GetCollectionName<Invoice>();
				await this.context.Database.CreateCollectionAsync(collectionName);

				IMongoCollection<Invoice> collection = this.context.GetCollection<Invoice>();

				await collection.Indexes.CreateManyAsync(new List<CreateIndexModel<Invoice>>
				{
					CreateIndexModel(x => x.TenantID, "Invoice_TenantID_Index"),
				});
			}
        }

		private static CreateIndexModel<Invoice> CreateIndexModel(Expression<Func<Invoice, object>> field, string name)
		{
			return new CreateIndexModel<Invoice>(
				Builders<Invoice>.IndexKeys.Ascending(field),
				new CreateIndexOptions<Invoice>
				{
					Unique = false,
					Name = name
				});
		}
    }
}