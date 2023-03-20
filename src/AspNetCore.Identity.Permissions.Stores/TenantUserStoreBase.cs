namespace MadEyeMatt.Extensions.Identity.Permissions
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
	using JetBrains.Annotations;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    ///     Represents a new instance of a persistence store for the specified user and role types.
    /// </summary>
    /// <typeparam name="TUser">The type representing a user.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for a role.</typeparam>
    [PublicAPI]
	public abstract class TenantUserStoreBase<TUser, TKey> : ITenantUserStore<TUser>
		where TUser : IdentityTenantUser<TKey>
		where TKey : IEquatable<TKey>
	{
		private bool disposed;

		/// <summary>
		///     Initializes a new instance of the <see cref="TenantUserStoreBase{TUser,TKey}" /> type.
		/// </summary>
		/// <param name="describer"></param>
		protected TenantUserStoreBase(IdentityErrorDescriber describer = null)
		{
			this.Describer = describer;
		}

		/// <summary>
		///     Gets the describer.
		/// </summary>
		public IdentityErrorDescriber Describer { get; }

		/// <summary>
		///     A navigation property for the users the store contains.
		/// </summary>
		public abstract IQueryable<TUser> Users { get; }

		/// <inheritdoc />
		public virtual Task<string> GetTenantIdAsync(TUser user, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			this.ThrowIfDisposed();
			if(user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			return Task.FromResult(this.ConvertIdToString(user.TenantId));
		}

		/// <summary>
		///     Dispose the store
		/// </summary>
		public void Dispose()
		{
			this.disposed = true;
		}

		/// <summary>
		///     Converts the provided <paramref name="id" /> to a strongly typed key object.
		/// </summary>
		/// <param name="id">The id to convert.</param>
		/// <returns>An instance of <typeparamref name="TKey" /> representing the provided <paramref name="id" />.</returns>
		public virtual TKey ConvertIdFromString(string id)
		{
			if(id == null)
			{
				return default;
			}

			return (TKey)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(id);
		}

		/// <summary>
		///     Converts the provided <paramref name="id" /> to its string representation.
		/// </summary>
		/// <param name="id">The id to convert.</param>
		/// <returns>An <see cref="string" /> representation of the provided <paramref name="id" />.</returns>
		public virtual string ConvertIdToString(TKey id)
		{
			return Equals(id, default(TKey)) ? null : id.ToString();
		}

		/// <summary>
		///     Throws if this class has been disposed.
		/// </summary>
		protected void ThrowIfDisposed()
		{
			if(this.disposed)
			{
				throw new ObjectDisposedException(this.GetType().Name);
			}
		}
	}
}
