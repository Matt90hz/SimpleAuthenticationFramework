using Example.Commands;
using System.Windows.Input;

namespace Example.ViewModels
{
    /// <summary>
    /// View model for <see cref="Views.Register"/> view.
    /// </summary>
    public sealed class ViewModelRegister : BaseViewModel
    {
        private bool _isAdmin;
        private bool _isUser;

        /// <summary>
        /// Data transfer property for <see cref="Models.User.UserName"/>.
        /// </summary>
        public string UserName { get; set; } = "nick name";

        /// <summary>
        /// Data transfer property for <see cref="Models.User.Name"/>.
        /// </summary>
        public string Name { get; set; } = "name";

        /// <summary>
        /// Data transfer property for <see cref="Models.User.Surname"/>.
        /// </summary>
        public string Surname { get; set; } = "surname";

        /// <summary>
        /// Property that feeded to the authentication framework will generate values for <see cref="Models.User.HashedPassword"/> and <see cref="Models.User.Salt"/>.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// If <c>true</c> the user will be subcrided to "ADMIN" role.
        /// </summary>
        /// <remarks>
        /// When <c>true</c> also <see cref="IsUser"/> is turned to <c>true</c>.
        /// </remarks>
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
        /// If <c>true</c> the user will be subcrided to "USER" role.
        /// </summary>
        /// <remarks>
        /// When <c>false</c> also <see cref="IsAdmin"/> is set to <c>false</c>.
        /// </remarks>
        public bool IsUser
        {
            get => _isUser;
            set
            {
                _isUser = value;
                if (!value) IsAdmin = false;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// If <c>true</c> the user will be subcrided to "GUEST" role.
        /// </summary>
        /// <remarks>
        /// Always <c>true</c>. All users created must be at least subscribed to "GUEST" role.
        /// </remarks>
        public bool IsGuest => true;

        /// <summary>
        /// Gets <see cref="Commands.RegisterUserCommand"/>.
        /// </summary>
        public ICommand RegisterUserCommand { get; }

        /// <summary>
        /// Gets <see cref="Commands.NavigateToCommand{ViewModelUsers}"/>.
        /// </summary>
        public ICommand AbortCommand { get; }

        /// <summary>
        /// Creates a new instance of <see cref="ViewModelRegister"/> and initializes the fields.
        /// </summary>
        /// <param name="registerUserCommand"></param>
        /// <param name="navigateToCommand"></param>
        public ViewModelRegister(RegisterUserCommand registerUserCommand, NavigateToCommand<ViewModelUsers> navigateToCommand)
        {
            RegisterUserCommand = registerUserCommand;
            AbortCommand = navigateToCommand;
        }

    }
}
