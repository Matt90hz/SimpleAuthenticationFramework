using Authentication;
using Authentication.Interfaces;
using AuthenticationFrameworkTests._Global;
using FluentAssertions;

namespace AuthenticationFrameworkTests;

public sealed class LoggerTests
{
    private readonly IUserManager<User, Role> _userManager = new UserManagerBuilder<User, Role>()
       .UseDbContextStore(new TestingDbContextFactory())
       .CreateUserManager();

    [Fact]
    public void Login_UsingValidCredential_CurrentUserIsSet()
    {
        //arrange
        string userName = "admin";
        string password = "admin!01";

        //act
        bool isLogged = _userManager.Logger.Login(userName, password);

        //assert
        isLogged.Should().BeTrue();
        _userManager.Logger.CurrentUser
            .Should().NotBeNull()
            .And
            .Match<User>(user => user.UserName == userName);
    }

    [Fact]
    public void Login_UsingInvalidUserName_CurrentUserStillNull()
    {
        //arrange
        string userName = "wrong";
        string password = "admin!01";
        _userManager.Logger.Logout();

        //act
        bool isLogged = _userManager.Logger.Login(userName, password);

        //assert
        isLogged.Should().BeFalse();
        _userManager.Logger.CurrentUser
            .Should().BeNull();
    }

    [Fact]
    public void Login_UsingInvalidPassword_CurrentUserStillNull()
    {
        //arrange
        string userName = "admin";
        string password = "wrong";
        _userManager.Logger.Logout();

        //act
        bool isLogged = _userManager.Logger.Login(userName, password);

        //assert
        isLogged.Should().BeFalse();
        _userManager.Logger.CurrentUser
            .Should().BeNull();
    }

    [Fact]
    public async Task LoginAsync_UsingValidCredential_CurrentUserIsSet()
    {
        //arrange
        string userName = "admin";
        string password = "admin!01";

        //act
        bool isLogged = await _userManager.Logger.LoginAsync(userName, password);

        //assert
        isLogged.Should().BeTrue();
        _userManager.Logger.CurrentUser
            .Should().NotBeNull()
            .And
            .Match<User>(user => user.UserName == userName);
    }

    [Fact]
    public async Task LoginAsync_UsingInvalidUserName_CurrentUserStillNull()
    {
        //arrange
        string userName = "wrong";
        string password = "admin!01";
        _userManager.Logger.Logout();

        //act
        bool isLogged = await _userManager.Logger.LoginAsync(userName, password);

        //assert
        isLogged.Should().BeFalse();
        _userManager.Logger.CurrentUser
            .Should().BeNull();
    }

    [Fact]
    public async Task LoginAsync_UsingInvalidPassword_CurrentUserStillNull()
    {
        //arrange
        string userName = "admin";
        string password = "wrong";
        _userManager.Logger.Logout();

        //act
        bool isLogged = await _userManager.Logger.LoginAsync(userName, password);

        //assert
        isLogged.Should().BeFalse();
        _userManager.Logger.CurrentUser
            .Should().BeNull();
    }

    [Fact]
    public void Logout_CurrentUser_ShouldBeNull()
    {
        //arrange
        bool isLogged = _userManager.Logger.Login("admin", "admin!01");

        //act
        _userManager.Logger.Logout();

        //assert
        isLogged.Should().BeTrue();
        _userManager.Logger.CurrentUser.Should().BeNull();
    }
}