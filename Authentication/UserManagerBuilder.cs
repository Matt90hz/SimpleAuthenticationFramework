using Authentication.Models;
using Authentication.Interfaces;
using Authentication.Stores;
using Microsoft.EntityFrameworkCore;
using Authentication.Stores.DbContextStore;

namespace Authentication
{
    /// <summary>
    /// Use this class to build and configure <see cref="UserManager{TUser, TRole}"/>.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    public class UserManagerBuilder<TUser, TRole>
        where TUser : class, IUser
        where TRole : class, IRole
    {
        protected IStore<TUser, TRole>? _store;
        protected IHasher? _hasher;
        protected IAuthenticator? _authenticator;
        protected ILogger<TUser>? _logger;
        protected IRegistrator<TUser>? _registrator;
        protected ISubscriber<TRole>? _subscriber;
        protected IValidator? _validator;
        protected IAuthorizer? _authorizer;

        /// <summary>
        /// Creates a new instance of <see cref="UserManagerBuilder{TUser, TRole}"/>.
        /// </summary>
        public UserManagerBuilder()
        {
            
        }

        /// <summary>
        /// Creates a new instance of <see cref="UserManager{TUser, TRole}"/>.
        /// </summary>
        /// <remarks>
        /// By default <see cref="InMemoryStore{TUser, TRole}"/> is used as repository.
        /// </remarks>
        /// <returns><see cref="UserManager{TUser, TRole}"/></returns>
        public UserManager<TUser, TRole> CreateUserManager()
        {
            _store ??= new InMemoryStore<TUser, TRole>();
            
            _hasher ??= new Hasher();
            
            _authenticator ??= new Authenticator<TUser, TRole>(_store, _hasher);
            
            _logger ??= new Logger<TUser, TRole>(_store, _authenticator);
            
            _validator ??= new Validator<TUser, TRole>(_store);

            _registrator ??= new Registrator<TUser, TRole>(_store, _hasher, _validator);
            
            _subscriber ??= new Subscriber<TUser, TRole>(_store);     

            _authorizer ??= new Authorizer<TUser, TRole>(_store, _logger);
            
            return new UserManager<TUser, TRole>(_authenticator, _logger, _registrator, _subscriber, _authorizer); 
        }

        /// <summary>
        /// Use this method to configure <see cref="UserManager{TUser, TRole}"/> to use <paramref name="logger"/> as implementation of <see cref="ILogger{TUser}"/>.
        /// </summary>
        /// <remarks>
        /// By default is used <see cref="Logger{TUser}"/>.
        /// </remarks>
        /// <param name="logger"></param>
        /// <returns><see cref="UserManagerBuilder{TUser, TRole}"/> to chain the configuration.</returns>
        public UserManagerBuilder<TUser, TRole> AddLogger(ILogger<TUser> logger)
        {
            _logger = logger;
            return this;
        }

        /// <summary>
        /// Use this method to configure <see cref="UserManager{TUser, TRole}"/> to use <paramref name="authenticator"/> as implementation of <see cref="IAuthenticator"/>.
        /// </summary>
        /// <remarks>
        /// By default is used <see cref="Authenticator{TUser, TRole}"/>.
        /// </remarks>
        /// <param name="authenticator"></param>
        /// <returns><see cref="UserManagerBuilder{TUser, TRole}"/> to chain the configuration.</returns>
        public UserManagerBuilder<TUser, TRole> AddAuthenticator(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            return this;
        }

        /// <summary>
        /// Use this method to configure <see cref="UserManager{TUser, TRole}"/> to use <paramref name="registrator"/> as implementation of <see cref="IRegistrator{TUser}"/>.
        /// </summary>
        /// <remarks>
        /// By default is used <see cref="Registrator{TUser, TRole}"/>.
        /// </remarks>
        /// <param name="registrator"></param>
        /// <returns><see cref="UserManagerBuilder{TUser, TRole}"/> to chain the configuration.</returns>
        public UserManagerBuilder<TUser, TRole> AddRegistrator(IRegistrator<TUser> registrator)
        {
            _registrator = registrator;
            return this;
        }

        /// <summary>
        /// Use this method to configure <see cref="UserManager{TUser, TRole}"/> to use <paramref name="subscriber"/> as implementation of <see cref="ISubscriber{TRole}"/>.
        /// </summary>
        /// <remarks>
        /// By default is used <see cref="Subscriber{TUser, TRole}"/>.
        /// </remarks>
        /// <param name="subscriber"></param>
        /// <returns><see cref="UserManagerBuilder{TUser, TRole}"/> to chain the configuration.</returns>
        public UserManagerBuilder<TUser, TRole> AddSubscriber(ISubscriber<TRole> subscriber)
        {
            _subscriber = subscriber;
            return this;
        }

        /// <summary>
        /// Use this method to configure <see cref="UserManager{TUser, TRole}"/> to use <paramref name="hasher"/> as implementation of <see cref="IHasher"/>.
        /// </summary>
        /// <remarks>
        /// By default is used <see cref="Hasher"/>.
        /// </remarks>
        /// <param name="hasher"></param>
        /// <returns><see cref="UserManagerBuilder{TUser, TRole}"/> to chain the configuration.</returns>
        public UserManagerBuilder<TUser, TRole> AddHasher(IHasher hasher)
        {
            _hasher = hasher;
            return this;
        }

        /// <summary>
        /// Use this method to configure <see cref="UserManager{TUser, TRole}"/> to use <paramref name="validator"/> as implementation of <see cref="IValidator"/>.
        /// </summary>
        /// <remarks>
        /// By default is used <see cref="Validator{TUser, TRole}"/>.
        /// </remarks>
        /// <param name="validator"></param>
        /// <returns><see cref="UserManagerBuilder{TUser, TRole}"/> to chain the configuration.</returns>
        public UserManagerBuilder<TUser, TRole> AddValidator(IValidator validator)
        {
            _validator = validator;
            return this;
        }

        /// <summary>
        /// Use this method to configure <see cref="UserManager{TUser, TRole}"/> to use <paramref name="authorizer"/> as implementation of <see cref="IAuthorizer"/>.
        /// </summary>
        /// <remarks>
        /// By default is used <see cref="Authorizer{TUser, TRole}"/>.
        /// </remarks>
        /// <param name="authorizer"></param>
        /// <returns><see cref="UserManagerBuilder{TUser, TRole}"/> to chain the configuration.</returns>
        public UserManagerBuilder<TUser, TRole> AddAuthorizer(IAuthorizer authorizer)
        {
            _authorizer = authorizer;
            return this;
        }

        /// <summary>
        /// Use this method to configure <see cref="UserManager{TUser, TRole}"/> to use <paramref name="contextStore"/> as implementation of <see cref="IStore{TUser, TRole}"/>.
        /// </summary>
        /// <remarks>
        /// By default is used <see cref="InMemoryStore{TUser, TRole}"/>.
        /// </remarks>
        /// <param name="contextStore"></param>
        /// <returns><see cref="UserManagerBuilder{TUser, TRole}"/> to chain the configuration.</returns>
        public UserManagerBuilder<TUser, TRole> AddStore(IStore<TUser,TRole> contextStore)
        {
            _store = contextStore;
            return this;
        }

        /// <summary>
        /// Use this method to configure <see cref="UserManager{TUser, TRole}"/> to use <see cref="DbContextStore{TUser, TRole, TDbContext}"/> as implementation of <see cref="IStore{TUser, TRole}"/>.
        /// </summary>
        /// <remarks>
        /// Is required an implementation of <see cref="IDbContextFactory{TContext}"/> to initialize <see cref="DbContextStore{TUser, TRole, TDbContext}"/>.
        /// </remarks>
        /// <param name="dbContextFactory"></param>
        /// <returns><see cref="UserManagerBuilder{TUser, TRole}"/> to chain the configuration.</returns>
        public UserManagerBuilder<TUser, TRole> UseDbContextStore<TDbContext>(IDbContextFactory<TDbContext> dbContextFactory) where TDbContext : DbContext, IAuthenticationDbContext<TUser, TRole>
        {
            _store = new DbContextStore<TUser, TRole, TDbContext>(dbContextFactory);
            return this;
        }

        /// <summary>
        /// Use this method to configure <see cref="UserManager{TUser, TRole}"/> to use <see cref="InMemoryStore{TUser, TRole}"/> as implementation of <see cref="IStore{TUser, TRole}"/>.
        /// </summary>
        /// <remarks>
        /// This configuration is used by default.
        /// </remarks>
        /// <returns><see cref="UserManagerBuilder{TUser, TRole}"/> to chain the configuration.</returns>
        public UserManagerBuilder<TUser, TRole> UseInMemoryStore()
        {
            _store = new InMemoryStore<TUser, TRole>();
            return this;
        }
    }

}
