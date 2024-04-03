using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Interfaces
{
    /// <summary>
    /// Manage the authentication process.
    /// </summary>
    public interface IAuthenticator
    {
        /// <summary>
        /// Checks if the credatials of a user are valid.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>
        /// <c>True</c> if the credential are valid. <c>False</c> if authentication fails.
        /// </returns>
        bool Authenticate(string userName, string password);

        /// <inheritdoc cref="Authenticate(string, string)"/>
        Task<bool> AuthenticateAsync(string userName, string password);
    }
}
