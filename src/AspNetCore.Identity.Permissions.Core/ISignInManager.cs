namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Authentication;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Provides an abstraction for user sign in.
	/// </summary>
	/// <remarks>
	///     Interface extracted from <see cref="SignInManager{TUser}" />.
	/// </remarks>
	/// <typeparam name="TUser">The type encapsulating a user.</typeparam>
	[PublicAPI]
	public interface ISignInManager<TUser>
		where TUser : class
	{
		/// <summary>
		///     Returns true if the principal has an identity with the application cookie identity
		/// </summary>
		/// <param name="principal">The <see cref="T:System.Security.Claims.ClaimsPrincipal" /> instance.</param>
		/// <returns>True if the user is logged in with identity.</returns>
		bool IsSignedIn(ClaimsPrincipal principal);

		/// <summary>
		///     Returns a flag indicating whether the specified user can sign in.
		/// </summary>
		/// <param name="user">The user whose sign-in status should be returned.</param>
		/// <returns>
		///     The task object representing the asynchronous operation, containing a flag that is true
		///     if the specified user can sign-in, otherwise false.
		/// </returns>
		Task<bool> CanSignInAsync(TUser user);

		/// <summary>
		///     Signs in the specified <paramref name="user" />, whilst preserving the existing
		///     AuthenticationProperties of the current signed-in user like rememberMe, as an asynchronous operation.
		/// </summary>
		/// <param name="user">The user to sign-in.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		Task RefreshSignInAsync(TUser user);

		/// <summary>
		///     Signs in the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user to sign-in.</param>
		/// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
		/// <param name="authenticationMethod">Name of the method used to authenticate the user.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		Task SignInAsync(TUser user, bool isPersistent, string authenticationMethod = null);

		/// <summary>
		///     Signs in the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user to sign-in.</param>
		/// <param name="authenticationProperties">Properties applied to the login and authentication cookie.</param>
		/// <param name="authenticationMethod">Name of the method used to authenticate the user.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		Task SignInAsync(TUser user, AuthenticationProperties authenticationProperties, string authenticationMethod = null);

		/// <summary>
		///     Signs in the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user to sign-in.</param>
		/// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
		/// <param name="additionalClaims">Additional claims that will be stored in the cookie.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		Task SignInWithClaimsAsync(TUser user, bool isPersistent, IEnumerable<Claim> additionalClaims);

		/// <summary>
		///     Signs in the specified <paramref name="user" />.
		/// </summary>
		/// <param name="user">The user to sign-in.</param>
		/// <param name="authenticationProperties">Properties applied to the login and authentication cookie.</param>
		/// <param name="additionalClaims">Additional claims that will be stored in the cookie.</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		Task SignInWithClaimsAsync(TUser user, AuthenticationProperties authenticationProperties, IEnumerable<Claim> additionalClaims);

		/// <summary>
		///     Signs the current user out of the application.
		/// </summary>
		Task SignOutAsync();

		/// <summary>
		///     Validates the security stamp for the specified <paramref name="principal" /> against
		///     the persisted stamp for the current user, as an asynchronous operation.
		/// </summary>
		/// <param name="principal">The principal whose stamp should be validated.</param>
		/// <returns>
		///     The task object representing the asynchronous operation. The task will contain the <typeparamref name="TUser" />
		///     if the stamp matches the persisted value, otherwise it will return null.
		/// </returns>
		Task<TUser> ValidateSecurityStampAsync(ClaimsPrincipal principal);

		/// <summary>
		///     Validates the security stamp for the specified <paramref name="principal" /> from one of
		///     the two factor principals (remember client or user id) against
		///     the persisted stamp for the current user, as an asynchronous operation.
		/// </summary>
		/// <param name="principal">The principal whose stamp should be validated.</param>
		/// <returns>
		///     The task object representing the asynchronous operation. The task will contain the <typeparamref name="TUser" />
		///     if the stamp matches the persisted value, otherwise it will return null.
		/// </returns>
		Task<TUser> ValidateTwoFactorSecurityStampAsync(ClaimsPrincipal principal);

		/// <summary>
		///     Validates the security stamp for the specified <paramref name="user" />.  If no user is specified, or if the store
		///     does not support security stamps, validation is considered successful.
		/// </summary>
		/// <param name="user">The user whose stamp should be validated.</param>
		/// <param name="securityStamp">The expected security stamp value.</param>
		/// <returns>The result of the validation.</returns>
		Task<bool> ValidateSecurityStampAsync(TUser user, string securityStamp);

		/// <summary>
		///     Attempts to sign in the specified <paramref name="user" /> and <paramref name="password" /> combination
		///     as an asynchronous operation.
		/// </summary>
		/// <param name="user">The user to sign in.</param>
		/// <param name="password">The password to attempt to sign in with.</param>
		/// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
		/// <param name="lockoutOnFailure">Flag indicating if the user account should be locked if the sign in fails.</param>
		/// <returns>
		///     The task object representing the asynchronous operation containing the <see name="SignInResult" />
		///     for the sign-in attempt.
		/// </returns>
		Task<SignInResult> PasswordSignInAsync(TUser user, string password, bool isPersistent, bool lockoutOnFailure);

		/// <summary>
		///     Attempts to sign in the specified <paramref name="userName" /> and <paramref name="password" /> combination
		///     as an asynchronous operation.
		/// </summary>
		/// <param name="userName">The user name to sign in.</param>
		/// <param name="password">The password to attempt to sign in with.</param>
		/// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
		/// <param name="lockoutOnFailure">Flag indicating if the user account should be locked if the sign in fails.</param>
		/// <returns>
		///     The task object representing the asynchronous operation containing the <see name="SignInResult" />
		///     for the sign-in attempt.
		/// </returns>
		Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);

		/// <summary>Attempts a password sign in for a user.</summary>
		/// <param name="user">The user to sign in.</param>
		/// <param name="password">The password to attempt to sign in with.</param>
		/// <param name="lockoutOnFailure">Flag indicating if the user account should be locked if the sign in fails.</param>
		/// <returns>
		///     The task object representing the asynchronous operation containing the <see name="SignInResult" />
		///     for the sign-in attempt.
		/// </returns>
		/// <returns></returns>
		Task<SignInResult> CheckPasswordSignInAsync(TUser user, string password, bool lockoutOnFailure);

		/// <summary>
		///     Returns a flag indicating if the current client browser has been remembered by two factor authentication
		///     for the user attempting to login, as an asynchronous operation.
		/// </summary>
		/// <param name="user">The user attempting to login.</param>
		/// <returns>
		///     The task object representing the asynchronous operation containing true if the browser has been remembered
		///     for the current user.
		/// </returns>
		Task<bool> IsTwoFactorClientRememberedAsync(TUser user);

		/// <summary>
		///     Sets a flag on the browser to indicate the user has selected "Remember this browser" for two factor authentication
		///     purposes,
		///     as an asynchronous operation.
		/// </summary>
		/// <param name="user">The user who choose "remember this browser".</param>
		/// <returns>The task object representing the asynchronous operation.</returns>
		Task RememberTwoFactorClientAsync(TUser user);

		/// <summary>
		///     Clears the "Remember this browser flag" from the current browser, as an asynchronous operation.
		/// </summary>
		/// <returns>The task object representing the asynchronous operation.</returns>
		Task ForgetTwoFactorClientAsync();

		/// <summary>
		///     Signs in the user without two factor authentication using a two factor recovery code.
		/// </summary>
		/// <param name="recoveryCode">The two factor recovery code.</param>
		/// <returns></returns>
		Task<SignInResult> TwoFactorRecoveryCodeSignInAsync(string recoveryCode);

		/// <summary>
		///     Validates the sign in code from an authenticator app and creates and signs in the user, as an asynchronous
		///     operation.
		/// </summary>
		/// <param name="code">The two factor authentication code to validate.</param>
		/// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
		/// <param name="rememberClient">
		///     Flag indicating whether the current browser should be remember, suppressing all further
		///     two factor authentication prompts.
		/// </param>
		/// <returns>
		///     The task object representing the asynchronous operation containing the <see name="SignInResult" />
		///     for the sign-in attempt.
		/// </returns>
		Task<SignInResult> TwoFactorAuthenticatorSignInAsync(string code, bool isPersistent, bool rememberClient);

		/// <summary>
		///     Validates the two factor sign in code and creates and signs in the user, as an asynchronous operation.
		/// </summary>
		/// <param name="provider">The two factor authentication provider to validate the code against.</param>
		/// <param name="code">The two factor authentication code to validate.</param>
		/// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
		/// <param name="rememberClient">
		///     Flag indicating whether the current browser should be remember, suppressing all further
		///     two factor authentication prompts.
		/// </param>
		/// <returns>
		///     The task object representing the asynchronous operation containing the <see name="SignInResult" />
		///     for the sign-in attempt.
		/// </returns>
		Task<SignInResult> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberClient);

		/// <summary>
		///     Gets the <typeparamref name="TUser" /> for the current two factor authentication login, as an asynchronous
		///     operation.
		/// </summary>
		/// <returns>
		///     The task object representing the asynchronous operation containing the <typeparamref name="TUser" />
		///     for the sign-in attempt.
		/// </returns>
		Task<TUser> GetTwoFactorAuthenticationUserAsync();

		/// <summary>
		///     Signs in a user via a previously registered third party login, as an asynchronous operation.
		/// </summary>
		/// <param name="loginProvider">The login provider to use.</param>
		/// <param name="providerKey">The unique provider identifier for the user.</param>
		/// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
		/// <returns>
		///     The task object representing the asynchronous operation containing the <see name="SignInResult" />
		///     for the sign-in attempt.
		/// </returns>
		Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent);

		/// <summary>
		///     Signs in a user via a previously registered third party login, as an asynchronous operation.
		/// </summary>
		/// <param name="loginProvider">The login provider to use.</param>
		/// <param name="providerKey">The unique provider identifier for the user.</param>
		/// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
		/// <param name="bypassTwoFactor">Flag indicating whether to bypass two factor authentication.</param>
		/// <returns>
		///     The task object representing the asynchronous operation containing the <see name="SignInResult" />
		///     for the sign-in attempt.
		/// </returns>
		Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor);

		/// <summary>
		///     Gets a collection of <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticationScheme" />s for the known
		///     external login providers.
		/// </summary>
		/// <returns>
		///     A collection of <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticationScheme" />s for the known
		///     external login providers.
		/// </returns>
		Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();

		/// <summary>
		///     Gets the external login information for the current login, as an asynchronous operation.
		/// </summary>
		/// <param name="expectedXsrf">
		///     Flag indication whether a Cross Site Request Forgery token was expected in the current
		///     request.
		/// </param>
		/// <returns>
		///     The task object representing the asynchronous operation containing the <see name="ExternalLoginInfo" />
		///     for the sign-in attempt.
		/// </returns>
		Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf = null);

		/// <summary>
		///     Stores any authentication tokens found in the external authentication cookie into the associated user.
		/// </summary>
		/// <param name="externalLogin">The information from the external login provider.</param>
		/// <returns>
		///     The <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation, containing the
		///     <see cref="T:Microsoft.AspNetCore.Identity.IdentityResult" /> of the operation.
		/// </returns>
		Task<IdentityResult> UpdateExternalAuthenticationTokensAsync(ExternalLoginInfo externalLogin);

		/// <summary>
		///     Configures the redirect URL and user identifier for the specified external login <paramref name="provider" />.
		/// </summary>
		/// <param name="provider">The provider to configure.</param>
		/// <param name="redirectUrl">The external login URL users should be redirected to during the login flow.</param>
		/// <param name="userId">The current user's identifier, which will be used to provide CSRF protection.</param>
		/// <returns>A configured <see cref="T:Microsoft.AspNetCore.Authentication.AuthenticationProperties" />.</returns>
		AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId = null);
	}
}
