namespace MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.Model
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A default permission implementation that used a string as type for the ID.
	/// </summary>
	[PublicAPI]
	public class PermissionsPermission : PermissionsPermission<string>
	{
	}

	/// <summary>
	///     The default permission implementation.
	/// </summary>
	/// <typeparam name="TKey">The type of the ID.</typeparam>
	[PublicAPI]
	public class PermissionsPermission<TKey> : IPermission
		where TKey : IEquatable<TKey>
	{
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
		///     A random value that should change whenever a permission is persisted to the store
		/// </summary>
		public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

		/// <summary>
		///     Returns the name of the permission.
		/// </summary>
		/// <returns>The name of the permission.</returns>
		public override string ToString()
		{
			return this.Name;
		}
	}
}
