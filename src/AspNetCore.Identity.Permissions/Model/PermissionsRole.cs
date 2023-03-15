namespace MadEyeMatt.AspNetCore.Identity.Permissions.Model
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     A default role implementation that used a string as type for the ID.
	/// </summary>
	[PublicAPI]
	public class PermissionsRole : PermissionsRole<string>
	{
		/// <summary>
		///     Initializes a new instance of <see cref="IdentityRole" />.
		/// </summary>
		/// <remarks>
		///     The Id property is initialized to form a new GUID string value.
		/// </remarks>
		public PermissionsRole()
		{
			this.Id = Guid.NewGuid().ToString();
		}

		/// <summary>
		///     Initializes a new instance of <see cref="IdentityRole" />.
		/// </summary>
		/// <param name="roleName">The role name.</param>
		/// <remarks>
		///     The Id property is initialized to form a new GUID string value.
		/// </remarks>
		public PermissionsRole(string roleName) : this()
		{
			this.Name = roleName;
		}
	}

	/// <summary>
	///     Represents a role in the identity system
	/// </summary>
	/// <typeparam name="TKey">The type used for the primary key for the role.</typeparam>
	[PublicAPI]
	public class PermissionsRole<TKey> : IdentityRole<TKey> where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Initializes a new instance of <see cref="IdentityRole{TKey}" />.
		/// </summary>
		public PermissionsRole()
		{
		}

		/// <summary>
		///     Initializes a new instance of <see cref="IdentityRole{TKey}" />.
		/// </summary>
		/// <param name="roleName">The role name.</param>
		public PermissionsRole(string roleName) : base(roleName)
		{
		}
	}
}
