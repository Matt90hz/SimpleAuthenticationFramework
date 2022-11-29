using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Models;

namespace Authentication.Interfaces
{
    /// <summary>
    /// Facade for user management abstraction.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    public interface IUserManager<TUser, TRole> 
        where TUser : IUser
        where TRole : IRole
    {
        /// <summary>
        /// Manage identity checks.
        /// </summary>
        IAuthenticator Authenticator { get; }
        /// <summary>
        /// Manage login and logout operations.
        /// </summary>
        ILogger<TUser> Logger { get; }
        /// <summary>
        /// Manage user registration.
        /// </summary>
        IRegistrator<TUser> Registrator { get; }
        /// <summary>
        /// Manage subscription of the users to a role.
        /// </summary>
        ISubscriber<TRole> Subscriber { get; }
        /// <summary>
        /// Manage the permissions of the current user.
        /// </summary>
        IAuthorizer Authorizer { get; }
    }

}
