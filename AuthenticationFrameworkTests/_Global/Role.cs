using Authentication.Models;

namespace AuthenticationFrameworkTests._Global;

internal sealed class Role : IRole
{
    public string RoleKey { get; set; } = string.Empty;
}
