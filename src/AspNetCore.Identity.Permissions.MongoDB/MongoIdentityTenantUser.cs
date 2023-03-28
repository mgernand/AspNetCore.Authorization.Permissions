namespace MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB
{
	using System;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.MongoDB;

	/// <summary>
    ///     Represents a tenant user in the identity system.
    /// </summary>
	[PublicAPI]
	public class MongoIdentityTenantUser : MongoIdentityTenantUser<string>
	{
        /// <summary>
        ///     Initializes a new instance of <see cref="MongoIdentityTenantUser" />.
        /// </summary>
        /// <remarks>
        ///     The Id property is initialized to form a new GUID string value.
        /// </remarks>
        public MongoIdentityTenantUser() : this(null)
		{
		}

		/// <summary>
		///     Initializes a new instance of <see cref="IdentityTenantUser" />.
		/// </summary>
		/// <param name="userName">The user name.</param>
		public MongoIdentityTenantUser(string userName)
			: base(userName)
		{
		}
    }

	/// <summary>
	///     Represents a tenant user in the identity system.
	/// </summary>
	[PublicAPI]
	public class MongoIdentityTenantUser<TKey> : MongoIdentityUser<TKey>, ITenantUser<TKey>
        where TKey : IEquatable<TKey>
	{
        /// <summary>
        ///     Initializes a new instance of <see cref="MongoIdentityTenantUser{TKey}" />.
        /// </summary>
        public MongoIdentityTenantUser() : this(null)
		{
		}

        /// <summary>
        ///     Initializes a new instance of <see cref="MongoIdentityTenantUser{TKey}" />.
        /// </summary>
        /// <param name="userName">The user name.</param>
        public MongoIdentityTenantUser(string userName)
			: base(userName)
		{
		}

		/// <summary>
		///     Gets or sets the primary key of the tenant user is linked to.
		/// </summary>
		public virtual TKey TenantId { get; set; }
    }
}
