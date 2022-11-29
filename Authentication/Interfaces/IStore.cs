using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Interfaces
{
    /// <summary>
    /// Abstraction for storing <typeparamref name="TUser"/> and <typeparamref name="TRole"/> data.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    public interface IStore<TUser, TRole>
        where TUser : IUser
        where TRole : IRole
    {
        /// <summary>
        /// Update <typeparamref name="TUser"/> if exsists, otherwise creates one.
        /// </summary>
        /// <param name="user"></param>
        void Update(TUser user);
        /// <summary>
        /// Delete <typeparamref name="TUser"/> with the given <paramref name="userName"/>.
        /// </summary>
        /// <param name="userName"></param>
        void DeleteUser(string userName);
        /// <summary>
        /// Find the <typeparamref name="TUser"/> with the given <paramref name="userName"/>.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns><typeparamref name="TUser"/> if the user is found, <c>null</c> otherwise.</returns>
        TUser? FindUser(string userName);
        /// <summary>
        /// Get a collection of all <typeparamref name="TUser"/> stored.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<TUser> GetAllUsers();
        /// <summary>
        /// Update <typeparamref name="TRole"/> if exsists, otherwise creates one.
        /// </summary>
        /// <param name="user"></param>
        void Update(TRole role);
        /// <summary>
        /// Delete <typeparamref name="TRole"/> with the given <paramref name="roleKey"/>.
        /// </summary>
        /// <param name="userName"></param>
        void DeleteRole(string roleKey);
        /// <summary>
        /// Find the <typeparamref name="TRole"/> with the given <paramref name="roleKey"/>.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns><typeparamref name="TRole"/> if the role is found, <c>null</c> otherwise.</returns>
        TRole? FindRole(string roleKey);
        /// <summary>
        /// Get a collection of all <typeparamref name="TRole"/> stored.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<TRole> GetAllRoles();
        /// <summary>
        /// Set many-to-many relation between <typeparamref name="TUser"/> and <typeparamref name="TRole"/>.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleKey"></param>
        void Join(string userName, string roleKey);
        /// <summary>
        /// Remove many-to-many relation between <typeparamref name="TUser"/> and <typeparamref name="TRole"/>.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleKey"></param>
        void Detach(string userName, string roleKey);

        /// <summary>
        /// Check if a <typeparamref name="TUser"/> as a many-to-many relations with any of the specified <typeparamref name="TRole"/>.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleKeys"></param>
        /// <returns><c>true</c> if there is a relation, otherwise <c>false</c>.</returns>
        bool IsSubscribed(string userName, params string[] roleKeys);
    }



}
