using Authentication.Interfaces;
using Example.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Example.ViewModels
{
    /// <summary>
    /// Rappresentation of <see cref="Models.User"/> for the display.
    /// </summary>
    public sealed class ViewModelUser : BaseViewModel
    {
        private bool _isAdmin;
        private bool _isUser;

        /// <summary>
        /// Data transfer for <see cref="Models.User.UserName"/> property.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Data transfer for <see cref="Models.User.UserName"/> property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Data transfer for <see cref="Models.User.UserName"/> property.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// <c>true</c> if the user is subscribed to "ADMIN" role.
        /// </summary>
        public bool IsAdmin
        {
            get => _isAdmin; 
            set
            {
                _isAdmin = value;
                if (value) IsUser = value;           
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// <c>true</c> if the user is subscribed to "USER" role.
        /// </summary>
        public bool IsUser
        {
            get => _isUser; 
            set
            {
                _isUser = value;
                if(!value) IsAdmin = false;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// <c>true</c> if the user is subscribed to "GUEST" role.
        /// </summary>
        public bool IsGuest => true;

        /// <summary>
        /// Get a readable rappresentation of the roles of the user.
        /// </summary>
        public string RoleDescription => this switch
        {
            { IsAdmin: true } => "Admin",
            { IsUser: true } => "User",
            _ => "Guest"
        };

        /// <summary>
        /// Creates a new instance of <see cref="ViewModelUsers"/> based on <paramref name="user"/> properties.
        /// </summary>
        /// <remarks>
        /// <see cref="IsAdmin"/> and <see cref="IsUser"/> property must be manually setted after creation based on user subcriptions.
        /// This because the roles of the user cannot be inferred from the <see cref="Models.User"/> object.
        /// <para>
        /// In future updates would be nice to have a navigation property to the roles on the user object. 
        /// This will also make possible implicit many-to-many relations users and roles and remove the need of the <see cref="Authentication.Models.Subscription"/> type.
        /// </para>
        /// </remarks>
        /// <param name="user"></param>
        public ViewModelUser(User user)
        {
            UserName = user.UserName;
            Name = user.Name;
            Surname = user.Surname;
        }

    }
}
