﻿using Authentication;
using Authentication.Exceptions;
using Authentication.Interfaces;
using AuthenticationFrameworkTests._Global;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationFrameworkTests;

public sealed class RegistratorTests
{
    readonly IDbContextFactory<TestingDbContext> _dbContextFactory;
    readonly IUserManager<User, Role> _userManager;

    public RegistratorTests()
    {
        _dbContextFactory = new TestingDbContextFactory(
            new DbContextOptionsBuilder()
                .UseSqlite("Datasource=RegistratorTestsDatabase")
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
        var context = _dbContextFactory.CreateDbContext();
        var userName = "Pam51";

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
        var userName = "wrong";

        //act
        Action act = () => _userManager.Registrator.Unregister(userName);

        //assert
        act.Should().Throw<InvalidUserException>();
    }

    [Fact]
    public void Users_ShouldContainAllTheUsers()
    {
        //act
        var users = _userManager.Registrator.Users;

        //assert
        users
            .Select(user => user.UserName)
            .Order()
            .Should()
            .Equal(["admin", "Carl101", "JohnDear56", "Pam51"]);
    }
}