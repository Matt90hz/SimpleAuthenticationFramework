using Authentication.Stores.DbContextStore;
using Example.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.Repository
{
    /// <summary>
    /// Showcase the simplest way to add user management repository to your project.
    /// Since it is an abstract class implementation no further inheritance can be used.
    /// If you need to avoid class inheritance use the approach shown in <see cref="AppDbContext"/>.
    /// </summary>
    /// <remarks>
    /// I think that is preferrable have a small self contanied <see cref="DbContext"/> that handles users and roles only.
    /// But this context can be extended if more <see cref="DbSet{TEntity}"/> are required.
    /// If <see cref="DbContext.OnModelCreating(ModelBuilder)"/> is overridden, make sure to call <c>base.OnModelCreating(modelBuilder)</c>
    /// since it configures the user management models.
    /// </remarks>
    public class SimpleAppDbContext : AuthenticationDbContext<User, Role>
    {
        /// <inheritdoc/>
        public SimpleAppDbContext(DbContextOptions options) : base(options)
        {

        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Important to include this call
            base.OnModelCreating(modelBuilder);

            //seed role is important, the framework does not allow role creation
            modelBuilder.Entity<Role>(r =>
            {
                r.HasData(new Role { RoleKey = "ADMIN", Description = "User with the highest privileges." });
                r.HasData(new Role { RoleKey = "USER", Description = "User with read/write privileges." });
                r.HasData(new Role { RoleKey = "GUEST", Description = "User with only read privileges" });
            });
        }
    }
}
