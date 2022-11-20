﻿namespace MadEyeMatt.AspNetCore.Authorization.Permissions
{
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Http;
    using ClaimsPrincipalExtensions = MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.ClaimsPrincipalExtensions;

    /// <summary>
	///     A tenant accessor that gets the tenant ID from the current user.
	/// </summary>
	[PublicAPI]
	public class HttpContextUserTenantProvider : MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.ITenantProvider
	{
		private readonly IHttpContextAccessor accessor;

		/// <summary>
		///     Creates a new instance of the <see cref="HttpContextUserTenantProvider" /> type.
		/// </summary>
		/// <param name="accessor"></param>
		public HttpContextUserTenantProvider(IHttpContextAccessor accessor)
		{
			this.accessor = accessor;
		}

		/// <inheritdoc />
		public string TenantId
		{
			get
			{
				if(this.accessor.HttpContext != null && this.accessor.HttpContext.User.Identity != null && this.accessor.HttpContext.User.Identity.IsAuthenticated)
				{
					return ClaimsPrincipalExtensions.GetTenantId(this.accessor.HttpContext?.User);
				}

				return null;
			}
		}
	}
}
