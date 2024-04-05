using Authentication;
using Authentication.Exceptions;
using Authentication.Interfaces;
using AuthenticationFrameworkTests._Global;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationFrameworkTests;

public sealed class SubscriberTests
{
    private readonly TestingDbContextFactory _dbContextFactory;
    private readonly IUserManager<User, Role> _userManager;

    public SubscriberTests()
    {
        _dbContextFactory = new TestingDbContextFactory(
            new DbContextOptionsBuilder()
                .UseSqlite("Datasource=SubscriberTestsDatabase")
                .EnableSensitiveDataLogging()
                .Options);

        _userManager = new UserManagerBuilder<User, Role>()
            .UseDbContextStore(_dbContextFactory)
            .CreateUserManager();
    }

    [Fact]
    public void Roles_Returns_AllRoles()
    {
        //act
        IEnumerable<Role> roles = _userManager.Subscriber.Roles;

        //assert
        roles
            .Select(role => role.RoleKey)
            .Order()
            .Should()
            .Equal(["ADMIN", "GUEST", "USER"]);
    }

    [Fact]
    public void GetUserRoles_Returns_AllTheRolesSubscribed()
    {
        //arrange
        string user = "Pam51";
        string[] expectedSubscriptions = ["GUEST", "USER"];

        //act
        IEnumerable<Role> subscriptions = _userManager.Subscriber.GetUserRoles(user);

        //assert
        subscriptions
            .Select(role => role.RoleKey)
            .Order()
            .Should()
            .Equal(expectedSubscriptions);
    }

    [Fact]
    public void IsSubscribed_UserSubscribedToRole_ShouldBeTrue()
    {
        //arrange
        string user = "Pam51";
        string role = "USER";
        string extraRole = "ADMIN";

        //act
        bool isSubscribed = _userManager.Subscriber.IsSubscribed(user, role, extraRole);

        //assert
        isSubscribed.Should().BeTrue();
    }

    [Fact]
    public void IsSubscribed_UserNotSubscribedToRole_ShouldBeFalse()
    {
        //arrange
        string user = "Pam51";
        string role = "ADMIN";

        //act
        bool isSubscribed = _userManager.Subscriber.IsSubscribed(user, role);

        //assert
        isSubscribed.Should().BeFalse();
    }

    [Fact]
    public void Subscribe_UserIsSubscribed_ShouldBeTrue()
    {
        //arrange
        string user = "Pam51";
        string addedRole = "ADMIN";

        //act
        _userManager.Subscriber.Subscribe(addedRole, user);
        bool isSubscribed = _userManager.Subscriber.IsSubscribed(user, addedRole);

        //assert
        isSubscribed.Should().BeTrue();

        //finally
        _dbContextFactory
            .CreateDbContext()
            .Database
            .EnsureDeleted();
    }

    [Fact]
    public void Subscribe_UsingInvalidUser_ShouldThrowInvalidUserException()
    {
        //arrange
        string user = "wrong";
        string addedRole = "ADMIN";

        //act
        Action act = () => _userManager.Subscriber.Subscribe(addedRole, user);

        //assert
        act.Should().Throw<InvalidUserException>();
    }

    [Fact]
    public void Subscribe_UsingInvalidRole_ShouldThrowInvalidRoleException()
    {
        //arrange
        string user = "Pam51";
        string addedRole = "wrong";

        //act
        Action act = () => _userManager.Subscriber.Subscribe(addedRole, user);

        //assert
        act.Should().Throw<InvalidRoleException>();
    }

    [Fact]
    public void Unsubscribe_UserIsSubscribed_ShouldBeFalse()
    {
        //arrange
        string user = "Pam51";
        string removedRole = "USER";

        //act
        _userManager.Subscriber.Unsubscribe(removedRole, user);
        bool isSubscribed = _userManager.Subscriber.IsSubscribed(user, removedRole);

        //assert
        isSubscribed.Should().BeFalse();

        //finally
        _dbContextFactory
            .CreateDbContext()
            .Database
            .EnsureDeleted();
    }

    [Fact]
    public void Unubscribe_UsingInvalidUser_ShouldThrowInvalidUserException()
    {
        //arrange
        string user = "wrong";
        string addedRole = "ADMIN";

        //act
        Action act = () => _userManager.Subscriber.Unsubscribe(addedRole, user);

        //assert
        act.Should().Throw<InvalidUserException>();
    }

    [Fact]
    public void Unubscribe_UsingInvalidRole_ShouldThrowInvalidRoleException()
    {
        //arrange
        string user = "Pam51";
        string addedRole = "wrong";

        //act
        Action act = () => _userManager.Subscriber.Unsubscribe(addedRole, user);

        //assert
        act.Should().Throw<InvalidRoleException>();
    }

    [Fact]
    public async Task GetRolesAsync_Returns_AllRoles()
    {
        //act
        IEnumerable<Role> roles = await _userManager.Subscriber.GetRolesAsync();

        //assert
        roles
            .Select(role => role.RoleKey)
            .Order()
            .Should()
            .Equal(["ADMIN", "GUEST", "USER"]);
    }

    [Fact]
    public async Task GetUserRolesAsync_Returns_AllTheRolesSubscribed()
    {
        //arrange
        string user = "Pam51";
        string[] expectedSubscriptions = ["GUEST", "USER"];

        //act
        IEnumerable<Role> subscriptions = await _userManager.Subscriber.GetUserRolesAsync(user);

        //assert
        subscriptions
            .Select(role => role.RoleKey)
            .Order()
            .Should()
            .Equal(expectedSubscriptions);
    }

    [Fact]
    public async Task IsSubscribedAsync_UserSubscribedToRole_ShouldBeTrue()
    {
        //arrange
        string user = "Pam51";
        string role = "USER";
        string extraRole = "ADMIN";

        //act
        bool isSubscribed = await _userManager.Subscriber.IsSubscribedAsync(user, role, extraRole);

        //assert
        isSubscribed.Should().BeTrue();
    }

    [Fact]
    public async Task IsSubscribedAsync_UserNotSubscribedToRole_ShouldBeFalse()
    {
        //arrange
        string user = "Pam51";
        string role = "ADMIN";

        //act
        bool isSubscribed = await _userManager.Subscriber.IsSubscribedAsync(user, role);

        //assert
        isSubscribed.Should().BeFalse();
    }

    [Fact]
    public async Task SubscribeAsync_UserIsSubscribed_ShouldBeTrue()
    {
        //arrange
        string user = "Pam51";
        string addedRole = "ADMIN";

        //act
        await _userManager.Subscriber.SubscribeAsync(addedRole, user);
        bool isSubscribed = _userManager.Subscriber.IsSubscribed(user, addedRole);

        //assert
        isSubscribed.Should().BeTrue();

        //finally
        _dbContextFactory
            .CreateDbContext()
            .Database
            .EnsureDeleted();
    }

    [Fact]
    public async Task SubscribeAsync_UsingInvalidUser_ShouldThrowInvalidUserException()
    {
        //arrange
        string user = "wrong";
        string addedRole = "ADMIN";

        //act
        Func<Task> act = async () => await _userManager.Subscriber.SubscribeAsync(addedRole, user);

        //assert
        await act.Should().ThrowAsync<InvalidUserException>();
    }

    [Fact]
    public async Task SubscribeAsync_UsingInvalidRole_ShouldThrowInvalidRoleException()
    {
        //arrange
        string user = "Pam51";
        string addedRole = "wrong";

        //act
        Func<Task> act = async () => await _userManager.Subscriber.SubscribeAsync(addedRole, user);

        //assert
        await act.Should().ThrowAsync<InvalidRoleException>();
    }

    [Fact]
    public async Task UnsubscribeAsync_UserIsSubscribed_ShouldBeFalse()
    {
        //arrange
        string user = "Pam51";
        string removedRole = "USER";

        //act
        await _userManager.Subscriber.UnsubscribeAsync(removedRole, user);
        bool isSubscribed = _userManager.Subscriber.IsSubscribed(user, removedRole);

        //assert
        isSubscribed.Should().BeFalse();

        //finally
        _dbContextFactory
            .CreateDbContext()
            .Database
            .EnsureDeleted();
    }

    [Fact]
    public async Task UnubscribeAsync_UsingInvalidUser_ShouldThrowInvalidUserException()
    {
        //arrange
        string user = "wrong";
        string addedRole = "ADMIN";

        //act
        Func<Task> act = async () => await _userManager.Subscriber.UnsubscribeAsync(addedRole, user);

        //assert
        await act.Should().ThrowAsync<InvalidUserException>();
    }

    [Fact]
    public async Task UnubscribeAsync_UsingInvalidRole_ShouldThrowInvalidRoleException()
    {
        //arrange
        string user = "Pam51";
        string addedRole = "wrong";

        //act
        Func<Task> act = async () => await _userManager.Subscriber.UnsubscribeAsync(addedRole, user);

        //assert
        await act.Should().ThrowAsync<InvalidRoleException>();
    }
}
