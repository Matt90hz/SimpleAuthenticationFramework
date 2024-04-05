using Authentication.Models;
using Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication
{
    /// <summary>
    /// Implementation of <see cref="IAuthorizer"/>.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    public class Authorizer<TUser, TRole> : IAuthorizer
        where TUser : IUser
        where TRole : IRole
    {
        private readonly IStore<TUser, TRole> _store;
        private readonly ILogger<TUser> _logger;

        /// <summary>
        /// Creates a new instance of <see cref="Authorizer{TUser, TRole}"/> and initialize the fields.
        /// </summary>
        /// <param name="store"></param>
        /// <param name="logger"></param>
        public Authorizer(IStore<TUser, TRole> store, ILogger<TUser> logger)
        {
            _store = store;
            _logger = logger;
        }

        /// <inheritdoc/>
        public virtual bool IsCurrentUserInRole(params string[] roleKeys)
        {
            return _logger.CurrentUser is TUser user && _store.IsSubscribed(user.UserName, roleKeys);
        }

        /// <inheritdoc/>
        public async Task<bool> IsCurrentUserInRoleAsync(params string[] roleKeys)
        {
            return _logger.CurrentUser is TUser user 
                && await _store.IsSubscribedAsync(user.UserName, roleKeys);
        }
    }
}
