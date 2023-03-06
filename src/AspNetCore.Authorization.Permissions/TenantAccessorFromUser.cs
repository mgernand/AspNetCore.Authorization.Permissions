namespace MadEyeMatt.AspNetCore.Authorization.Permissions
{
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions;
	using Microsoft.AspNetCore.Http;

	/// <summary>
	///     A tenant accessor that gets the tenant ID from the current user.
	/// </summary>
	[PublicAPI]
	public class HttpContextUserTenantProvider : ITenantProvider
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
		public string TenantID
		{
			get
			{
				if(this.accessor.HttpContext?.User.Identity != null && this.accessor.HttpContext.User.Identity.IsAuthenticated)
				{
					return (this.accessor.HttpContext?.User).GetTenantId();
				}

				return null;
			}
		}
	}
}
