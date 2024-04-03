using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Interfaces
{
    /// <summary>
    /// Type agnostic part of <see cref="ILogger{TUser}"/>.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Set <see cref="ILogger{TUser}.CurrentUser"/> to null.
        /// </summary>
        void Logout();

        /// <summary>
        /// Set <see cref="ILogger{TUser}.CurrentUser"/> if <paramref name="userName"/> and <paramref name="password"/> are valid credentials.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns><c>true</c> loging successful, otherwise <c>false</c></returns>
        bool Login(string userName, string password);

        /// <inheritdoc cref="Login(string, string)"/>
        Task<bool> LoginAsync(string userName, string password);

    }

    /// <summary>
    /// Login and logout abstraction.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public interface ILogger<TUser> : ILogger 
        where TUser : IUser
    {
        /// <summary>
        /// Stores the <typeparamref name="TUser"/> currently logged.<br/> 
        /// <c>null</c> if no one is logged.
        /// </summary>
        TUser? CurrentUser { get; }
    }
}
