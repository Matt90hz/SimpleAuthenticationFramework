using Authentication.Interfaces;
using Example.Models;
using Example.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Example.Commands
{
    /// <summary>
    /// Command to login a user.
    /// </summary>
    public sealed class LoginCommand : ICommand
    {
        private readonly IUserManager<User, Role> _userManager;
        private readonly ViewModelMessageBox _viewModelMessageBox;
        private readonly ViewModelNavigationManager _viewModelNavigationManager;

        /// <inheritdoc/>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Creates a new instance of <see cref="LoginCommand"/> and initialize the fields.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="viewModelMessageBox"></param>
        /// <param name="viewModelNavigationManager"></param>
        public LoginCommand(IUserManager<User, Role> userManager,ViewModelMessageBox viewModelMessageBox, ViewModelNavigationManager viewModelNavigationManager)
        {
            _userManager = userManager;
            _viewModelMessageBox = viewModelMessageBox;
            _viewModelNavigationManager = viewModelNavigationManager;
        }

        /// <inheritdoc/>
        public bool CanExecute(object? parameter)
        {
            //can always execute
            return true;
        }

        /// <inheritdoc/>
        public void Execute(object? parameter)
        {
            //get the login credentials thru the command parameter
            if(parameter is not ViewModelLogin viewModelLogin) return;

            //try to login
            if (!_userManager.Logger.Login(viewModelLogin.UserName, viewModelLogin.Password))
            {
                _viewModelMessageBox.Show("Invalid credentials!");
                return;
            }

            //navigate to main page if login succeeds
            _viewModelNavigationManager.NavigateTo<ViewModelNoteBoard>();
        }
    }
}
