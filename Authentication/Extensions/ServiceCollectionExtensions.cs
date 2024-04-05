using Authentication.Interfaces;
using Authentication.Models;
using Authentication.Stores.DbContextStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Authentication.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add a scoped <see cref="IUserManager{TUser, TRole}"/> service implemented by <see cref="UserManager{TUser, TRole}"/>.
        /// <para>
        /// Use <paramref name="builder"/> to configure <see cref="UserManager{TUser, TRole}"/> using <see cref="UserManagerBuilder{TUser, TRole}"/>.
        /// </para>
        /// </summary>
        /// <remarks>
        /// By default <see cref="UserManagerBuilder{TUser, TRole}"/> uses <see cref="Stores.InMemoryStore{TUser, TRole}"/> as repository.
        /// </remarks>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TRole"></typeparam>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddUserManager<TUser, TRole>(this IServiceCollection services, Action<IServiceProvider, UserManagerBuilder<TUser, TRole>>? builder = null)
            where TUser : class, IUser
            where TRole : class, IRole
        {
            services.AddScoped<IUserManager<TUser, TRole>, UserManager<TUser, TRole>>(serviceProvider =>
            {
                var userManagerBuilder = new UserManagerBuilder<TUser, TRole>();
                builder?.Invoke(serviceProvider, userManagerBuilder);

                return userManagerBuilder.CreateUserManager();
            });

            return services;
        }

        /// <summary>
        /// Add a scoped <see cref="IUserManager{TUser, TRole}"/> service implemented by <see cref="UserManager{TUser, TRole}"/>. 
        /// Uses <typeparamref name="TDbContext"/> as repository.
        /// </summary>
        /// <remarks>
        /// To create <typeparamref name="TDbContext"/> the method uses <see cref="IDbContextFactory{TContext}"/> 
        /// (this because in WPF there is no scope thus a factory makes easier the <see cref="DbContext"/> management). 
        /// Is required that <see cref="IDbContextFactory{TContext}"/> is registered as a service in the <see cref="IServiceProvider"/>.
        /// </remarks>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TRole"></typeparam>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddUserManager<TUser, TRole, TDbContext>(this IServiceCollection services, Action<UserManagerBuilder<TUser, TRole>>? builder = null)
            where TUser : class, IUser
            where TRole : class, IRole
            where TDbContext : DbContext, IAuthenticationDbContext<TUser, TRole>
        {
            services.AddScoped<IUserManager<TUser, TRole>, UserManager<TUser, TRole>>(serviceProvider =>
            {
                var userManagerBuilder = new UserManagerBuilder<TUser, TRole>();
                var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<TDbContext>>();

                builder?.Invoke(userManagerBuilder);

                return userManagerBuilder.UseDbContextStore(dbContextFactory).CreateUserManager();
            });

            return services;
        }

    }
}
