using Authentication.Models;
using Authentication.Stores.DbContextStore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationFrameworkTests._Global;

internal sealed class TestingDbContext(DbContextOptions options) : AuthenticationDbContext<User, Role>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasData(new Role { RoleKey = "ADMIN" });
            entity.HasData(new Role { RoleKey = "USER" });
            entity.HasData(new Role { RoleKey = "GUEST" });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasData(new User
            {
                UserName = "admin",
                HashedPassword = "T0+kRC2PJdGSEF++WbFAgARsQ4elKh5J",
                Salt = "dEhAONLdFJwlXExilf64Xw==",
            });

            entity.HasData(new User
            {
                UserName = "Pam51",
                HashedPassword = "XOal7lCwNMKBv722SGpcXHtfG2DB2f8N",
                Salt = "ffeyNB3vWSSDKuxMORvg3g==",
            });

            entity.HasData(new User
            {
                UserName = "Carl101",
                HashedPassword = "z4F2HapFCa/F/DLoaD7+ALAPOLpiqE1+",
                Salt = "DPKUHYj97QDp0D19uyqGlw==",
            });

            entity.HasData(new User
            {
                UserName = "JohnDear56",
                HashedPassword = "nbfsGSxokbt1X1EP2da+tB4wwcz17e2E",
                Salt = "0icoavKHpc5aDss1C+eRpQ==",
            });
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasData(new Subscription("admin", "ADMIN"));
            entity.HasData(new Subscription("admin", "USER"));
            entity.HasData(new Subscription("admin", "GUEST"));
            entity.HasData(new Subscription("Pam51", "USER"));
            entity.HasData(new Subscription("Pam51", "GUEST"));
            entity.HasData(new Subscription("Carl101", "USER"));
            entity.HasData(new Subscription("Carl101", "GUEST"));
            entity.HasData(new Subscription("JohnDear56", "GUEST"));
        });
    }
}
