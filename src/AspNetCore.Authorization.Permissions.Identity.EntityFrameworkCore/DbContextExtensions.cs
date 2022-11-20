namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore
{
	using System.Linq;
	using MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.ChangeTracking;

	/// <summary>
	///     Extension methods for the <see cref="DbContext" /> type.
	/// </summary>
	public static class DbContextExtensions
	{
		/// <summary>
		///     Sets the given tenant ID to the added entities.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="tenantId"></param>
		public static void SetTenantIdToAddedEntities(this DbContext context, string tenantId)
		{
			foreach(EntityEntry entry in context.ChangeTracker.Entries().Where(x => x.State == EntityState.Added))
			{
				if(entry.Entity is ITenantObject tenantObject)
				{
					if(string.IsNullOrWhiteSpace(tenantObject.TenantId))
					{
						tenantObject.TenantId = tenantId;
					}
				}
			}
		}
	}
}
