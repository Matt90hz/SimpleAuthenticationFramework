using Authentication.Interfaces;
using Example.Models;
using Example.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Example.Services;
using Authentication;
using System.Windows;

namespace Example.ViewModels
{
    /// <summary>
    /// View model for <see cref="Views.NoteBoard"/> view.
    /// </summary>
    public sealed class ViewModelNoteBoard : BaseViewModel
    {
        
        private readonly IUserManager<User, Role> _userManager;
        private readonly INoteService _noteService;

        /// <summary>
        /// Collection of notes.
        /// </summary>
        public IEnumerable<string> Nostes => _noteService.Notes;

        /// <summary>
        /// Readable login status.
        /// </summary>
        public string LoginDescription => $"Logged as {_userManager.Logger.CurrentUser?.Name} {_userManager.Logger.CurrentUser?.Surname}, role {GetRole(_userManager)}.";

        /// <summary>
        /// Get <see cref="Commands.AddNoteCommand"/>.
        /// </summary>
        public ICommand AddNoteCommand { get; }

        /// <summary>
        /// Get <see cref="Commands.NavigateToCommand{ViewModelUsers}"/>.
        /// </summary>
        public ICommand UsersCommand { get; }

        /// <summary>
        /// Get <see cref="Commands.LogoutCommand"/>
        /// </summary>
        public ICommand LogoutCommand { get; }

        /// <summary>
        /// Assess if is visible the "Add note" section in the view, based on the role of the logged user.
        /// </summary>
        /// <remarks>
        /// Not the best approach to hide functionality based on roles, but is an easy way to get the result. Fine for the sake of the example.
        /// </remarks>
        public Visibility AddNoteFrameVisibility => _userManager.Authorizer.IsCurrentUserInRole("USER") ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// Assess if is visible the "Users" section in the view, based on the role of the logged user.
        /// </summary>
        /// /// <remarks>
        /// Would be better to use <see cref="ICommand.CanExecute(object?)"/> method to negate the functionality. But it would have add complexity to the example.
        /// </remarks>
        public Visibility UsersMenuVisibility => _userManager.Authorizer.IsCurrentUserInRole("ADMIN") ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// Creates a new instance of <see cref="ViewModelNoteBoard"/> and initializes the fieldes.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="noteService"></param>
        /// <param name="addNoteCommand"></param>
        /// <param name="navigateToUsersCommand"></param>
        /// <param name="logoutCommand"></param>
        public ViewModelNoteBoard(IUserManager<User, Role> userManager, INoteService noteService, 
            AddNoteCommand addNoteCommand, NavigateToCommand<ViewModelUsers> navigateToUsersCommand, LogoutCommand logoutCommand)
        {
            _userManager = userManager;
            _noteService = noteService;

            AddNoteCommand = addNoteCommand;
            UsersCommand = navigateToUsersCommand;
            LogoutCommand = logoutCommand;
        }

        /// <summary>
        /// Helper method to get the <see cref="Role.RoleKey "/> form the current user.
        /// </summary>
        /// <param name="userManager"></param>
        /// <returns>The <see cref="Role.RoleKey"/> of the current user, or an empty string if no one is logged.</returns>
        private static string GetRole(IUserManager<User, Role> userManager)
        {
            if (userManager.Authorizer.IsCurrentUserInRole("ADMIN")) return "ADMIN";
            if (userManager.Authorizer.IsCurrentUserInRole("USER")) return "USER";
            if (userManager.Authorizer.IsCurrentUserInRole("GUEST")) return "GUEST";

            return string.Empty;
        }

    }
}
