using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Example.Services
{
    /// <summary>
    /// <see cref="INoteService"/> implementation.
    /// </summary>
    public class NoteService : INoteService
    {
        private readonly ObservableCollection<string> _notes = new();

        /// <inheritdoc/>
        public virtual IEnumerable<string> Notes => _notes;

        /// <inheritdoc/>
        public virtual void AddNote(string note)
        {
            _notes.Add(note);
        }
    }
}
