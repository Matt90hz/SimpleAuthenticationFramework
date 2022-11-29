using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Services
{
    /// <summary>
    /// Service that holds a list of notes.
    /// </summary>
    /// <remarks>
    /// The notes would be better to be a class rather then a simple string. It is fine for the sake of the example.
    /// </remarks>
    public interface INoteService
    {
        /// <summary>
        /// Add a note to <see cref="Notes"/>.
        /// </summary>
        /// <param name="note"></param>
        void AddNote(string note);

        /// <summary>
        /// Collection of notes.
        /// </summary>
        IEnumerable<string> Notes { get; }
    }
}
