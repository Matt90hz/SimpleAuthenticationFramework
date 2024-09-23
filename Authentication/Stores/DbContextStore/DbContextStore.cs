using Authentication.Exceptions;
using Authentication.Interfaces;
using Authentication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Stores.DbContextStore
{
    /// <summary>
    /// Implementation of <see cref="IStore{TUser, TRole}"/> that use <see cref="DbContext"/> as repository.
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    /// <typeparam name="TDbContext"></typeparam>
    public class DbContextStore<TUser, TRole, TDbContext> : IStore<TUser, TRole>
        where TUser : class, IUser
        where TRole : class, IRole
        where TDbContext : DbContext, IAuthenticationDbContext<TUser, TRole>
    {
        private readonly IDbContextFactory<TDbContext> _dbContextFactory;

        /// <summary>
        /// Create a new instance of <see cref="DbContextStore"/>, uses <paramref name="dbContextFactory"/> to create the <see cref="DbContext"/> when needed.
        /// </summary>
        /// <param name="dbContextFactory"></param>
        public DbContextStore(IDbContextFactory<TDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc/>
        public virtual void DeleteUser(string userName)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if (context.Users.Find(userName) is not TUser user) return;

            context.Remove(user);
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public virtual void DeleteRole(string roleKey)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if (context.Roles.Find(roleKey) is not TRole role) return;

            context.Remove(role);
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public virtual TUser? FindUser(string userName)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var user = context.Users
                .AsNoTracking()
                .OrderBy(x => x.UserName)
                .FirstOrDefault(x => x.UserName == userName);

            return user;
        }

        /// <inheritdoc/>
        public virtual TRole? FindRole(string roleKey)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var role = context.Roles
                .AsNoTracking()
                .OrderBy(x => x.RoleKey)
                .FirstOrDefault(x => x.RoleKey == roleKey);

            return role;
        }

        /// <inheritdoc/>
        public virtual void Update(TUser user)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if (context.Users.AsNoTracking().Any(u => u.UserName == user.UserName))
            {
                context.Users.Update(user);
            }
            else
            {
                context.Users.Add(user);
            }

            context.SaveChanges();

        }

        /// <inheritdoc/>
        public virtual IEnumerable<TUser> GetAllUsers()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return context.Users.AsNoTracking().ToArray();
        }

        /// <inheritdoc/>
        public virtual void Join(string userName, string roleKey)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if (context.Subscriptions.AsNoTracking().Any(s => s.UserName == userName && s.RoleKey == roleKey)) return;

            context.Subscriptions.Add(new(userName, roleKey));
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public virtual void Detach(string userName, string roleKey)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var subscription = context.Subscriptions
                .OrderBy(x => x.UserName)
                .FirstOrDefault(x => x.UserName == userName && x.RoleKey == roleKey);

            if (subscription is null) return;

            context.Remove(subscription);
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public virtual void Update(TRole role)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if (context.Roles.AsNoTracking().Any(r => r.RoleKey == role.RoleKey))
            {
                context.Roles.Update(role);
            }
            else
            {
                context.Roles.Add(role);
            }

            context.SaveChanges();
        }

        /// <inheritdoc/>
        public IEnumerable<TRole> GetAllRoles()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return context.Roles.AsNoTracking().ToArray();
        }

        /// <inheritdoc/>
        public bool IsSubscribed(string userName, params string[] roleKeys)
        {
            using var context = _dbContextFactory.CreateDbContext();

            bool isSubscribed = context.Subscriptions
                .AsNoTracking()
                .Any(subscription => subscription.UserName == userName && roleKeys.Contains(subscription.RoleKey));

            return isSubscribed;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(TUser user)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if (await context.Users.AsNoTracking().AnyAsync(x => x.UserName == user.UserName))
            {
                context.Update(user);
            }
            else
            {
                context.Add(user);
            }

            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteUserAsync(string userName)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var user = await context.FindAsync<TUser>(userName);

            if (user is null) return;

            context.Remove(user);

            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<TUser?> FindUserAsync(string userName)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var user = await context.Users
                .AsNoTracking()
                .OrderBy(x => x.UserName)
                .FirstOrDefaultAsync(x => x.UserName == userName);

            return user;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TUser>> GetUsersAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();

            var users = await context.Users.AsNoTracking().ToArrayAsync();

            return users;
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(TRole role)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if(await context.Roles.AsNoTracking().AnyAsync(x => x.RoleKey == role.RoleKey))
            {
                context.Update(role);
            }
            else
            {
                context.Add(role);
            }

            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteRoleAsync(string roleKey)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var role = await context.FindAsync<TRole>(roleKey);

            if (role is null) return;

            context.Remove(role);

            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<TRole?> FindRoleAsync(string roleKey)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var role = await context.Roles
                .AsNoTracking()
                .OrderBy(x => x.RoleKey)
                .FirstOrDefaultAsync(x => x.RoleKey == roleKey);

            return role;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TRole>> GetRolesAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();

            var roles = await context.Roles.AsNoTracking().ToArrayAsync();

            return roles;
        }

        /// <inheritdoc/>
        public async Task JoinAsync(string userName, string roleKey)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if (await context.Subscriptions.AsNoTracking().AnyAsync(s => s.UserName == userName && s.RoleKey == roleKey)) return;

            context.Add(new Subscription(userName, roleKey));

            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DetachAsync(string userName, string roleKey)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var subscription = context.Subscriptions
                .AsNoTracking()
                .OrderBy(x => x.UserName)
                .FirstOrDefault(s => s.UserName == userName && s.RoleKey == roleKey);

            if (subscription is null) return;

            context.Remove(subscription);

            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> IsSubscribedAsync(string userName, params string[] roleKeys)
        {
            using var context = _dbContextFactory.CreateDbContext();

            bool isSubscribed = await context.Subscriptions
                .AsNoTracking()
                .AnyAsync(subscription => subscription.UserName == userName && roleKeys.Contains(subscription.RoleKey));

            return isSubscribed;
        }
    }
}