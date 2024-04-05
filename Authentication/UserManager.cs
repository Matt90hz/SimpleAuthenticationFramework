using Authentication.Interfaces;
using Authentication.Models;

namespace Authentication
{
    /// <summary>
    /// Implementation of <see cref="IUserManager{TUser, TRole}"/>
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    public class UserManager<TUser, TRole> : IUserManager<TUser, TRole>
        where TUser : IUser
        where TRole : IRole
    {

        /// <summary>
        /// This constructor is not meant to be used. Use <see cref="UserManagerBuilder{TUser, TRole}"/> instead.
        /// </summary>
        /// <param name="authenticator"></param>
        /// <param name="logger"></param>
        /// <param name="registrator"></param>
        /// <param name="subscriber"></param>
        /// <param name="authorizer"></param>
        public UserManager(IAuthenticator authenticator, ILogger<TUser> logger, IRegistrator<TUser> registrator, ISubscriber<TRole> subscriber, IAuthorizer authorizer)
        {
            Authenticator = authenticator;
            Logger = logger;
            Registrator = registrator;
            Subscriber = subscriber;
            Authorizer = authorizer;
        }

        /// <inheritdoc/>
        public virtual IAuthenticator Authenticator { get; }

        /// <inheritdoc/>
        public virtual ILogger<TUser> Logger { get; }

        /// <inheritdoc/>
        public virtual IRegistrator<TUser> Registrator { get; }

        /// <inheritdoc/>
        public virtual ISubscriber<TRole> Subscriber { get; }

        /// <inheritdoc/>
        public virtual IAuthorizer Authorizer { get; }
    }
}
