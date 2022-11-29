using Authentication.Interfaces;
using Example.Commands;
using Example.Models;
using Example.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Example.ViewModels
{
    /// <summary>
    /// View model for <see cref="Views.Users"/> view.
    /// </summary>
    public sealed class ViewModelUsers : BaseViewModel
    {
        private readonly IUserManager<User, Role> _userManager;

        /// <summary>
        /// Collection of the registered users.
        /// </summary>
        public IEnumerable<ViewModelUser> Users { get; }

        /// <summary>
        /// Gets <see cref="NavigateToCommand{ViewModelNoteBoard}"/>
        /// </summary>
        public ICommand NavigateToNoteBoardCommand { get; }

        /// <summary>
        /// Gets <see cref="Commands.RegisterUserCommand"/>
        /// </summary>
        public ICommand RegisterUser { get; }

        /// <summary>
        /// Gets <see cref="Commands.UnregisterUserCommand"/>
        /// </summary>
        public ICommand UnregisterUserCommand { get; }

        /// <summary>
        /// Creates a new instance of <see cref="ViewModelUsers"/> and initializes the fields.
        /// </summary>
        /// <param name="navigateToNoteBoardCommand"></param>
        /// <param name="navigateToRegisterCommand"></param>
        /// <param name="unregisterUserCommand"></param>
        /// <param name="userManager"></param>
        public ViewModelUsers(NavigateToCommand<ViewModelNoteBoard> navigateToNoteBoardCommand, NavigateToCommand<ViewModelRegister> navigateToRegisterCommand, UnregisterUserCommand unregisterUserCommand, IUserManager<User, Role> userManager)
        {
            //iniialize fields
            _userManager = userManager;

            //initialize list
            Users =_userManager.Registrator.Users.Select(u => new ViewModelUser(u)
            {
                IsAdmin = _userManager.Subscriber.IsSubscribed(u.UserName, "ADMIN"),
                IsUser = _userManager.Subscriber.IsSubscribed(u.UserName, "USER"),
            });

            //initialize commands
            RegisterUser = navigateToRegisterCommand;
            UnregisterUserCommand = unregisterUserCommand;
            NavigateToNoteBoardCommand = navigateToNoteBoardCommand;
            
        }
    }
}
