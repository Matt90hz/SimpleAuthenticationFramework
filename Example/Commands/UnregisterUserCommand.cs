using Authentication.Interfaces;
using Example.Models;
using Example.ViewModels;
using System;
using System.Windows.Input;

namespace Example.Commands
{
    /// <summary>
    /// Command to delete users.
    /// </summary>
    public class UnregisterUserCommand : ICommand
    {
        private readonly IUserManager<User, Role> _userManager;
        private readonly ViewModelMessageBox _viewModelMessageBox;
        private readonly ViewModelNavigationManager _navigationManager;

        /// <inheritdoc/>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Creates a new instance of <see cref="UnregisterUserCommand"/> and initializes the fields.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="viewModelMessageBox"></param>
        /// <param name="navigationManager"></param>
        public UnregisterUserCommand(IUserManager<User, Role> userManager, ViewModelMessageBox viewModelMessageBox, ViewModelNavigationManager navigationManager)
        {
            _userManager = userManager;
            _viewModelMessageBox = viewModelMessageBox;
            _navigationManager = navigationManager;
        }

        /// <inheritdoc/>
        public bool CanExecute(object? parameter)
        {
            //can always be executed
            return true;
        }

        /// <inheritdoc/>
        public void Execute(object? parameter)
        {
            //get the user to delete for command parameters
            if (parameter is not ViewModelUser viewModelUser) 
            { 
                _viewModelMessageBox.Show("Select a user!"); 
                return;
            } 

            //try to delete the user
            try
            {
                _userManager.Registrator.Unregister(viewModelUser.UserName);

                //if the current user is delete logs out then navigate to log in
                if (viewModelUser.UserName == _userManager.Logger.CurrentUser?.UserName)
                {
                    _userManager.Logger.Logout();
                    _navigationManager.NavigateTo<ViewModelLogin>();
                    return;
                }

                //navigate to users management page
                _navigationManager.NavigateTo<ViewModelUsers>();
            }
            catch
            {
                //notify failure
                _viewModelMessageBox.Show("Failed!");
            }

        }
    }
}
