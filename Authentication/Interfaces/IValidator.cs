namespace Authentication.Interfaces
{
    /// <summary>
    /// Abstraction to validate key properties of <see cref="Models.IUser"/> and <see cref="Models.IRole"/>.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Checks if a password is valid.
        /// </summary>
        /// <remarks>
        /// By default must have at least 6 characters.
        /// </remarks>
        /// <param name="password"></param>
        /// <returns><c>true</c> valid password, otherwise <c>false</c>.</returns>
        bool IsValidPassword(string password);
        /// <summary>
        /// Checks if a user name is valid.
        /// </summary>
        /// <remarks>
        /// By default must have at least 1 character. No symblos. No users with the same <paramref name="userName"/> in the store.
        /// </remarks>
        /// <param name="userName"></param>
        /// <returns><c>true</c> valid, otherwise <c>false</c>.</returns>
        bool IsValidUserName(string userName);
        /// <summary>
        /// Checks if a <c>string</c> is a valid email format.
        /// </summary>
        /// <param name="email"></param>
        /// <returns><c>true</c> valid email, otherwise <c>false</c>.</returns>
        bool IsValidEmail(string email);
        /// <summary>
        /// Checks if a role key is valid.
        /// </summary>
        /// <remarks>
        /// By default must have at least 1 character. No symblos. No roles with the same <paramref name="roleKey"/> in the store.
        /// </remarks>
        /// <param name="roleKey"></param>
        /// <returns><c>true</c> valid, otherwise <c>false</c>.</returns>
        bool IsValidRoleKey(string roleKey);
    }
}
