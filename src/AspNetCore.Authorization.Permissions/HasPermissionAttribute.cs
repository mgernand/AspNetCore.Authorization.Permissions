namespace AspNetCore.Authorization.Permissions
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authorization;

	/// <summary>
	///     This attribute can be applied just like the [Authorize].
	///     This will only allow users which have a role containing the given permission.
	/// </summary>
	[PublicAPI]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
	public sealed class HasPermissionAttribute : Attribute, IAuthorizeData
	{
		private string policy;

		/// <summary>
		///     Creates a new instance of the <see cref="HasPermissionAttribute" /> type.
		/// </summary>
		/// <param name="permission"></param>
		public HasPermissionAttribute(string permission)
		{
			if(string.IsNullOrWhiteSpace(permission))
			{
				throw new ArgumentNullException(nameof(permission));
			}

			this.policy = permission;
		}

		/// <inheritdoc />
		string IAuthorizeData.Policy
		{
			get => this.policy;
			set => this.policy = value;
		}

		/// <inheritdoc />
		string IAuthorizeData.Roles { get; set; }

		/// <inheritdoc />
		public string AuthenticationSchemes { get; set; }
	}
}
