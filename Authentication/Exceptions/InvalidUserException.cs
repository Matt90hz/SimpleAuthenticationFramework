using System;

namespace Authentication.Exceptions
{
    /// <summary>
    /// Exception thrown during user handling operation due to blanck or non existing user name
    /// </summary>
    [Serializable]
    public sealed class InvalidUserException : Exception
    {
        /// <inheritdoc />
        public InvalidUserException() { }

        /// <inheritdoc />
        public InvalidUserException(string message) : base(message) { }

        /// <inheritdoc />
        public InvalidUserException(string message, Exception inner) : base(message, inner) { }

    }
}
