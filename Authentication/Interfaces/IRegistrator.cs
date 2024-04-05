using Authentication.Exceptions;
using Authentication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authentication.Interfaces
{
    /// <summary>
    /// <typeparamref name="TUser"/> registration abstraction.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface IRegistrator<TUser> where TUser : IUser
    {
        /// <summary>
        /// Register <paramref name="user"/> with the given <paramref name="password"/>.
        /// </summary>
        /// <remarks>
        /// Throws an <see cref="InvalidUserException"/> or <see cref="InvalidRoleException"/> if <paramref name="user"/> or <paramref name="password"/> are invalid due to <see cref="IValidator"/> policies.
        /// </remarks>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <exception cref="InvalidUserException"/>
        /// <exception cref="InvalidRoleException"/>
        void Register(TUser user, string password);

        /// <inheritdoc cref="Register(TUser, string)"/>
        Task RegisterAsync(TUser user, string password);

        /// <summary>
        /// Change the password for the the given user.
        /// </summary>
        /// <remarks>
        /// Throws an <see cref="InvalidUserException"/> if <paramref name="userName"/> is not an existing <see cref="IUser.UserName"/> 
        /// or an <see cref="InvalidPasswordException"/> if <paramref name="newPassword"/> is invalid due to <see cref="IValidator"/> policies.
        /// </remarks>
        /// <param name="userName"></param>
        /// <param name="newPassword"></param>
        /// <exception cref="InvalidUserException"/>
        /// <exception cref="InvalidPasswordException"/>
        void ChangePassword(string userName, string newPassword);

        /// <inheritdoc cref="ChangePassword(string, string)"/>
        Task ChangePasswordAsync(string userName, string newPassword);

        /// <summary>
        /// Remove the given user from the users.
        /// </summary>
        /// <remarks>
        /// Throws an <see cref="InvalidUserException"/> if no <see cref="IUser"/> has <paramref name="userName"/> as <see cref="IUser.UserName"/>.
        /// </remarks>
        /// <param name="userName"></param>
        /// <exception cref="InvalidUserException"/>
        void Unregister(string userName);

        /// <inheritdoc cref="Unregister(string)"/>
        Task UnregisterAsync(string userName);

        /// <summary>
        /// Collection of all registered users
        /// </summary>
        IEnumerable<TUser> Users { get; }

        /// <summary>
        /// Non blocking way to get users
        /// </summary>
        /// <returns>Collection of all registered users</returns>
        Task<IEnumerable<TUser>> GetUsersAsync();
    }
}
