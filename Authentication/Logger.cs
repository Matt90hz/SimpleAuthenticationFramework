using Authentication.Models;
using Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace Authentication
{
    /// <summary>
    /// Implementation of <see cref="ILogger{TUser}"/>.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public class Logger<TUser, TRole> : ILogger<TUser>
        where TUser : class, IUser
        where TRole : class, IRole
    {
        private readonly IStore<TUser, TRole> _store;
        protected readonly IAuthenticator _authenticator;

        /// <summary>
        /// Creates a new instance of <see cref="Logger{TUser, TRole}"/> and initialize the fields.
        /// </summary>
        /// <param name="authenticator"></param>
        public Logger(IStore<TUser, TRole> store, IAuthenticator authenticator)
        {
            _store = store;
            _authenticator = authenticator;
        }

        /// <inheritdoc/>
        public virtual TUser? CurrentUser { get; protected set; }

        /// <inheritdoc/>
        public virtual bool Login(string userName, string password)
        {
            if(!_authenticator.Authenticate(userName, password)) return false;
                
            CurrentUser = _store.FindUser(userName);

            return true;
        }

        /// <inheritdoc/>
        public virtual void Logout()
        {
            CurrentUser = null;
        }

    }
}
