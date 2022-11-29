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
    /// Implementation of <see cref="IRegistrator{TUser}"/>.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    public class Registrator<TUser, TRole> : IRegistrator<TUser>
        where TUser : class, IUser
        where TRole : class, IRole
    {
        private readonly IStore<TUser, TRole> _store;
        private readonly IHasher _hasher;
        private readonly IValidator _validator;

        /// <inheritdoc/>
        public virtual IEnumerable<TUser> Users => _store.GetAllUsers();

        /// <summary>
        /// Creates a new instance of <see cref="Registrator{TUser, TRole}"/> and initialize the fields.
        /// </summary>
        /// <param name="store"></param>
        /// <param name="hasher"></param>
        /// <param name="validator"></param>
        public Registrator(IStore<TUser, TRole> store, IHasher hasher, IValidator validator)
        {
            _store = store;
            _hasher = hasher;
            _validator = validator;
        }

        /// <inheritdoc/>
        public virtual void ChangePassword(string userName, string newPassword)
        {
            if (_store.FindUser(userName) is not TUser user) throw new Exception("User not found.");

            if (!_validator.IsValidPassword(newPassword)) throw new Exception("Invalid password");
            
            user.Salt = _hasher.GenerateSalt();
            user.HashedPassword = _hasher.Hash(newPassword, user.Salt);
            _store.Update(user);

        }

        /// <inheritdoc/>
        public virtual void Register(TUser user, string password)
        {
            if (!_validator.IsValidUserName(user.UserName)) throw new Exception("Invalid user name.");

            if (!_validator.IsValidPassword(password)) throw new Exception("Invalid password");

            user.Salt = _hasher.GenerateSalt();

            user.HashedPassword = _hasher.Hash(password, user.Salt);

            _store.Update(user);

        }

        /// <inheritdoc/>
        public virtual void Unregister(string userName)
        {
            if (_store.FindUser(userName) is null) throw new Exception("User not found.");

            _store.DeleteUser(userName);

        }

    }
}
