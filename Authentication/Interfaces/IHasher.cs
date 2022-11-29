using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Interfaces
{
    /// <summary>
    /// Password encription abstraction
    /// </summary>
    public interface IHasher
    {
        /// <summary>
        /// Hash and salt the specified password.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns><c>string</c> that rapresent the hashed value.</returns>
        string Hash(string password, string salt);

        /// <summary>
        /// Check if the password provided match to an hash.
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns><c>True</c> if the has match, <c>False</c> if it does not.</returns>
        bool Check(string hash, string password, string salt);

        /// <summary>
        /// Generate a random value that can be used to salt the password before the hashing.
        /// </summary>
        /// <returns>A <c>string</c> that rapresent the salt value.</returns>
        string GenerateSalt();
    }


}
