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
        public virtual IEnumerable<TRole> GetUserRoles(string userName)
        {
            foreach(var role in Roles)
            {
                if(_store.IsSubscribed(userName, role.RoleKey)) yield return role;
            }
        }

        /// <inheritdoc/>
        public virtual bool IsSubscribed(string userName,params string[] roleKeys)
        {
            return _store.IsSubscribed(userName, roleKeys);
        }

        /// <inheritdoc/>
        public virtual void Subscribe(string roleKey, string userName)
        {
            if (_store.FindRole(roleKey) is null) throw new Exception("Role not found.");

            if (_store.FindUser(userName) is null) throw new Exception("User not found.");

            _store.Join(userName, roleKey);
            
        }

        /// <inheritdoc/>
        public virtual void Unsubcribe(string roleKey, string userName)
        {
            if(_store.FindRole(roleKey) is null) throw new Exception("Role not found.");

            if (_store.FindUser(userName) is null) throw new Exception("User not found.");

            if (!_store.IsSubscribed(userName, roleKey)) return;

            _store.Detach(userName, roleKey);
        }

    }
}
