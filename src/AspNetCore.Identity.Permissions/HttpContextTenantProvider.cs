namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Http;

	/// <summary>
	///     A tenant accessor that gets the tenant ID from the current user.
	/// </summary>
	[PublicAPI]
	public class HttpContextTenantProvider : ITenantProvider
	{
		private readonly IHttpContextAccessor httpContextAccessor;

		/// <summary>
		///     Initializes a new instance of the <see cref="HttpContextTenantProvider" /> type.
		/// </summary>
		/// <param name="httpContextAccessor"></param>
		public HttpContextTenantProvider(IHttpContextAccessor httpContextAccessor)
		{
			this.httpContextAccessor = httpContextAccessor;
		}

		/// <inheritdoc />
		public string TenantID
		{
			get
			{
				if(this.httpContextAccessor.HttpContext?.User.Identity != null && this.httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
				{
					return (this.httpContextAccessor.HttpContext?.User).GetTenantId();
				}

				return null;
			}
		}
	}
}
