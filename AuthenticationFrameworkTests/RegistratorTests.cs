using Authentication;
using Authentication.Exceptions;
using Authentication.Interfaces;
using AuthenticationFrameworkTests._Global;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationFrameworkTests;

public sealed class RegistratorTests
{
    private readonly TestingDbContextFactory _dbContextFactory;
    private readonly IUserManager<User, Role> _userManager;

    public RegistratorTests()
    {
        _dbContextFactory = new TestingDbContextFactory(
            new DbContextOptionsBuilder()
                .UseSqlite("Datasource=RegistratorTestsDatabase")
                .EnableSensitiveDataLogging()
                .Options);

        _userManager = new UserManagerBuilder<User, Role>()
            .UseDbContextStore(_dbContextFactory)
            .CreateUserManager();
    }

    [Fact]
    public void ChangePassword_User_CanLoginWithNewPassword()
    {
        //arrange
        string userName = "admin";
        string password = "admin!01";
        string newPassword = "newpassword!01";
        bool isLogged = _userManager.Logger.Login(userName, password);
        _userManager.Logger.Logout();

        //act
        _userManager.Registrator.ChangePassword(userName, newPassword);
        bool isLoggedWithNewPass = _userManager.Logger.Login(userName, newPassword);
        _userManager.Logger.Logout();

        //assert
        isLogged.Should().BeTrue();
        isLoggedWithNewPass.Should().BeTrue();

        //finally
        _dbContextFactory
            .CreateDbContext()
            .Database
            .EnsureDeleted();
    }

    [Fact]
    public void ChangePassword_ToInvalidUser_ThrowInvalidUserException()
    {
        //arrange
        string userName = "wrong";
        string newPassword = "newpassword!01";

        //act
        Action act = () => _userManager.Registrator.ChangePassword(userName, newPassword);

        //assert
        act.Should().Throw<InvalidUserException>();
    }

    [Fact]
    public void ChangePassword_UsingInvalidPassword_ThrowInvalidPasswordException()
    {
        //arrange
        string userName = "admin";
        string newPassword = "wrong";

        //act
        Action act = () => _userManager.Registrator.ChangePassword(userName, newPassword);

        //assert
        act.Should().Throw<InvalidPasswordException>();
    }

    [Fact]
    public void Register_NewUser_CanLogin()
    {
        //arrange
        User newUser = new()
        {
            UserName = "newUser",
        };
        string password = "newpass!01";

        //act
        _userManager.Registrator.Register(newUser, password);
        bool isLogged = _userManager.Logger.Login(newUser.UserName, password);

        //assert
        isLogged.Should().BeTrue();

        //finally
        _dbContextFactory
            .CreateDbContext()
            .Database
            .EnsureDeleted();
    }

    [Fact]
    public void Register_UserWithInvalidUserName_ThrowInvalidUsernameException()
    {
        //arrange
        User newUser = new()
        {
            UserName = string.Empty,
        };
        string password = "newpass!01";

        //act
        Action act = () => _userManager.Registrator.Register(newUser, password);

        //assert
        act.Should().Throw<InvalidUserException>();
    }

    [Fact]
    public void Register_UserWithExistingUserName_ThrowInvalidUsernameException()
    {
        //arrange
        User newUser = new()
        {
            UserName = "admin",
        };
        string password = "newpass!01";

        //act
        Action act = () => _userManager.Registrator.Register(newUser, password);

        //assert
        act.Should().Throw<InvalidUserException>();
    }

    [Fact]
    public void Register_UserWithInvalidPassword_ThrowInvalidPasswordException()
    {
        //arrange
        User newUser = new()
        {
            UserName = "newUser",
        };
        string password = "wrong";

        //act
        Action act = () => _userManager.Registrator.Register(newUser, password);

        //assert
        act.Should().Throw<InvalidPasswordException>();
    }

    [Fact]
    public void Unregister_User_RemovesItFromDatabase()
    {
        //arrange
        TestingDbContext context = _dbContextFactory.CreateDbContext();
        string userName = "Pam51";

        //act
        bool pamExsists = context.Users.Any(user => user.UserName == userName);
        _userManager.Registrator.Unregister(userName);
        bool pamExsistsAfter = context.Users.Any(user => user.UserName == userName);

        //assert
        pamExsists.Should().BeTrue();
        pamExsistsAfter.Should().BeFalse();

        //finally
        context.Database.EnsureDeleted();
    }

    [Fact]
    public void Unregister_InvalidUser_ThorwInvalidUserException()
    {
        //arrange
        string userName = "wrong";

        //act
        Action act = () => _userManager.Registrator.Unregister(userName);

        //assert
        act.Should().Throw<InvalidUserException>();
    }

    [Fact]
    public void Users_ShouldContainAllTheUsers()
    {
        //act
        IEnumerable<User> users = _userManager.Registrator.Users;

        //assert
        users
            .Select(user => user.UserName)
            .Order()
            .Should()
            .Equal(["admin", "Carl101", "JohnDear56", "Pam51"]);
    }

    [Fact]
    public async Task ChangePasswordAsync_User_CanLoginWithNewPassword()
    {
        //arrange
        string userName = "admin";
        string password = "admin!01";
        string newPassword = "newpassword!01";
        bool isLogged = _userManager.Logger.Login(userName, password);
        _userManager.Logger.Logout();

        //act
        await _userManager.Registrator.ChangePasswordAsync(userName, newPassword);
        bool isLoggedWithNewPass = _userManager.Logger.Login(userName, newPassword);
        _userManager.Logger.Logout();

        //assert
        isLogged.Should().BeTrue();
        isLoggedWithNewPass.Should().BeTrue();

        //finally
        _dbContextFactory
            .CreateDbContext()
            .Database
            .EnsureDeleted();
    }

    [Fact]
    public async Task ChangePasswordAsync_ToInvalidUser_ThrowInvalidUserException()
    {
        //arrange
        string userName = "wrong";
        string newPassword = "newpassword!01";

        //act
        Func<Task> act = async () => await _userManager.Registrator.ChangePasswordAsync(userName, newPassword);

        //assert
        await act.Should().ThrowAsync<InvalidUserException>();
    }

    [Fact]
    public async Task ChangePasswordAsync_UsingInvalidPassword_ThrowInvalidPasswordException()
    {
        //arrange
        string userName = "admin";
        string newPassword = "wrong";

        //act
        Func<Task> act = async () => await _userManager.Registrator.ChangePasswordAsync(userName, newPassword);

        //assert
        await act.Should().ThrowAsync<InvalidPasswordException>();
    }

    [Fact]
    public async Task RegisterAsync_NewUser_CanLogin()
    {
        //arrange
        User newUser = new()
        {
            UserName = "newUser",
        };
        string password = "newpass!01";

        //act
        await _userManager.Registrator.RegisterAsync(newUser, password);
        bool isLogged = _userManager.Logger.Login(newUser.UserName, password);

        //assert
        isLogged.Should().BeTrue();

        //finally
        _dbContextFactory
            .CreateDbContext()
            .Database
            .EnsureDeleted();
    }

    [Fact]
    public async Task RegisterAsync_UserWithInvalidUserName_ThrowInvalidUsernameException()
    {
        //arrange
        User newUser = new()
        {
            UserName = string.Empty,
        };
        string password = "newpass!01";

        //act
        Func<Task> act = async () => await _userManager.Registrator.RegisterAsync(newUser, password);

        //assert
        await act.Should().ThrowAsync<InvalidUserException>();
    }

    [Fact]
    public async Task RegisterAsync_UserWithExistingUserName_ThrowInvalidUsernameException()
    {
        //arrange
        User newUser = new()
        {
            UserName = "admin",
        };
        string password = "newpass!01";

        //act
        Func<Task> act = async () => await _userManager.Registrator.RegisterAsync(newUser, password);

        //assert
        await act.Should().ThrowAsync<InvalidUserException>();
    }

    [Fact]
    public async Task RegisterAsync_UserWithInvalidPassword_ThrowInvalidPasswordException()
    {
        //arrange
        User newUser = new()
        {
            UserName = "newUser",
        };
        string password = "wrong";

        //act
        Func<Task> act = async () => await _userManager.Registrator.RegisterAsync(newUser, password);

        //assert
        await act.Should().ThrowAsync<InvalidPasswordException>();
    }

    [Fact]
    public async Task UnregisterAsync_User_RemovesItFromDatabase()
    {
        //arrange
        TestingDbContext context = _dbContextFactory.CreateDbContext();
        string userName = "Pam51";

        //act
        bool pamExsists = context.Users.Any(user => user.UserName == userName);
        await _userManager.Registrator.UnregisterAsync(userName);
        bool pamExsistsAfter = context.Users.Any(user => user.UserName == userName);

        //assert
        pamExsists.Should().BeTrue();
        pamExsistsAfter.Should().BeFalse();

        //finally
        context.Database.EnsureDeleted();
    }

    [Fact]
    public async Task UnregisterAsync_InvalidUser_ThorwInvalidUserException()
    {
        //arrange
        string userName = "wrong";

        //act
        Func<Task> act = async () => await _userManager.Registrator.UnregisterAsync(userName);

        //assert
        await act.Should().ThrowAsync<InvalidUserException>();
    }

    [Fact]
    public async Task GetUsersAsync_ShouldContainAllTheUsers()
    {
        //act
        IEnumerable<User> users = await _userManager.Registrator.GetUsersAsync();

        //assert
        users
            .Select(user => user.UserName)
            .Order()
            .Should()
            .Equal(["admin", "Carl101", "JohnDear56", "Pam51"]);
    }
}
