using Authentication.Extensions;
using Authentication.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Authentication.Stores.DbContextStore
{
    /// <summary>
    /// Abstract class that implements <see cref="IAuthenticationDbContext{TUser, TRole}"/>. 
    /// Inherit from this class to get a ready to use <see cref="DbContext"/> for <see cref="Stores.DbContextStore"/>.
    /// </summary>
    /// <remarks>
    /// If you implement directly <see cref="IAuthenticationDbContext{TUser, TRole}"/> on a <see cref="DbContext"/>, remember to call
    /// <see cref="Extensions.ModelBuilderExtensions.ApplyUserManagerModelConfiguration{TUser, TRole}(ModelBuilder)"/>
    /// to configue the model. You can override <see cref="DbContext.OnModelCreating(ModelBuilder)"/> to do so.
    /// </remarks>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    public abstract class AuthenticationDbContext<TUser, TRole> : DbContext, IAuthenticationDbContext<TUser, TRole> where TUser : class, IUser
        where TRole : class, IRole
    {
        /// <inheritdoc/>
        protected AuthenticationDbContext(DbContextOptions options) : base(options)
        {

        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyUserManagerModelConfiguration<TUser, TRole>();

            base.OnModelCreating(modelBuilder);
        }

        /// <inheritdoc/>
        public virtual DbSet<TUser> Users => Set<TUser>();

        /// <inheritdoc/>
        public virtual DbSet<TRole> Roles => Set<TRole>();

        /// <inheritdoc/>
        public virtual DbSet<Subscription> Subscriptions => Set<Subscription>();
    }




}
