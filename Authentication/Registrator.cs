using Authentication.Exceptions;
using Authentication.Interfaces;
using Authentication.Models;
using System.Collections.Generic;
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
            if (_store.FindUser(userName) is not TUser user) throw new InvalidUserException($"Invalid user name: {userName}.");

            if (!_validator.IsValidPassword(newPassword)) throw new InvalidPasswordException("Invalid password!");

            user.Salt = _hasher.GenerateSalt();
            user.HashedPassword = _hasher.Hash(newPassword, user.Salt);
            _store.Update(user);

        }

        /// <inheritdoc/>
        public virtual void Register(TUser user, string password)
        {
            if (!_validator.IsValidUserName(user.UserName)) throw new InvalidUserException($"Invalid user name: {user.UserName}.");

            if (!_validator.IsValidPassword(password)) throw new InvalidPasswordException("Invalid password!");

            user.Salt = _hasher.GenerateSalt();

            user.HashedPassword = _hasher.Hash(password, user.Salt);

            _store.Update(user);

        }

        /// <inheritdoc/>
        public virtual void Unregister(string userName)
        {
            if (_store.FindUser(userName) is null) throw new InvalidUserException($"Invalid user name: {userName}.");

            _store.DeleteUser(userName);

        }

        /// <inheritdoc/>
        public async Task RegisterAsync(TUser user, string password)
        {
            if (_validator.IsValidUserName(user.UserName) is false) throw new InvalidUserException($"Invalid user name: {user.UserName}.");

            if (_validator.IsValidPassword(password) is false) throw new InvalidPasswordException("Invalid password!");

            user.Salt = _hasher.GenerateSalt();

            user.HashedPassword = _hasher.Hash(password, user.Salt);

            await _store.UpdateAsync(user);
        }

        /// <inheritdoc/>
        public async Task ChangePasswordAsync(string userName, string newPassword)
        {
            if (_store.FindUser(userName) is not TUser user) throw new InvalidUserException($"Invalid user name: {userName}.");

            if (_validator.IsValidPassword(newPassword) is false) throw new InvalidPasswordException("Invalid password!");

            user.Salt = _hasher.GenerateSalt();

            user.HashedPassword = _hasher.Hash(newPassword, user.Salt);

            await _store.UpdateAsync(user);
        }

        /// <inheritdoc/>
        public async Task UnregisterAsync(string userName)
        {
            if (_store.FindUser(userName) is null) throw new InvalidUserException($"Invalid user name: {userName}.");

            await _store.DeleteUserAsync(userName);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TUser>> GetUsersAsync()
        {
            return _store.GetUsersAsync();
        }
    }
}
