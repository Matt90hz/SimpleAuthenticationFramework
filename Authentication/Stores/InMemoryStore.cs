using Authentication.Models;
using Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Stores
{
    /// <summary>
    /// Implementation of <see cref="IStore{TUser, TRole}"/> that use an in memory collection as repository.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    public class InMemoryStore<TUser, TRole> : IStore<TUser, TRole>
        where TUser : IUser
        where TRole : IRole
    {
        private readonly List<TUser> _users = new();
        private readonly List<TRole> _roles = new();
        private readonly List<Subscription> _subscriptions = new();

        /// <summary>
        /// Class that rappresent the relation between <typeparamref name="TUser"/> and <typeparamref name="TRole"/>.
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="RoleKey"></param>
        private record Subscription(string UserName, string RoleKey);

        /// <inheritdoc/>
        public void DeleteRole(string roleKey)
        {
            if(_roles.Find(r => r.RoleKey == roleKey) is not TRole role) return;

            _roles.Remove(role);
            
            foreach(var subscription in _subscriptions)
            {
                if (subscription.RoleKey == roleKey) _subscriptions.Remove(subscription);
            }

        }

        /// <inheritdoc/>
        public void DeleteUser(string userName)
        {
            if (_users.Find(r => r.UserName == userName) is not TUser user) return;

            _users.Remove(user);

            foreach (var subscription in _subscriptions)
            {
                if (subscription.UserName == userName) _subscriptions.Remove(subscription);
            }
        }

        /// <inheritdoc/>
        public void Detach(string userName, string roleKey)
        {
            if (_subscriptions.Find(r => r.UserName == userName && r.RoleKey == roleKey) is not Subscription subscription) return;

            _subscriptions.Remove(subscription);
        }

        /// <inheritdoc/>
        public TRole? FindRole(string roleKey)
        {
            return _roles.Find(r => r.RoleKey == roleKey);
        }

        /// <inheritdoc/>
        public TUser? FindUser(string userName)
        {
            return _users.Find(r => r.UserName == userName);
        }

        /// <inheritdoc/>
        public IEnumerable<TRole> GetAllRoles()
        {
            return _roles;
        }

        /// <inheritdoc/>
        public IEnumerable<TUser> GetAllUsers()
        {
            return _users;
        }

        /// <inheritdoc/>
        public bool IsSubscribed(string userName,params string[] roleKeys)
        {
            return _subscriptions.Where(s => s.UserName == userName).Select(s => s.RoleKey).Intersect(roleKeys).Any();
        }

        /// <inheritdoc/>
        public void Join(string userName, string roleKey)
        {
            if (IsSubscribed(userName, roleKey) || FindRole(roleKey) is null || FindUser(userName) is null) return;

            _subscriptions.Add(new(userName, roleKey));
        }

        /// <inheritdoc/>
        public void Update(TUser user)
        {
            if(FindUser(user.UserName) is TUser existingUser) _users.Remove(existingUser);

            _users.Add(user);

        }

        /// <inheritdoc/>
        public void Update(TRole role)
        {
            if (FindRole(role.RoleKey) is TRole existingRole) _roles.Remove(existingRole);

            _roles.Add(role);
        }

        /// <inheritdoc/>
        public Task UpdateAsync(TUser user)
        {
            Update(user);

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task DeleteUserAsync(string userName)
        {
            DeleteUser(userName);

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task<TUser?> FindUserAsync(string userName)
        {
            var user = FindUser(userName);

            return Task.FromResult(user);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TUser>> GetUsersAsync()
        {
            var users = GetAllUsers();

            return Task.FromResult(users);
        }
        
        /// <inheritdoc/>
        public Task UpdateAsync(TRole role)
        {
            Update(role);

            return Task.CompletedTask;
        }
        
        /// <inheritdoc/>
        public Task DeleteRoleAsync(string roleKey)
        {
            DeleteRole(roleKey);

            return Task.CompletedTask;
        }
        
        /// <inheritdoc/>
        public Task<TRole?> FindRoleAsync(string roleKey)
        {
            var role = FindRole(roleKey);

            return Task.FromResult(role);
        }
        
        /// <inheritdoc/>
        public Task<IEnumerable<TRole>> GetRolesAsync()
        {
            var roles = GetAllRoles();

            return Task.FromResult(roles);
        }
        
        /// <inheritdoc/>
        public Task JoinAsync(string userName, string roleKey)
        {
            Join(userName, roleKey);

            return Task.CompletedTask;
        }
        
        /// <inheritdoc/>
        public Task DetachAsync(string userName, string roleKey)
        {
            Detach(userName, roleKey);

            return Task.CompletedTask;
        }
        
        /// <inheritdoc/>
        public Task<bool> IsSubscribedAsync(string userName, params string[] roleKeys)
        {
            var isSubscribed = IsSubscribed(userName, roleKeys);

            return Task.FromResult(isSubscribed);
        }
    }
}
