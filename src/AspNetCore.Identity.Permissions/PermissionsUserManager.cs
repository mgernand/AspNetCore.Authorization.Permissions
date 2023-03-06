namespace MadEyeMatt.AspNetCore.Identity.Permissions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using MadEyeMatt.AspNetCore.Identity.Permissions.Model;
    using MadEyeMatt.AspNetCore.Identity.Permissions.Stores;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    /// <inheritdoc />
    [PublicAPI]
    public class PermissionsUserManager<TUser> : UserManager<TUser>
        where TUser : class, IUser
    {
        private readonly IPermissionsUserStore<TUser> permissionsUserStore;

        /// <inheritdoc />
        public PermissionsUserManager(
            IPermissionsUserStore<TUser> permissionsUserStore,
            IUserStore<TUser> userStore,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<TUser> passwordHasher,
            IEnumerable<IUserValidator<TUser>> userValidators,
            IEnumerable<IPasswordValidator<TUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<PermissionsUserManager<TUser>> logger)
            : base(userStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.permissionsUserStore = permissionsUserStore;
        }

        /// <summary>
        ///     Gets the tenant name for the specified <paramref name="user" />.
        /// </summary>
        /// <param name="user">The user whose tenant name should be retrieved.</param>
        /// <returns>
        ///     The <see cref="Task" /> that represents the asynchronous operation, containing the name for the specified
        ///     <paramref name="user" />.
        /// </returns>
        public virtual async Task<string> GetTenantIdAsync(TUser user)
        {
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await permissionsUserStore.GetTenantIdAsync(user, CancellationToken);
        }
    }
}
