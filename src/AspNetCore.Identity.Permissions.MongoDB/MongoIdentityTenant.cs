namespace MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;
	using MadEyeMatt.Extensions.Identity.Permissions.Stores;

	/// <summary>
    ///     Represents a tenant in the identity system.
    /// </summary>
    [PublicAPI]
    public class MongoIdentityTenant : MongoIdentityTenant<string>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="MongoIdentityTenant" /> type.
		/// </summary>
		public MongoIdentityTenant() : this(null)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="MongoIdentityTenant" /> type.
		/// </summary>
		/// <param name="tenantName">The user name.</param>
		public MongoIdentityTenant(string tenantName)
			: base(tenantName)
		{
		}
    }

	/// <summary>
	///     Represents a tenant in the identity system.
	/// </summary>
	[PublicAPI]
    public class MongoIdentityTenant<TKey> : IdentityTenant<TKey> 
		where TKey : IEquatable<TKey>
	{
        /// <summary>
        ///     Initializes a new instance of the <see cref="MongoIdentityTenant{TKey}" /> type.
        /// </summary>
        public MongoIdentityTenant() : this(null)
		{
		}

        /// <summary>
        ///     Initializes a new instance of the <see cref="MongoIdentityTenant{TKey}" /> type.
        /// </summary>
        /// <param name="tenantName">The user name.</param>
        public MongoIdentityTenant(string tenantName) 
			: base(tenantName)
		{
			this.Roles = new List<TKey>();
		}

		/// <summary>
		///     The IDs of the roles of the user.
		/// </summary>
		public IList<TKey> Roles { get; set; }

		/// <summary>
		///     Adds a role to the tenant.
		/// </summary>
		/// <param name="roleId">The id of the role add.</param>
		/// <returns>Returns <c>true</c> if the role was successfully added.</returns>
		public bool AddRole(TKey roleId)
		{
			ArgumentNullException.ThrowIfNull(roleId);
			if (roleId.Equals(default))
			{
				throw new ArgumentNullException(nameof(roleId));
			}

			// Prevent adding duplicate roles.
			if (this.Roles.Contains(roleId))
			{
				return false;
			}

			this.Roles.Add(roleId);
			return true;
		}

        /// <summary>
        ///     Removes a role from the tenant.
        /// </summary>
        /// <param name="roleId">The id of the role to remove.</param>
        /// <returns>Returns <c>true</c> if the role was successfully removed.</returns>
		public bool RemoveRole(TKey roleId)
		{
			ArgumentNullException.ThrowIfNull(roleId);
			if (roleId.Equals(default))
			{
				throw new ArgumentNullException(nameof(roleId));
			}

			TKey id = this.Roles.FirstOrDefault(e => e.Equals(roleId));
			if (id == null || id.Equals(default))
			{
				return false;
			}

			this.Roles.Remove(roleId);
			return true;
		}
    }
}
