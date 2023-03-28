namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     A default tenant user implementation that used a string as type for the ID.
	/// </summary>
	[PublicAPI]
	public class IdentityTenantUser : IdentityTenantUser<string>
	{
		/// <summary>
		///     Initializes a new instance of <see cref="IdentityTenantUser" />.
		/// </summary>
		public IdentityTenantUser() : this(null)
		{
		}

		/// <summary>
		///     Initializes a new instance of <see cref="IdentityTenantUser" />.
		/// </summary>
		/// <param name="userName">The user name.</param>
		public IdentityTenantUser(string userName) 
			: base(userName)
		{
		}
	}

	/// <summary>
	///     The default tenant user implementation.
	/// </summary>
	/// <typeparam name="TKey">The type of the ID.</typeparam>
	[PublicAPI]
	public class IdentityTenantUser<TKey> : IdentityUser<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Initializes a new instance of <see cref="IdentityTenantUser{TKey}" />.
		/// </summary>
		public IdentityTenantUser() : this(null)
		{
		}

		/// <summary>
		///     Initializes a new instance of <see cref="IdentityTenantUser{TKey}" />.
		/// </summary>
		/// <param name="userName">The user name.</param>
		public IdentityTenantUser(string userName) 
			: base(userName)
		{
		}

		/// <summary>
		///     Gets or sets the primary key of the tenant user is linked to.
		/// </summary>
		public virtual TKey TenantId { get; set; }
	}
}
