namespace MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore
{
	using System.Linq;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
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
		/// <param name="tenantID"></param>
		public static void SetTenantIdToAddedEntities(this DbContext context, string tenantID)
		{
			foreach(EntityEntry entry in context.ChangeTracker.Entries().Where(x => x.State == EntityState.Added))
			{
				if(entry.Entity is ITenantObject tenantObject)
				{
					if(string.IsNullOrWhiteSpace(tenantObject.TenantID))
					{
						tenantObject.TenantID = tenantID;
					}
				}
			}
		}
	}
}
