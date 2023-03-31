namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
	using System;
	using System.Reflection;
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.AspNetCore.Identity;

	/// <summary>
	///     Extensions for the <see cref="IUserStore{TUser}" /> type.
	/// </summary>
	[PublicAPI]
	public static class UserStoreExtensions
	{
		/// <summary>
		///     Gets the tenant ID for the specified <paramref name="user" />.
		/// </summary>
		/// <param name="store">The user store.</param>
		/// <param name="user">The user whose identifier should be retrieved.</param>
		/// <param name="cancellationToken">
		///     The <see cref="T:System.Threading.CancellationToken" /> used to propagate notifications
		///     that the operation should be canceled.
		/// </param>
		/// <returns>
		///     The <see cref="Task" /> that represents the asynchronous operation, containing the identifier for the
		///     specified <paramref name="user" />.
		/// </returns>
		public static Task<string> GetTenantIdAsync<TUser>(this IUserStore<TUser> store, TUser user, CancellationToken cancellationToken = default)
			where TUser : class
		{
			cancellationToken.ThrowIfCancellationRequested();
			store.ThrowIfDisposed();
			ArgumentNullException.ThrowIfNull(user);

			PropertyInfo propertyInfo = user.GetType().GetProperty("TenantId", BindingFlags.Public | BindingFlags.Instance);
			string id = propertyInfo?.GetValue(user) as string;

			return Task.FromResult(id);
		}

		private static void ThrowIfDisposed<TUser>(this IUserStore<TUser> userStore)
			where TUser : class
		{
			MethodInfo methodInfo = userStore.GetType().GetMethod("ThrowIfDisposed", BindingFlags.NonPublic | BindingFlags.Instance);
			methodInfo?.Invoke(userStore, Array.Empty<object>());
        }
    }
}
