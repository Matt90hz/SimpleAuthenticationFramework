using Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    /// <summary>
    /// Implementation of <see cref="IHasher"/>.
    /// </summary>
    public class Hasher : IHasher
    {
        /// <inheritdoc/>
        public virtual bool Check(string hash, string password, string salt)
        {
            return hash == Hash(password, salt);
        }

        /// <inheritdoc/>
        public virtual string GenerateSalt()
        {
            var salt = new byte[128 / 8];

            RandomNumberGenerator.Create().GetBytes(salt);

            return Convert.ToBase64String(salt);
        }

        /// <inheritdoc/>
        public virtual string Hash(string password, string salt)
        {
            Rfc2898DeriveBytes hash = new(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt), 10000);

            return Convert.ToBase64String(hash.GetBytes(24));
        }

    }
}
