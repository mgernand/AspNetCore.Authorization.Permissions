namespace MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB
{
	using System;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.MongoDB;

	/// <summary>
	///		Extension methods for the <see cref="IServiceProvider"/> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceProviderExtensions
	{
		/// <summary>
		///		Initializes the MongoDB driver and ensures schema and indexes.
		/// </summary>
		/// <param name="serviceProvider"></param>
		/// <returns></returns>
		public static async Task InitializePermissionMongoDbStores(this IServiceProvider serviceProvider)
		{
			await serviceProvider.InitializeMongoDbStores();


		}
	}
}
