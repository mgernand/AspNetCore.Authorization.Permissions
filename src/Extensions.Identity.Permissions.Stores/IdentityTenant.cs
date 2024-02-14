namespace MadEyeMatt.Extensions.Identity.Permissions.Stores
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     A default tenant implementation that used a string as type for the ID.
	/// </summary>
	[PublicAPI]
	public class IdentityTenant : IdentityTenant<string>
	{
        /// <summary>
        ///     Initializes a new instance of the <see cref="IdentityTenant" /> type.
        /// </summary>
        public IdentityTenant() : this(null)
		{
		}

        /// <summary>
        ///     Initializes a new instance of the <see cref="IdentityTenant" /> type.
        /// </summary>
		/// <param name="tenantName">The tenant name.</param>
        public IdentityTenant(string tenantName) : base(tenantName)
		{
		}
    }

	/// <summary>
	///     The default tenant implementation.
	/// </summary>
	/// <typeparam name="TKey">The type of the ID.</typeparam>
	[PublicAPI]
	public class IdentityTenant<TKey> where TKey : IEquatable<TKey>
	{
        /// <summary>
        ///     Initializes a new instance of the <see cref="IdentityTenant{TKey}" /> type.
        /// </summary>
        public IdentityTenant() : this(null)
        {
		}

        /// <summary>
        ///     Initializes a new instance of the <see cref="IdentityTenant{TKey}" /> type.
        /// </summary>
        /// <param name="tenantName">The tenant name.</param>
        public IdentityTenant(string tenantName)
		{
			this.Name = tenantName;
		}

		/// <summary>
		///     Gets or sets the primary key for this user.
		/// </summary>
		[PersonalData]
		public virtual TKey Id { get; set; }

		/// <summary>
		///     Gets or sets the name of the tenant.
		/// </summary>
		[ProtectedPersonalData]
		public virtual string Name { get; set; }

		/// <summary>
		///     Gets or sets the normalized name of the tenant.
		/// </summary>
		public virtual string NormalizedName { get; set; }

		/// <summary>
		///     Gets or sets the display name of the tenant.
		/// </summary>
		[ProtectedPersonalData]
		public virtual string DisplayName { get; set; }

		/// <summary>
		///     A random value that should change whenever a tenant is persisted to the store
		/// </summary>
		public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString("N");

		/// <summary>
		///     Returns the name of the tenant.
		/// </summary>
		/// <returns>The name of the tenant.</returns>
		public override string ToString()
		{
			return this.Name ?? string.Empty;
		}
	}
}
