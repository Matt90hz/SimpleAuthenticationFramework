using Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Extensions
{
    /// <summary>
    /// Exstension methods for <see cref="ModelBuilder"/>
    /// </summary>
    public static partial class ModelBuilderExtensions
    {
        /// <summary>
        /// Configures the minimum propreties and relations in order for the <see cref="Stores.DbContextStore.DbContextStore{TUser, TRole, TDbContext}"/> to work properly. 
        /// <see cref="Stores.DbContextStore.AuthenticationDbContext{TUser, TRole}"/> already use this method to configure the entities.
        /// <para>
        /// Use this method when a custom <see cref="DbContext"/> want to be used. The custom <c>DbContext</c> must implement <see cref="Stores.DbContextStore.IAuthenticationDbContext{TUser, TRole}"/>.
        /// Than override <see cref="DbContext.OnModelCreating(ModelBuilder)"/> and apply this method to the builder.
        /// </para>
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <typeparam name="TRole"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <returns>
        /// The same <see cref="ModelBuilder"/> instance so that additional configuration calls can be chained.
        /// </returns>
        public static ModelBuilder ApplyUserManagerModelConfiguration<TUser, TRole>(this ModelBuilder modelBuilder)
            where TUser : class, IUser
            where TRole : class, IRole
        {
            modelBuilder.Entity<TRole>(entity =>
            {
                entity.HasKey(r => r.RoleKey);
                entity.Property(r => r.RoleKey).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<TUser>(entity =>
            {
                entity.HasKey(u => u.UserName);
                entity.Property(u => u.UserName).HasMaxLength(50).IsRequired();
                entity.Property(u => u.Salt).HasMaxLength(50).IsRequired();
                entity.Property(u => u.HashedPassword).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.HasKey(s => new { s.UserName, s.RoleKey });
                entity.HasOne<TUser>().WithMany().HasForeignKey(s => s.UserName).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne<TRole>().WithMany().HasForeignKey(s => s.RoleKey).OnDelete(DeleteBehavior.Cascade);
            });

            return modelBuilder;
        }
    }
}
