using Authentication.Models;

namespace Example.Models
{
    /// <summary>
    /// Object that rappresents a role. Implements <see cref="IRole"/>.
    /// </summary>
    public class Role : IRole
    {
        /// <inheritdoc/>
        public string RoleKey { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string? Description { get; set; }
    }
}
