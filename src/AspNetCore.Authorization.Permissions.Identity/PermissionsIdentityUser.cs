namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Identity
{
	using System;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.Model;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     A default tenant user implementation that used a string as type for the ID.
	/// </summary>
	[PublicAPI]
	public class PermissionsIdentityUser : PermissionsIdentityUser<string>
	{
		/// <summary>
		///     Initializes a new instance of <see cref="PermissionsIdentityUser" />.
		/// </summary>
		/// <remarks>
		///     The Id property is initialized to form a new GUID string value.
		/// </remarks>
		public PermissionsIdentityUser()
		{
			this.Id = Guid.NewGuid().ToString();
			this.SecurityStamp = Guid.NewGuid().ToString();
		}

		/// <summary>
		///     Initializes a new instance of <see cref="PermissionsIdentityUser" />.
		/// </summary>
		/// <param name="userName">The user name.</param>
		/// <remarks>
		///     The Id property is initialized to form a new GUID string value.
		/// </remarks>
		public PermissionsIdentityUser(string userName) : this()
		{
			this.UserName = userName;
		}
	}

	/// <summary>
	///     The default tenant user implementation.
	/// </summary>
	/// <typeparam name="TKey">The type of the ID.</typeparam>
	[PublicAPI]
	public class PermissionsIdentityUser<TKey> : IdentityUser<TKey>, IUser
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Initializes a new instance of <see cref="PermissionsIdentityUser{TKey}" />.
		/// </summary>
		public PermissionsIdentityUser()
		{
		}

		/// <summary>
		///     Initializes a new instance of <see cref="PermissionsIdentityUser{TKey}" />.
		/// </summary>
		/// <param name="userName">The user name.</param>
		public PermissionsIdentityUser(string userName) : this()
		{
			this.UserName = userName;
		}

		/// <summary>
		///     Gets or sets the primary key of the tenant user is linked to.
		/// </summary>
		public virtual TKey TenantId { get; set; }
	}
}
