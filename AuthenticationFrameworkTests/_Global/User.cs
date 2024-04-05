using Authentication.Models;

namespace AuthenticationFrameworkTests._Global;

internal sealed class User : IUser
{
    public string UserName { get; set; } = string.Empty;

    public string HashedPassword { get; set; } = string.Empty;

    public string Salt { get; set; } = string.Empty;
}
