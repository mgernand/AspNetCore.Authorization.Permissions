namespace MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;

	/// <summary>
	///     A default permission implementation that used a string as type for the ID.
	/// </summary>
	[PublicAPI]
    public class MongoIdentityPermission : MongoIdentityPermission<string>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MongoIdentityPermission" /> type.
        /// </summary>
        public MongoIdentityPermission() : this(null)
		{
		}

        /// <summary>
        ///     Initializes a new instance of the <see cref="MongoIdentityPermission" /> type.
        /// </summary>
        /// <param name="permissionName">The permission name.</param>
        public MongoIdentityPermission(string permissionName)
			: base(permissionName)
		{
		}
    }

	/// <summary>
	///     The default permission implementation.
	/// </summary>
	/// <typeparam name="TKey">The type of the ID.</typeparam>
	[PublicAPI]
	public class MongoIdentityPermission<TKey> : IdentityPermission<TKey> 
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="MongoIdentityPermission" /> type.
		/// </summary>
		public MongoIdentityPermission() : this(null)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="MongoIdentityPermission" /> type.
		/// </summary>
		/// <param name="permissionName">The permission name.</param>
		public MongoIdentityPermission(string permissionName)
			: base(permissionName)
		{
			this.Roles = new List<TKey>();
		}

        /// <summary>
		///		The roles associated to the permission.
        /// </summary>
        public IList<TKey> Roles { get; set; }

		/// <summary>
		///     Adds a role to the permission.
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
        ///     Removes a role from the permission.
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
