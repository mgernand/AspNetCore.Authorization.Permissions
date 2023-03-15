namespace MadEyeMatt.AspNetCore.Identity.Permissions.Model
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     A default tenant user implementation that used a string as type for the ID.
	/// </summary>
	[PublicAPI]
	public class PermissionsUser : PermissionsUser<string>
	{
		/// <summary>
		///     Initializes a new instance of <see cref="PermissionsUser" />.
		/// </summary>
		/// <remarks>
		///     The Id property is initialized to form a new GUID string value.
		/// </remarks>
		public PermissionsUser()
		{
			this.Id = Guid.NewGuid().ToString();
			this.SecurityStamp = Guid.NewGuid().ToString();
		}

		/// <summary>
		///     Initializes a new instance of <see cref="PermissionsUser" />.
		/// </summary>
		/// <param name="userName">The user name.</param>
		/// <remarks>
		///     The Id property is initialized to form a new GUID string value.
		/// </remarks>
		public PermissionsUser(string userName) : this()
		{
			this.UserName = userName;
		}
	}

	/// <summary>
	///     The default tenant user implementation.
	/// </summary>
	/// <typeparam name="TKey">The type of the ID.</typeparam>
	[PublicAPI]
	public class PermissionsUser<TKey> : IdentityUser<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Initializes a new instance of <see cref="PermissionsUser{TKey}" />.
		/// </summary>
		public PermissionsUser()
		{
		}

		/// <summary>
		///     Initializes a new instance of <see cref="PermissionsUser{TKey}" />.
		/// </summary>
		/// <param name="userName">The user name.</param>
		public PermissionsUser(string userName) : this()
		{
			this.UserName = userName;
		}

		/// <summary>
		///     Gets or sets the primary key of the tenant user is linked to.
		/// </summary>
		public virtual TKey TenantId { get; set; }
	}
}
