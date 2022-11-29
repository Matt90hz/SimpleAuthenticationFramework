using Authentication.Interfaces;
using Example.Models;
using Example.ViewModels;
using System;
using System.Windows.Input;

namespace Example.Commands
{
    /// <summary>
    /// Command to perform logout.
    /// </summary>
    public sealed class LogoutCommand : ICommand
    {
        private readonly IUserManager<User, Role> _userManager;
        private readonly ViewModelNavigationManager _navigationManager;

        /// <inheritdoc/>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Creates a new instance of <see cref="LoginCommand"/> and initializes the fields.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="navigationManager"></param>
        public LogoutCommand(IUserManager<User, Role> userManager, ViewModelNavigationManager navigationManager)
        {
            _userManager = userManager;
            _navigationManager = navigationManager;
        }

        /// <inheritdoc/>
        public bool CanExecute(object? parameter)
        {
            //can always be executed.
            return true;
        }

        /// <inheritdoc/>
        public void Execute(object? parameter)
        {
            //logout
            _userManager.Logger.Logout();

            //navigate to login page.
            _navigationManager.NavigateTo<ViewModelLogin>();
        }
    }
}
