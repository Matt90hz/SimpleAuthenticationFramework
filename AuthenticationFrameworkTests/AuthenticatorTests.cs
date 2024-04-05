using Authentication;
using Authentication.Interfaces;
using AuthenticationFrameworkTests._Global;
using FluentAssertions;

namespace AuthenticationFrameworkTests;

public sealed class AuthenticatorTests
{
    private readonly IUserManager<User, Role> _userManager = new UserManagerBuilder<User, Role>()
        .UseDbContextStore(new TestingDbContextFactory())
        .CreateUserManager();

    [Fact]
    public void Authenticate_GoodCredential_ReturnsTrue()
    {
        //arrange
        string userName = "admin";
        string password = "admin!01";

        //act
        bool isAuth = _userManager.Authenticator.Authenticate(userName, password);

        //assert
        isAuth.Should().BeTrue();
    }

    [Fact]
    public void Authenticate_UsingWrongPassword_ReturnsFalse()
    {
        //arrange
        string userName = "admin";
        string password = "wrong";

        //act
        bool isAuth = _userManager.Authenticator.Authenticate(userName, password);

        //assert
        isAuth.Should().BeFalse();
    }

    [Fact]
    public void Authenticate_UsingWrongUserName_ReturnsFalse()
    {
        //arrange
        string userName = "wrong";
        string password = "admin!02";

        //act
        bool isAuth = _userManager.Authenticator.Authenticate(userName, password);

        //assert
        isAuth.Should().BeFalse();
    }

    [Fact]
    public async Task AuthenticateAsync_GoodCredential_ReturnsTrue()
    {
        //arrange
        string userName = "admin";
        string password = "admin!01";

        //act
        bool isAuth = await _userManager.Authenticator.AuthenticateAsync(userName, password);

        //assert
        isAuth.Should().BeTrue();
    }

    [Fact]
    public async Task AuthenticateAsync_UsingWrongPassword_ReturnsFalse()
    {
        //arrange
        string userName = "admin";
        string password = "wrong";

        //act
        bool isAuth = await _userManager.Authenticator.AuthenticateAsync(userName, password);

        //assert
        isAuth.Should().BeFalse();
    }

    [Fact]
    public async Task AuthenticateAsync_UsingWrongUserName_ReturnsFalse()
    {
        //arrange
        string userName = "wrong";
        string password = "admin!02";

        //act
        bool isAuth = await _userManager.Authenticator.AuthenticateAsync(userName, password);

        //assert
        isAuth.Should().BeFalse();
    }
}
