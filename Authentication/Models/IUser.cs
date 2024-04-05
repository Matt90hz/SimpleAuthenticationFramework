namespace Authentication.Models
{
    /// <summary>
    /// User abstraction.
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// User primary key
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// Encrypted user password.
        /// </summary>
        string HashedPassword { get; set; }
        /// <summary>
        /// Salt value for encryption.
        /// </summary>
        /// <remarks>
        /// Used to guarantee different <see cref="HashedPassword"/> for every user.
        /// </remarks>
        string Salt { get; set; }

    }
}
