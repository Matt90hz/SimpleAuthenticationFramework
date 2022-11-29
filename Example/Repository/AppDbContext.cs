using Authentication.Extensions;
using Authentication.Models;
using Authentication.Stores.DbContextStore;
using Example.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Repository
{
    /// <summary>
    /// Showcase <see cref="IAuthenticationDbContext{TUser, TRole}"/> implementation.
    /// This approach can be used for example when a <see cref="DbContext"/> already inherit from a class.
    /// </summary>
    /// <remarks>
    /// I think that is preferrable have a small self contanied <see cref="DbContext"/> that handles users and roles only.
    /// But there are a nuber of scenarios that can arise in a project and this way guarantees more flexibility. 
    /// </remarks>
    public class AppDbContext : DbContext, IAuthenticationDbContext<User, Role>
    {
        /// <inheritdoc/>
        public DbSet<Role> Roles => Set<Role>();

        /// <inheritdoc/>
        public DbSet<User> Users => Set<User>();

        /// <inheritdoc/>
        public DbSet<Subscription> Subscriptions => Set<Subscription>();

        /// <inheritdoc/>
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        /// <summary>
        /// Remember is key to apply user management configuration in order for the framework to work.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Key operation
            modelBuilder.ApplyUserManagerModelConfiguration<User, Role>();

            //seed role is important, the framework does not allow role creation
            modelBuilder.Entity<Role>(r =>
            {
                r.HasData(new Role { RoleKey = "ADMIN", Description = "User with the highest privileges." });
                r.HasData(new Role { RoleKey = "USER", Description = "User with read/write privileges." });
                r.HasData(new Role { RoleKey = "GUEST", Description = "User with only read privileges" });
            });
        }
    }

    /// <inheritdoc cref="IDesignTimeDbContextFactory{TContext}"/>
    public class AppDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        /// <inheritdoc/>
        public AppDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite();

            return new AppDbContext(options.Options);
        }
    }
}
