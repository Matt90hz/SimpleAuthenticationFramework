using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationFrameworkTests._Global;

internal sealed class User : IUser
{
    public string UserName { get; set; } = string.Empty;

    public string HashedPassword { get; set; } = string.Empty;

    public string Salt { get; set; } = string.Empty;
}
