using Authentication.Models;
using Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Exceptions;

namespace Authentication
{
    /// <summary>
    /// Implementation of <see cref="ISubscriber{TRole}"/>.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    /// <remarks>
    /// There is no methods to create the roles, they must be manually creted and saved in the store. 
    /// See <seealso cref="ISubscriber{TRole}"/> for a more detailed explanation.
    /// </remarks>
    public class Subscriber<TUser, TRole> : ISubscriber<TRole>
        where TRole : IRole
        where TUser : IUser
    {
        private readonly IStore<TUser, TRole> _store;

        /// <summary>
        /// Creates a new instance of <see cref="Subscriber{TUser, TRole}"/> and initialize the fields.
        /// </summary>
        /// <param name="store"></param>
        public Subscriber(IStore<TUser, TRole> store)
        {
            _store = store;
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TRole> Roles => _store.GetAllRoles();
        
        /// <inheritdoc/>
        public Task<IEnumerable<TRole>> GetRolesAsync()
        {
            return _store.GetRolesAsync();
        }

        /// <inheritdoc/>
        public virtual IEnumerable<TRole> GetUserRoles(string userName)
        {
            foreach (var role in Roles)
            {
                if (_store.IsSubscribed(userName, role.RoleKey)) yield return role;
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TRole>> GetUserRolesAsync(string userName)
        {
            var roles = await _store.GetRolesAsync();

            List<TRole> rolesList = new();

            foreach (var role in roles)
            {
                if (await _store.IsSubscribedAsync(userName, role.RoleKey)) rolesList.Add(role);
            }

            return rolesList;
        }

        /// <inheritdoc/>
        public virtual bool IsSubscribed(string userName, params string[] roleKeys)
        {
            return _store.IsSubscribed(userName, roleKeys);
        }

        /// <inheritdoc/>
        public Task<bool> IsSubscribedAsync(string userName, params string[] roleKeys)
        {
            return _store.IsSubscribedAsync(userName, roleKeys);
        }

        /// <inheritdoc/>
        public virtual void Subscribe(string roleKey, string userName)
        {
            if (_store.FindRole(roleKey) is null) throw new InvalidRoleException($"Role {roleKey} not found.");

            if (_store.FindUser(userName) is null) throw new InvalidUserException($"User {userName} not found.");

            _store.Join(userName, roleKey);

        }

        /// <inheritdoc/>
        public async Task SubscribeAsync(string roleKey, string userName)
        {
            if (_store.FindRole(roleKey) is null) throw new InvalidRoleException($"Role {roleKey} not found.");

            if (_store.FindUser(userName) is null) throw new InvalidUserException($"User {userName} not found.");

            await _store.JoinAsync(userName, roleKey);
        }

        /// <inheritdoc/>
        public virtual void Unsubscribe(string roleKey, string userName)
        {
            if (_store.FindRole(roleKey) is null) throw new InvalidRoleException($"Role {roleKey} not found.");

            if (_store.FindUser(userName) is null) throw new InvalidUserException($"User {userName} not found.");

            if (_store.IsSubscribed(userName, roleKey) is false) return;

            _store.Detach(userName, roleKey);
        }

        /// <inheritdoc/>
        public async Task UnsubscribeAsync(string roleKey, string userName)
        {
            if (_store.FindRole(roleKey) is null) throw new InvalidRoleException($"Role {roleKey} not found.");

            if (_store.FindUser(userName) is null) throw new InvalidUserException($"User {userName} not found.");

            if (_store.IsSubscribed(userName, roleKey) is false) return;

            await _store.DetachAsync(userName, roleKey);
        }
    }
}
