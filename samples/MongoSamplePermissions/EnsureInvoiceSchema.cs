namespace MongoSamplePermissions
{
	using System.Threading.Tasks;
	using MadEyeMatt.MongoDB.DbContext;
	using MadEyeMatt.MongoDB.DbContext.Initialization;
	using MongoSamplePermissions.Model;

	public class EnsureInvoiceSchema<TContext> : EnsureSchemaBase<TContext> 
		where TContext : MongoDbContext
	{
		private readonly TContext context;

		public EnsureInvoiceSchema(TContext context) : base(context)
		{
			this.context = context;
		}

        /// <inheritdoc />
        public override async Task ExecuteAsync()
		{
			bool exists = await this.CollectionExistsAsync<Invoice>();
			if (!exists)
			{
				string collectionName = this.context.GetCollectionName<Invoice>();
				await this.context.Database.CreateCollectionAsync(collectionName);
			}
        }
	}
}