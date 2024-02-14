namespace MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Threading;
	using System.Threading.Tasks;
	using global::MongoDB.Driver;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.MongoDB;
	using MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB.Properties;
	using MadEyeMatt.Extensions.Identity.Permissions;
	using MadEyeMatt.Extensions.Identity.Permissions.Stores;
	using MadEyeMatt.MongoDB.DbContext;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Represents a new instance of a persistence store for the specified permission, role and db context types.
	/// </summary>
	/// <typeparam name="TPermission">The type of the class representing a permission.</typeparam>
	/// <typeparam name="TRole">The type of the class representing a role</typeparam>
	/// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
	[PublicAPI]
	public class PermissionStore<TPermission, TRole, TContext> : PermissionStore<TPermission, TRole, TContext, string>
		where TPermission : MongoIdentityPermission<string>
        where TRole : MongoIdentityRole<string>
        where TContext : MongoDbContext
    {
        /// <summary>
        ///     Constructs a new instance of <see cref="PermissionStore{TPermission, TRole, TContext}" />.
        /// </summary>
        /// <param name="context">The <see cref="MongoDbContext" />.</param>
        /// <param name="describer">The <see cref="IdentityErrorDescriber" />.</param>
        public PermissionStore(TContext context, IdentityErrorDescriber describer = null)
			: base(context, describer)
		{
		}
	}

	/// <summary>
	///     Represents a new instance of a persistence store for the specified permission, role, db context, key and role
	///     permission types.
	/// </summary>
	/// <typeparam name="TPermission">The type of the class representing a permission.</typeparam>
	/// <typeparam name="TRole"></typeparam>
	/// <typeparam name="TContext">The type of the data context class used to access the store.</typeparam>
	/// <typeparam name="TKey">The type of the primary key for a role.</typeparam>
	[PublicAPI]
	public class PermissionStore<TPermission, TRole, TContext, TKey> : PermissionStoreBase<TPermission, TRole, TKey>,
		IQueryablePermissionStore<TPermission>,
		IRolePermissionStore<TPermission>
		where TPermission : MongoIdentityPermission<TKey>
		where TRole : MongoIdentityRole<TKey>
		where TKey : IEquatable<TKey>
		where TContext : MongoDbContext
	{
		private readonly TContext context;

        /// <summary>
        ///     Constructs a new instance of <see cref="PermissionStore{TPermission, TContext, TKey}" />.
        /// </summary>
        /// <param name="context">The <see cref="MongoDbContext" />.</param>
        /// <param name="describer">The <see cref="IdentityErrorDescriber" />.</param>
        public PermissionStore(TContext context, IdentityErrorDescriber describer = null)
			: base(describer)
		{
			Guard.ThrowIfNull(context);

			this.context = context;
        }

		/// <inheritdoc />
		public IQueryable<TPermission> Permissions => this.PermissionsCollection.AsQueryable();

		/// <summary>
		///     The collection of permissions in the database.
		/// </summary>
		public virtual IMongoCollection<TPermission> PermissionsCollection => this.context.GetCollection<TPermission>();

		/// <summary>
		///     The collection of roles in the database.
		/// </summary>
		public virtual IMongoCollection<TRole> RolesCollection => this.context.GetCollection<TRole>();

        /// <inheritdoc />
        public override async Task<IdentityResult> CreateAsync(TPermission permission, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			Guard.ThrowIfNull(permission);

			permission.ConcurrencyStamp = Guid.NewGuid().ToString("N");
			await this.PermissionsCollection.InsertOneAsync(permission, new InsertOneOptions(), cancellationToken);

			return IdentityResult.Success;
        }

		/// <inheritdoc />
		public override async Task<IdentityResult> UpdateAsync(TPermission permission, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			Guard.ThrowIfNull(permission);

			string oldConcurrencyStamp = permission.ConcurrencyStamp;
			permission.ConcurrencyStamp = Guid.NewGuid().ToString("N");

			Expression<Func<TPermission, bool>> predicate = x => x.Id.Equals(permission.Id) && x.ConcurrencyStamp.Equals(oldConcurrencyStamp);
			ReplaceOneResult result = await this.PermissionsCollection.ReplaceOneAsync(predicate, permission, cancellationToken: cancellationToken);

			return result.ModifiedCount == 0
				? IdentityResult.Failed(this.ErrorDescriber.ConcurrencyFailure())
				: IdentityResult.Success;
        }

		/// <inheritdoc />
		public override async Task<IdentityResult> DeleteAsync(TPermission permission, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			Guard.ThrowIfNull(permission);

			string oldConcurrencyStamp = permission.ConcurrencyStamp;
			permission.ConcurrencyStamp = Guid.NewGuid().ToString("N");

			Expression<Func<TPermission, bool>> predicate = x => x.Id.Equals(permission.Id) && x.ConcurrencyStamp.Equals(oldConcurrencyStamp);
			DeleteResult result = await this.PermissionsCollection.DeleteOneAsync(predicate, cancellationToken);

			return result.DeletedCount == 0
				? IdentityResult.Failed(this.ErrorDescriber.ConcurrencyFailure())
				: IdentityResult.Success;
        }

		/// <inheritdoc />
		public override async Task<TPermission> FindByIdAsync(string id, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();

			TKey permissionId = this.ConvertIdFromString(id);
			return await this.PermissionsCollection.Find(x => x.Id.Equals(permissionId)).FirstOrDefaultAsync(cancellationToken);
        }

		/// <inheritdoc />
		public override async Task<TPermission> FindByNameAsync(string normalizedPermissionName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();

			return await this.PermissionsCollection.Find(x => x.NormalizedName == normalizedPermissionName).FirstOrDefaultAsync(cancellationToken);
        }


		/// <inheritdoc />
		public override async Task AddToRoleAsync(TPermission permission, string normalizedRoleName, CancellationToken cancellationToken = default)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			Guard.ThrowIfNull(permission);
			Guard.ThrowIfNullOrWhiteSpace(normalizedRoleName);

			TRole role = await this.FindRoleAsync(normalizedRoleName, cancellationToken);
			if (role is null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.RoleNotFound, normalizedRoleName));
			}

			permission.AddRole(role.Id);
        }

		/// <inheritdoc />
		public override async Task RemoveFromRoleAsync(TPermission permission, string normalizedRoleName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			Guard.ThrowIfNull(permission);
			Guard.ThrowIfNullOrWhiteSpace(normalizedRoleName);

			TRole role = await this.FindRoleAsync(normalizedRoleName, cancellationToken);
			if (role is null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.RoleNotFound, normalizedRoleName));
			}

			permission.RemoveRole(role.Id);
        }

		/// <inheritdoc />
		public override async Task<IList<string>> GetRolesAsync(TPermission permission, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			Guard.ThrowIfNull(permission);

			if (permission.Roles.Any())
			{
				return await this.RolesCollection
					.Find(x => permission.Roles.Contains(x.Id))
					.Project(x => x.Name)
					.ToListAsync(cancellationToken);
			}

			return new List<string>(0);
        }

		/// <inheritdoc />
		public override async Task<IList<string>> GetRoleIdsAsync(TPermission permission, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			Guard.ThrowIfNull(permission);

			if (permission.Roles.Any())
			{
				IList<TKey> keys = await this.RolesCollection
					.Find(x => permission.Roles.Contains(x.Id))
					.Project(x => x.Id)
					.ToListAsync(cancellationToken);

				return keys.Select(this.ConvertIdToString).ToList();
			}

			return new List<string>(0);
        }

		/// <inheritdoc />
        public override async Task<bool> IsInRoleAsync(TPermission permission, string normalizedRoleName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			Guard.ThrowIfNull(permission);
			Guard.ThrowIfNullOrWhiteSpace(normalizedRoleName);

			TRole role = await this.RolesCollection.Find(x => x.NormalizedName == normalizedRoleName).FirstOrDefaultAsync(cancellationToken);
			return role is not null && permission.Roles.Any(x => x.Equals(role.Id));
        }

		/// <inheritdoc />
		public override async Task<IList<TPermission>> GetPermissionsInRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			Guard.ThrowIfNullOrWhiteSpace(normalizedRoleName);

			TRole role = await this.FindRoleAsync(normalizedRoleName, cancellationToken);
			if (role is not null)
			{
				return await this.PermissionsCollection.Find(x => x.Roles.Contains(role.Id)).ToListAsync(cancellationToken);
			}

			return new List<TPermission>(0);
        }

		/// <inheritdoc />
		protected override async Task<TRole> FindRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
		{
			return await this.RolesCollection.Find(x => x.NormalizedName == normalizedRoleName).FirstOrDefaultAsync(cancellationToken);
		}
	}
}
