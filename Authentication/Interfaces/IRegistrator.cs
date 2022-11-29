using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// Throws an <see cref="Exception"/> if <paramref name="user"/> or <paramref name="password"/> are invalid due to <see cref="IValidator"/> policies.
        /// </remarks>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <exception cref="Exception"/>
        void Register(TUser user, string password);

        /// <summary>
        /// Change the password for the the given user.
        /// </summary>
        /// <remarks>
        /// Throws an <see cref="Exception"/> if <paramref name="userName"/> is not an existing <see cref="TUser.UserName"/> or <paramref name="newPassword"/> is invalid due to <see cref="IValidator"/> policies.
        /// </remarks>
        /// <param name="userName"></param>
        /// <param name="newPassword"></param>
        /// <exception cref="Exception"/>
        void ChangePassword(string userName, string newPassword);

        /// <summary>
        /// Remove the given user from the users.
        /// </summary>
        /// <remarks>
        /// Throws an <see cref="Exception"/> if no <see cref="TUser"/> has <paramref name="userName"/> as <see cref="TUser.UserName"/>.
        /// </remarks>
        /// <param name="userName"></param>
        /// <exception cref="Exception"/>
        void Unregister(string userName);

        /// <summary>
        /// Collection of all registered users
        /// </summary>
        IEnumerable<TUser> Users { get; } 
    }
}
