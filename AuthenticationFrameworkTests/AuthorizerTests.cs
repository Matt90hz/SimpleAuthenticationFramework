using Authentication;
using Authentication.Interfaces;
using AuthenticationFrameworkTests._Global;
using FluentAssertions;

namespace AuthenticationFrameworkTests;

public sealed class AuthorizerTests
{
    readonly IUserManager<User, Role> _userManager = new UserManagerBuilder<User, Role>()
        .UseDbContextStore(new TestingDbContextFactory())
        .CreateUserManager();

    [Fact]
    public void IsCurrentUserInRole_UserLoggedIsInRole_ReturnsTrue()
    {
        //arrange
        _userManager.Logger.Login("admin", "admin!01");

        //act
        bool isAuthorized = _userManager.Authorizer.IsCurrentUserInRole("ADMIN");
        
        //assert
        isAuthorized.Should().BeTrue();
    }

    [Fact]
    public void IsCurrentUserInRole_UserNotLogged_ReturnsFalse()
    {
        //arrange
        _userManager.Logger.Logout();

        //act
        bool isAuthorized = _userManager.Authorizer.IsCurrentUserInRole("ADMIN", "USER", "GUEST");

        //assert
        isAuthorized.Should().BeFalse();
    }

    [Fact]
    public void IsCurrentUserInRole_UserLoggedNotInRole_ReturnsFalse()
    {
        //arrange
        _userManager.Logger.Login("Pam51", "pampam!23");

        //act
        bool isAuthorized = _userManager.Authorizer.IsCurrentUserInRole("ADMIN");

        //assert
        isAuthorized.Should().BeFalse();
    }

    [Fact]
    public async Task IsCurrentUserInRoleAsync_UserLoggedIsInRole_ReturnsTrue()
    {
        //arrange
        _userManager.Logger.Login("admin", "admin!01");

        //act
        bool isAuthorized = await _userManager.Authorizer.IsCurrentUserInRoleAsync("ADMIN");

        //assert
        isAuthorized.Should().BeTrue();
    }

    [Fact]
    public async Task IsCurrentUserInRoleAsync_UserNotLogged_ReturnsFalse()
    {
        //arrange
        _userManager.Logger.Logout();

        //act
        bool isAuthorized = await _userManager.Authorizer.IsCurrentUserInRoleAsync("ADMIN", "USER", "GUEST");

        //assert
        isAuthorized.Should().BeFalse();
    }

    [Fact]
    public async Task IsCurrentUserInRoleAsync_UserLoggedNotInRole_ReturnsFalse()
    {
        //arrange
        _userManager.Logger.Login("Pam51", "pampam!23");

        //act
        bool isAuthorized = await _userManager.Authorizer.IsCurrentUserInRoleAsync("ADMIN");

        //assert
        isAuthorized.Should().BeFalse();
    }
}