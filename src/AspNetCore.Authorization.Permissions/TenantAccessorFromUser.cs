namespace AspNetCore.Authorization.Permissions
{
	using AspNetCore.Authorization.Permissions.Abstractions;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
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
		public string TenantId
		{
			get
			{
				if(this.accessor.HttpContext != null && this.accessor.HttpContext.User.IsAuthenticated())
				{
					return this.accessor.HttpContext?.User.GetTenantId();
				}

				return null;
			}
		}
	}
}
