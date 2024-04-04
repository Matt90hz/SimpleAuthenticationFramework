using Authentication;
using Authentication.Interfaces;
using Authentication.Stores;
using Authentication.Stores.DbContextStore;
using AuthenticationFrameworkTests._Global;
using FluentAssertions;

namespace AuthenticationFrameworkTests;

public sealed class AuthenticatorTests
{
    readonly IUserManager<User, Role> _userManager = new UserManagerBuilder<User, Role>()
        .UseDbContextStore(new TestingDbContextFactory())
        .CreateUserManager();

    [Fact]
    public void Authenticate_GoodCredential_ReturnsTrue()
    {
        //arrange
        var userName = "admin";
        var password = "admin!01";

        //act
        bool isAuth = _userManager.Authenticator.Authenticate(userName, password);

        //assert
        isAuth.Should().BeTrue();
    }

    [Fact]
    public void Authenticate_UsingWrongPassword_ReturnsFalse()
    {
        //arrange
        var userName = "admin";
        var password = "wrong";

        //act
        bool isAuth = _userManager.Authenticator.Authenticate(userName, password);

        //assert
        isAuth.Should().BeFalse();
    }

    [Fact]
    public void Authenticate_UsingWrongUserName_ReturnsFalse()
    {
        //arrange
        var userName = "wrong";
        var password = "admin!02";

        //act
        bool isAuth = _userManager.Authenticator.Authenticate(userName, password);

        //assert
        isAuth.Should().BeFalse();
    }

    [Fact]
    public async Task AuthenticateAsync_GoodCredential_ReturnsTrue()
    {
        //arrange
        var userName = "admin";
        var password = "admin!01";

        //act
        bool isAuth = await _userManager.Authenticator.AuthenticateAsync(userName, password);

        //assert
        isAuth.Should().BeTrue();
    }

    [Fact]
    public async Task AuthenticateAsync_UsingWrongPassword_ReturnsFalse()
    {
        //arrange
        var userName = "admin";
        var password = "wrong";

        //act
        bool isAuth = await _userManager.Authenticator.AuthenticateAsync(userName, password);

        //assert
        isAuth.Should().BeFalse();
    }

    [Fact]
    public async Task AuthenticateAsync_UsingWrongUserName_ReturnsFalse()
    {
        //arrange
        var userName = "wrong";
        var password = "admin!02";

        //act
        bool isAuth = await _userManager.Authenticator.AuthenticateAsync(userName, password);

        //assert
        isAuth.Should().BeFalse();
    }
}
