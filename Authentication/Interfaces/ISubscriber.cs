using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Models;

namespace Authentication.Interfaces
{
    /// <summary>
    /// Subcription to a <typeparamref name="TRole"/> abstraction.
    /// </summary>
    /// <remarks>
    /// There is no way to create, edit or delete roles using the facade class <see cref="UserManager{TUser, TRole}"/> or any of the more specific facade class inside it.
    /// The idea is that the roles for an application are decided at development time and are not meant to be edited by the users.
    /// The roles must be seeded in the repository manually using an implementation of <see cref="IStore{TUser, TRole}"/> or any other way to write directly into the repository of choise.
    /// </remarks>
    /// <typeparam name="TRole"></typeparam>
    public interface ISubscriber<TRole>
        where TRole : IRole
    {
        /// <summary>
        /// All the <typeparamref name="TRole"/> stored.
        /// </summary>
        IEnumerable<TRole> Roles { get; }
        /// <summary>
        /// Remove a <see cref="IUser"/> form a certain role.
        /// </summary>
        /// <remarks>
        /// Throws an <see cref="Exception"/> if <paramref name="roleKey"/> or <paramref name="userName"/> are not found in the store.
        /// </remarks>
        /// <param name="roleKey"></param>
        /// <param name="userName"></param>
        /// <exception cref="Exception"/>
        void Unsubcribe(string roleKey, string userName);
        /// <summary>
        /// Subscribe a <see cref="IUser"/> to a certain role.
        /// </summary>
        /// <remarks>
        /// Throws an <see cref="Exception"/> if <paramref name="roleKey"/> or <paramref name="userName"/> are not found in the store.
        /// </remarks>
        /// <param name="roleKey"></param>
        /// <param name="userName"></param>
        /// <exception cref="Exception"/>
        void Subscribe(string roleKey, string userName);

        /// <summary>
        /// Get a collection of all roles that a user is subscribed to.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>All <typeparamref name="TRole"/> of a user. An empty collection if <paramref name="userName"/> is not a valid <see cref="IUser.UserName"/>.</returns>
        IEnumerable<TRole> GetUserRoles(string userName);

        /// <summary>
        /// Check if a user is subscribed to any of the roles in <paramref name="roleKeys"/>.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleKeys"></param>
        /// <returns><c>true</c> if is subcribed, otherwise <c>false</c> (even if the role or the user does not exsists).</returns>
        bool IsSubscribed(string userName, params string[] roleKeys);
    }
}
