using Authentication.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Authentication.Stores.DbContextStore
{
    /// <summary>
    /// Abstraction of the authentication <see cref="DbContext"/> to store data.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    public interface IAuthenticationDbContext<TUser, TRole>
        where TUser : class, IUser
        where TRole : class, IRole
    {
        /// <summary>
        /// Get <see cref="DbSet{TEntity}"/> of <typeparamref name="TRole"/>.
        /// </summary>
        DbSet<TRole> Roles { get; }

        /// <summary>
        /// Get <see cref="DbSet{TEntity}"/> of <typeparamref name="TUser"/>.
        /// </summary>
        DbSet<TUser> Users { get; }

        /// <summary>
        /// Get <see cref="DbSet{TEntity}"/> that rappresent the many-to-many relation between <typeparamref name="TUser"/> and <typeparamref name="TRole"/>.
        /// </summary>
        /// <remarks>
        /// Makes life way easier than configure implicit many-to-many ralation. On the other side the relation cannot be configured further.
        /// </remarks>
        DbSet<Subscription> Subscriptions { get; }
    }
}