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

        /// <inheritdoc cref="Update(TUser)"/>
        Task UpdateAsync(TUser user);

        /// <summary>
        /// Delete <typeparamref name="TUser"/> with the given <paramref name="userName"/>.
        /// </summary>
        /// <param name="userName"></param>
        void DeleteUser(string userName);

        /// <inheritdoc cref="DeleteUser(string)"/>
        Task DeleteUserAsync(string userName);

        /// <summary>
        /// Finds the <typeparamref name="TUser"/> with the given <paramref name="userName"/>.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns><typeparamref name="TUser"/> if the user is found, <c>null</c> otherwise.</returns>
        TUser? FindUser(string userName);

        /// <inheritdoc cref="FindUser(string)"/>
        Task<TUser?> FindUserAsync();

        /// <summary>
        /// Get a collection of all <typeparamref name="TUser"/> stored.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/></returns>
        IEnumerable<TUser> GetAllUsers();

        /// <inheritdoc cref="GetAllUsers"/>
        Task<IEnumerable<TUser>> GetUsersAsync();

        /// <summary>
        /// Update <typeparamref name="TRole"/> if exsists, otherwise creates one.
        /// </summary>
        /// <param name="role"></param>
        void Update(TRole role);

        /// <inheritdoc cref="Update(TRole)"/>
        Task UpdateAsync(TRole role);

        /// <summary>
        /// Delete <typeparamref name="TRole"/> with the given <paramref name="roleKey"/>.
        /// </summary>
        /// <param name="roleKey"></param>
        void DeleteRole(string roleKey);

        /// <inheritdoc cref="DeleteRole(string)"/>
        Task DeleteRoleAsync(string roleKey);

        /// <summary>
        /// Find the <typeparamref name="TRole"/> with the given <paramref name="roleKey"/>.
        /// </summary>
        /// <param name="roleKey"></param>
        /// <returns><typeparamref name="TRole"/> if the role is found, <c>null</c> otherwise.</returns>
        TRole? FindRole(string roleKey);

        /// <inheritdoc cref="FindRole(string)"/>
        Task<TRole?> FindRoleAsync(string roleKey);

        /// <summary>
        /// Get a collection of all <typeparamref name="TRole"/> stored.
        /// </summary>
        /// <returns><see cref="IEnumerable{TRole}"/></returns>
        IEnumerable<TRole> GetAllRoles();

        /// <inheritdoc cref="GetAllRoles"/>
        Task<IEnumerable<TRole>> GetRolesAsync();

        /// <summary>
        /// Set many-to-many relation between <typeparamref name="TUser"/> and <typeparamref name="TRole"/>.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleKey"></param>
        void Join(string userName, string roleKey);

        /// <inheritdoc cref="Join(string, string)"/>
        Task JoinAsync(string userName, string roleKey);

        /// <summary>
        /// Remove many-to-many relation between <typeparamref name="TUser"/> and <typeparamref name="TRole"/>.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleKey"></param>
        void Detach(string userName, string roleKey);

        /// <inheritdoc cref="Detach(string, string)"/>
        Task DetachAsync(string userName, string roleKey);

        /// <summary>
        /// Check if a <typeparamref name="TUser"/> as a many-to-many relations with any of the specified <typeparamref name="TRole"/>.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleKeys"></param>
        /// <returns><c>true</c> if there is a relation, otherwise <c>false</c>.</returns>
        bool IsSubscribed(string userName, params string[] roleKeys);

        /// <inheritdoc cref="IsSubscribed(string, string[])"/>
        Task<bool> IsSubscribedAsync(string userName, params string[] roleKeys);
    }



}
