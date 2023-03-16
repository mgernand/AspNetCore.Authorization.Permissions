namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authentication;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     Provides the APIs for user sign in.
	/// </summary>
	/// <typeparam name="TUser">The type encapsulating a user.</typeparam>
	[PublicAPI]
	public class CustomSignInManager<TUser> : SignInManager<TUser>, ISignInManager<TUser>
		where TUser : class
	{
		/// <inheritdoc />
		public CustomSignInManager(
			UserManager<TUser> userManager,
			IHttpContextAccessor contextAccessor,
			IUserClaimsPrincipalFactory<TUser> claimsFactory,
			IOptions<IdentityOptions> optionsAccessor,
			ILogger<SignInManager<TUser>> logger,
			IAuthenticationSchemeProvider schemes,
			IUserConfirmation<TUser> confirmation)
			: base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
		{
		}
	}
}
