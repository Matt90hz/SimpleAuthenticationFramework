namespace Authentication.Models
{
    /// <summary>
    /// Utility class used to easily manage many-to-many realation between <see cref="IUser"/> and <see cref="IRole"/> in Entity Framework.
    /// </summary>
    /// <remarks>
    /// This is not elegant, woud be more appropriate use implicit many-to-many relation but that would mean put at least one navigation
    /// property inside <see cref="IUser"/> or <see cref="IRole"/>.
    /// Maybe in future updates.
    /// </remarks>
    public sealed class Subscription
    {
        /// <summary>
        /// <see cref="IUser"/> key value.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// <see cref="IRole"/> key value.
        /// </summary>
        public string RoleKey { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="Subscription"/> and initialize the properties.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleKey"></param>
        public Subscription(string userName, string roleKey)
        {
            UserName = userName;
            RoleKey = roleKey;
        }

    }
}
