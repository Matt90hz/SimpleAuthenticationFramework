using Authentication.Interfaces;
using Example.Models;
using Example.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Example.Commands
{
    /// <summary>
    /// Command to add a note to <see cref="NoteService"/>.
    /// </summary>
    public sealed class AddNoteCommand : ICommand
    {
        private readonly INoteService _noteService;
        private readonly IUserManager<User, Role> _userManager;

        /// <inheritdoc/>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Create a new instance of <see cref="AddNoteCommand"/> and initialize the fields.
        /// </summary>
        /// <param name="noteService"></param>
        /// <param name="userManager"></param>
        public AddNoteCommand(INoteService noteService, IUserManager<User, Role> userManager)
        {
            _noteService = noteService;
            _userManager = userManager;
        }

        /// <inheritdoc/>
        public bool CanExecute(object? parameter)
        {
            //can add a note only if part of "USER" role.
            return _userManager.Authorizer.IsCurrentUserInRole("USER");
        }

        /// <inheritdoc/>
        public void Execute(object? parameter)
        {
            //the note is passed by the command parameter if is null just ignore the command.
            if (parameter is not string note || string.IsNullOrWhiteSpace(note)) return;

            //add a note to the service.
            _noteService.AddNote($"{DateTime.Now}, {_userManager.Logger.CurrentUser?.Name.ToUpper()} {_userManager.Logger.CurrentUser?.Surname.ToUpper()}:\n {note}");
        }
    }
}
