using Authentication.Interfaces;
using Example.Models;
using Example.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Example.Commands
{
    /// <summary>
    /// Command to register users.
    /// </summary>
    public class RegisterUserCommand : ICommand
    {
        private readonly IUserManager<User, Role> _userManager;
        private readonly ViewModelMessageBox _viewModelMessageBox;
        private readonly ViewModelNavigationManager _viewModelNavigationManager;

        /// <inheritdoc/>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Creates a new instance of <see cref="RegisterUserCommand"/> and initializes the fields.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="viewModelMessageBox"></param>
        /// <param name="viewModelNavigationManager"></param>
        public RegisterUserCommand(IUserManager<User, Role> userManager, ViewModelMessageBox viewModelMessageBox, ViewModelNavigationManager viewModelNavigationManager)
        {
            _userManager = userManager;
            _viewModelMessageBox = viewModelMessageBox;
            _viewModelNavigationManager = viewModelNavigationManager;
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
            //get the user properties values thru the command parameter
            if (parameter is not ViewModelRegister viewModelRegister) return;

            //set the user properties
            var user = new User
            {
                UserName = viewModelRegister.UserName,
                Name = viewModelRegister.Name,
                Surname = viewModelRegister.Surname
            };

            //try to register the user
            try
            {
                _userManager.Registrator.Register(user, viewModelRegister.Password);

                if (viewModelRegister.IsAdmin) _userManager.Subscriber.Subscribe("ADMIN", user.UserName);
                if (viewModelRegister.IsUser) _userManager.Subscriber.Subscribe("USER", user.UserName);
                _userManager.Subscriber.Subscribe("GUEST", user.UserName);
            }
            catch
            {
                //notify in case os failure
                _viewModelMessageBox.Show("Registration failed!");
                return;
            }

            //navigate to users management page
            _viewModelNavigationManager.NavigateTo<ViewModelUsers>();

        }
    }
}
