using Authentication.Models;
using Authentication.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Exceptions;

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
            return context.Users.AsNoTracking().FirstOrDefault(u => u.UserName == userName);
        }

        /// <inheritdoc/>
        public virtual TRole? FindRole(string roleKey)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Roles.AsNoTracking().FirstOrDefault(r => r.RoleKey == roleKey);
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
            return context.Users.AsNoTracking().ToList();
        }

        /// <inheritdoc/>
        public virtual void Join(string userName, string roleKey)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if (FindUser(userName) is null || FindRole(roleKey) is null) return;

            if(context.Subscriptions.Any(s => s.UserName == userName && s.RoleKey == roleKey)) return;

            context.Subscriptions.Add(new(userName, roleKey));
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public virtual void Detach(string userName, string roleKey)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if (context.Subscriptions.FirstOrDefault(s => s.UserName == userName && s.RoleKey == roleKey) is not Subscription subscription) return;

            context.Subscriptions.Remove(subscription);
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
            return context.Roles.AsNoTracking().ToList();
        }

        /// <inheritdoc/>
        public bool IsSubscribed(string userName,params string[] roleKeys)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return context.Subscriptions
                .AsNoTracking()
                .Where(s => s.UserName == userName)               
                .ToArray()
                .Any(subscription => roleKeys.Contains(subscription.RoleKey));
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(TUser user)
        {
            using var context = _dbContextFactory.CreateDbContext();

            context.Users.Update(user);

            await context.SaveChangesAsync();
        }
        
        /// <inheritdoc/>
        public async Task DeleteUserAsync(string userName)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var user = await context.FindAsync<TUser>(userName) ?? throw new InvalidUserException($"User {userName} not found!");

            context.Remove(user);

            await context.SaveChangesAsync();
        }
        
        /// <inheritdoc/>
        public async Task<TUser?> FindUserAsync(string userName)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return await context.FindAsync<TUser>(userName);
        }
        
        /// <inheritdoc/>
        public async Task<IEnumerable<TUser>> GetUsersAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();

            return await context.Users.AsNoTracking().ToArrayAsync();
        }
        
        /// <inheritdoc/>
        public async Task UpdateAsync(TRole role)
        {
            using var context = _dbContextFactory.CreateDbContext();

            context.Update(role);

            await context.SaveChangesAsync();
        }
        
        /// <inheritdoc/>
        public async Task DeleteRoleAsync(string roleKey)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var role = await context.FindAsync<TRole>(roleKey) ?? throw new InvalidRoleException($"Role {roleKey} not found!");
        }
        
        /// <inheritdoc/>
        public async Task<TRole?> FindRoleAsync(string roleKey)
        {
            using var context = _dbContextFactory.CreateDbContext();

            return await context.FindAsync<TRole>(roleKey);
        }
        
        /// <inheritdoc/>
        public async Task<IEnumerable<TRole>> GetRolesAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();

            return await context.Roles.AsNoTracking().ToArrayAsync();
        }
        
        /// <inheritdoc/>
        public async Task JoinAsync(string userName, string roleKey)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var user = await context.FindAsync<TUser>(userName) ?? throw new InvalidUserException($"User {userName} not found!");
            var role =  await context.FindAsync<TRole>(roleKey) ?? throw new InvalidRoleException($"Role {roleKey} not found!");

            if (context.Subscriptions.Any(s => s.UserName == userName && s.RoleKey == roleKey)) return;

            context.Subscriptions.Add(new(userName, roleKey));

            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DetachAsync(string userName, string roleKey)
        {
            using var context = _dbContextFactory.CreateDbContext();

            if (context.Subscriptions.FirstOrDefault(s => s.UserName == userName && s.RoleKey == roleKey) is not Subscription subscription) return;

            context.Remove(subscription);

            await context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> IsSubscribedAsync(string userName, params string[] roleKeys)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var subscriptions = await context.Subscriptions.AsNoTracking().Where(s => s.UserName == userName).ToArrayAsync();

            return subscriptions.Any(subscription => roleKeys.Contains(subscription.RoleKey));

        }
    }


}
