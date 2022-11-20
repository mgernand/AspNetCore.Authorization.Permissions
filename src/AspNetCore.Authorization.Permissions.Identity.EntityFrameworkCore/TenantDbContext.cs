namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore
{
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
	///     Base class for the Entity Framework database context used for identity.
	/// </summary>
	[PublicAPI]
	public abstract class TenantDbContext : DbContext
	{
		/// <summary>
		///     Initializes a new instance of <see cref="IdentityDbContext" />.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		/// <param name="tenantProvider"></param>
		protected TenantDbContext(DbContextOptions options, MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.ITenantProvider tenantProvider)
			: base(options)
		{
			this.TenantProvider = tenantProvider;
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="IdentityDbContext" /> class.
		/// </summary>
		protected TenantDbContext()
		{
		}

		/// <summary>
		///     Gets the tenant provider.
		/// </summary>
		protected MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.ITenantProvider TenantProvider { get; }

		/// <inheritdoc />
		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			this.SetTenantIdToAddedEntities(this.TenantProvider.TenantId);

			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		/// <inheritdoc />
		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
		{
			this.SetTenantIdToAddedEntities(this.TenantProvider.TenantId);

			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
	}
}
