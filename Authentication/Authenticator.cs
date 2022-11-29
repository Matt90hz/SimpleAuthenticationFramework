using Authentication.Models;
using Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    /// <summary>
    /// Class that implements <see cref="IAuthenticator"/> for the given <typeparamref name="TUser"/>, <typeparamref name="TRole"/>.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    public class Authenticator<TUser, TRole> : IAuthenticator
        where TUser : IUser
        where TRole : IRole
    {
        private readonly IStore<TUser, TRole> _store;
        private readonly IHasher _hasher;

        /// <summary>
        /// Creates a new instance of <see cref="Authenticator{TUser, TRole}"/> and initilize the fields.
        /// </summary>
        /// <param name="store"></param>
        /// <param name="hasher"></param>
        public Authenticator(IStore<TUser, TRole> store, IHasher hasher)
        {
            _store = store;
            _hasher = hasher;
        }

        /// <inheritdoc/>
        public virtual bool Authenticate(string userName, string password)
        {
            return _store.FindUser(userName) is IUser user && _hasher.Check(user.HashedPassword, password, user.Salt);
        }
    }
}
