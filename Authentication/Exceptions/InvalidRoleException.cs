using System;

namespace Authentication.Exceptions
{
    /// <summary>
    /// Exception thrown during subscription when the role do not exsists
    /// </summary>
    [Serializable]
    public sealed class InvalidRoleException : Exception
    {
        /// <inheritdoc />
        public InvalidRoleException() { }

        /// <inheritdoc />
        public InvalidRoleException(string message) : base(message) { }

        /// <inheritdoc />
        public InvalidRoleException(string message, Exception inner) : base(message, inner) { }

    }
}
