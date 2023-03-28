namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A default permission implementation that used a string as type for the ID.
	/// </summary>
	[PublicAPI]
	public class IdentityPermission : IdentityPermission<string>
	{
        /// <summary>
        ///     Initializes a new instance of the <see cref="IdentityPermission" /> type.
        /// </summary>
        public IdentityPermission() : this(null)
		{
		}

        /// <summary>
        ///     Initializes a new instance of the <see cref="IdentityPermission" /> type.
        /// </summary>
        /// <param name="permissionName">The permission name.</param>
        public IdentityPermission(string permissionName) 
			: base(permissionName)
		{
		}
	}

	/// <summary>
	///     The default permission implementation.
	/// </summary>
	/// <typeparam name="TKey">The type of the ID.</typeparam>
	[PublicAPI]
	public class IdentityPermission<TKey> where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="IdentityPermission{TKey}" /> type.
		/// </summary>
        public IdentityPermission() : this(null)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="IdentityPermission{TKey}" /> type.
		/// </summary>
		/// <param name="permissionName">The permission name.</param>
        public IdentityPermission(string permissionName)
		{
			this.Name = permissionName;
		}

        /// <summary>
        ///     Gets or sets the primary key for this user.
        /// </summary>
        public virtual TKey Id { get; set; }

		/// <summary>
		///     Gets or sets the name of the permission.
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		///     Gets or sets the normalized name of the permission.
		/// </summary>
		public virtual string NormalizedName { get; set; }

		/// <summary>
		///     Gets or sets the display name of the permission.
		/// </summary>
		public virtual string DisplayName { get; set; }

        /// <summary>
        ///     A random value that should change whenever a permission is persisted to the store
        /// </summary>
        public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString("N");

		/// <summary>
		///     Returns the name of the permission.
		/// </summary>
		/// <returns>The name of the permission.</returns>
		public override string ToString()
		{
			return this.Name ?? string.Empty;
		}
	}
}
