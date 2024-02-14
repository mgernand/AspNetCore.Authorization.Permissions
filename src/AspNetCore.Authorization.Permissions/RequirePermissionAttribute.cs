namespace MadEyeMatt.AspNetCore.Authorization.Permissions
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authorization;

	/// <summary>
	///     This attribute can be applied just like the [Authorize] attribute.
	///     This will only allow users which have a role containing the given permission.
	/// </summary>
	[PublicAPI]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
	public class RequirePermissionAttribute : Attribute, IAuthorizeData
	{
		private string policy;

		/// <summary>
		///     Initializes a new instance of the <see cref="RequirePermissionAttribute" /> type.
		/// </summary>
		/// <param name="permission"></param>
		public RequirePermissionAttribute(string permission)
		{
			if (string.IsNullOrWhiteSpace(permission))
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