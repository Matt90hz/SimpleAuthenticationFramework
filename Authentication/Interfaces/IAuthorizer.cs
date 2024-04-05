using System.Threading.Tasks;

namespace Authentication.Interfaces
{
    /// <summary>
    /// Authorization abstraction
    /// </summary>
    public interface IAuthorizer
    {
        /// <summary>
        /// Checks if the user currently logged is subscribed to any role in <paramref name="roleKeys"/>.
        /// </summary>
        /// <param name="roleKeys"></param>
        /// <returns>
        /// <c>True</c> if the user is subscribed to the role. <c>False</c> if not.
        /// </returns>
        bool IsCurrentUserInRole(params string[] roleKeys);

        /// <inheritdoc cref="IsCurrentUserInRole(string[])"/>
        Task<bool> IsCurrentUserInRoleAsync(params string[] roleKeys);
    }
}
