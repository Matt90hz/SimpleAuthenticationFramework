using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Models
{
    /// <summary>
    /// Object that rappresent a user. Implements <see cref="IUser"/>.
    /// </summary>
    public class User : IUser
    {
        /// <inheritdoc/>
        public string UserName { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string HashedPassword { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string Salt { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc/>
        public string Surname { get; set; } = string.Empty;
    }
}
