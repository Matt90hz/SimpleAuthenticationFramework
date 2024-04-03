using Authentication.Interfaces;
using System;

namespace Authentication.Exceptions
{
    /// <summary>
    /// Exception thrown during password change due invalid password based on <see cref="IValidator"/>
    /// </summary>
    [Serializable]
    public sealed class InvalidPasswordException : Exception
    {
        /// <inheritdoc />
        public InvalidPasswordException() { }

        /// <inheritdoc />
        public InvalidPasswordException(string message) : base(message) { }

        /// <inheritdoc />
        public InvalidPasswordException(string message, Exception inner) : base(message, inner) { }

    }
}
