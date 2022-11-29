using System.Collections.Generic;
using System.Linq;

namespace Authentication.Models
{
    /// <summary>
    /// Role abstraction.
    /// </summary>
    public interface IRole
    {
        /// <summary>
        /// Role primary key
        /// </summary>
        string RoleKey { get; set; }
    }

}
