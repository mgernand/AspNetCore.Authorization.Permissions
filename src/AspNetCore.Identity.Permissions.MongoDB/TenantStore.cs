namespace MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Threading;
	using System.Threading.Tasks;
	using global::MongoDB.Driver;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.MongoDB;
	using MadEyeMatt.MongoDB.DbContext;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Represents a new instance of a persistence store for the specified tenant, role and db context types.
	/// </summary>
	/// <typeparam name="TTenant">The type of the class representing a tenant.</typeparam>
	/// <typeparam name="TRole">The type representing a role.</typeparam>
	/// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
	[PublicAPI]
	public class TenantStore<TTenant, TRole, TContext> : TenantStore<TTenant, TRole, TContext, string>
		where TTenant : MongoIdentityTenant<string>
        where TRole : MongoIdentityRole<string>
        where TContext : MongoDbContext
    {
		/// <inheritdoc />
		public TenantStore(TContext context, IdentityErrorDescriber describer = null)
			: base(context, describer)
		{
		}
	}

	/// <summary>
	///     Represents a new instance of a persistence store for the specified tenant, role, db context, key and role types.
	/// </summary>
	/// <typeparam name="TTenant">The type of the class representing a tenant.</typeparam>
	/// <typeparam name="TRole">The type representing a role.</typeparam>
	/// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
	/// <typeparam name="TKey">The type of the primary key for a tenant.</typeparam>
	[PublicAPI]
	public class TenantStore<TTenant, TRole, TContext, TKey> : TenantStoreBase<TTenant, TRole, TKey>,
		IQueryableTenantStore<TTenant>
		where TTenant : MongoIdentityTenant<TKey>
		where TRole : MongoIdentityRole<TKey>
		where TKey : IEquatable<TKey>
		where TContext : MongoDbContext
	{
		private readonly TContext context;

		/// <inheritdoc />
		public TenantStore(TContext context, IdentityErrorDescriber describer = null)
			: base(describer)
		{
			ArgumentNullException.ThrowIfNull(context);

			this.context = context;
        }

		/// <inheritdoc />
		public IQueryable<TTenant> Tenants => this.TenantsCollection.AsQueryable();

		/// <summary>
		///     The collection of tenants in the database.
		/// </summary>
		public virtual IMongoCollection<TTenant> TenantsCollection => this.context.GetCollection<TTenant>();

		/// <summary>
		///     The collection of roles in the database.
		/// </summary>
		public virtual IMongoCollection<TRole> RolesCollection => this.context.GetCollection<TRole>();

		/// <inheritdoc />
        public override async Task<IdentityResult> CreateAsync(TTenant tenant, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(tenant);

			tenant.ConcurrencyStamp = Guid.NewGuid().ToString("N");
			await this.TenantsCollection.InsertOneAsync(tenant, new InsertOneOptions(), cancellationToken);

			return IdentityResult.Success;
        }

		/// <inheritdoc />
		public override async Task<IdentityResult> UpdateAsync(TTenant tenant, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(tenant);

			string oldConcurrencyStamp = tenant.ConcurrencyStamp;
			tenant.ConcurrencyStamp = Guid.NewGuid().ToString("N");

			Expression<Func<TTenant, bool>> predicate = x => x.Id.Equals(tenant.Id) && x.ConcurrencyStamp.Equals(oldConcurrencyStamp);
			ReplaceOneResult result = await this.TenantsCollection.ReplaceOneAsync(predicate, tenant, cancellationToken: cancellationToken);

			return result.ModifiedCount == 0
				? IdentityResult.Failed(this.ErrorDescriber.ConcurrencyFailure())
				: IdentityResult.Success;
        }

		/// <inheritdoc />
		public override async Task<IdentityResult> DeleteAsync(TTenant tenant, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(tenant);

			string oldConcurrencyStamp = tenant.ConcurrencyStamp;
			tenant.ConcurrencyStamp = Guid.NewGuid().ToString("N");

			Expression<Func<TTenant, bool>> predicate = x => x.Id.Equals(tenant.Id) && x.ConcurrencyStamp.Equals(oldConcurrencyStamp);
			DeleteResult result = await this.TenantsCollection.DeleteOneAsync(predicate, cancellationToken);

			return result.DeletedCount == 0
				? IdentityResult.Failed(this.ErrorDescriber.ConcurrencyFailure())
				: IdentityResult.Success;
        }

		/// <inheritdoc />
		public override async Task<TTenant> FindByIdAsync(string id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();

			TKey userId = this.ConvertIdFromString(id);
			return await this.TenantsCollection.Find(x => x.Id.Equals(userId)).FirstOrDefaultAsync(cancellationToken);
        }

		/// <inheritdoc />
		public override async Task<TTenant> FindByNameAsync(string normalizedTenantName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();

			return await this.TenantsCollection.Find(x => x.NormalizedName == normalizedTenantName).FirstOrDefaultAsync(cancellationToken);
        }

		/// <inheritdoc />
		protected override async Task<TTenant> FindTenantAsync(TKey tenantId, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();

			return await this.TenantsCollection.Find(x => x.Id.Equals(tenantId)).FirstOrDefaultAsync(cancellationToken);
        }

		/// <inheritdoc />
		protected override async Task<TRole> FindRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();

            return await this.RolesCollection.Find(x => x.NormalizedName == normalizedRoleName).FirstOrDefaultAsync(cancellationToken);
        }

		/// <inheritdoc />
		public override async Task AddToRoleAsync(TTenant tenant, string normalizedRoleName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(tenant);
			ArgumentException.ThrowIfNullOrEmpty(normalizedRoleName);

			TRole role = await this.FindRoleAsync(normalizedRoleName, cancellationToken);
			if (role is null)
			{
				throw new InvalidOperationException($"The role '{normalizedRoleName}' was not found.");
			}

			tenant.AddRole(role.Id);
		}

		/// <inheritdoc />
        public override async Task RemoveFromRoleAsync(TTenant tenant, string normalizedRoleName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(tenant);
			ArgumentException.ThrowIfNullOrEmpty(normalizedRoleName);

			TRole role = await this.FindRoleAsync(normalizedRoleName, cancellationToken);
			if (role is not null)
			{
				tenant.RemoveRole(role.Id);
			}
        }

		/// <inheritdoc />
		public override async Task<IList<string>> GetRolesAsync(TTenant tenant, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(tenant);

			if (tenant.Roles.Any())
			{
				return await this.RolesCollection
					.Find(x => tenant.Roles.Contains(x.Id))
					.Project(x => x.Name)
					.ToListAsync(cancellationToken);
			}

			return new List<string>(0);
        }

		/// <inheritdoc />
		public override async Task<IList<string>> GetRoleIdsAsync(TTenant tenant, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(tenant);

			if (tenant.Roles.Any())
			{
				IList<TKey> keys = await this.RolesCollection
					.Find(x => tenant.Roles.Contains(x.Id))
					.Project(x => x.Id)
					.ToListAsync(cancellationToken);

				return keys.Select(this.ConvertIdToString).ToList();
            }

			return new List<string>(0);
        }

		/// <inheritdoc />
		public override async Task<bool> IsInRoleAsync(TTenant tenant, string normalizedRoleName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(tenant);
			ArgumentException.ThrowIfNullOrEmpty(normalizedRoleName);

			TRole role = await this.RolesCollection.Find(x => x.NormalizedName == normalizedRoleName).FirstOrDefaultAsync(cancellationToken);
			return role is not null && tenant.Roles.Any(x => x.Equals(role.Id));
        }

		/// <inheritdoc />
		public override async Task<IList<TTenant>> GetTenantsInRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			ArgumentException.ThrowIfNullOrEmpty(normalizedRoleName);

			TRole role = await this.FindRoleAsync(normalizedRoleName, cancellationToken);
			if (role is not null)
			{
				return await this.TenantsCollection.Find(x => x.Roles.Contains(role.Id)).ToListAsync(cancellationToken);
			}

			return new List<TTenant>(0);
        }
	}
}
