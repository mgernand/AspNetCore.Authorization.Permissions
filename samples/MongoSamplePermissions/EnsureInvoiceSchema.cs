namespace MongoSamplePermissions
{
	using System.Threading.Tasks;
	using MadEyeMatt.AspNetCore.Identity.MongoDB;
	using MongoSamplePermissions.Model;

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
			}
        }
	}
}