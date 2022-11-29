using Authentication.Interfaces;
using Example.Commands;
using Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Example.ViewModels
{
    /// <summary>
    /// View model for <see cref="Views.Login"/> view.
    /// </summary>
    public sealed class ViewModelLogin : BaseViewModel
    {
        /// <summary>
        /// Small explaining of what to do.
        /// </summary>
        public string Tutorial => 
            "Try one to login as one of the users to see how the interface change based on roles.\n" +
            "\n" +
            "USER\t\tPASSWORD\t\tROLE\n" +
            "--------------------------------------------------------------\n" +
            "admin\t\tadmin!01\t\tADMIN\n" +
            "Pam51\t\tpampam!23\t\tUSER\n" +
            "Carl101\t\tsancarl%12\t\tUSER\n" +
            "JohnDear56\tjohnthabest\t\tGUEST\n";

        /// <summary>
        /// UserName.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// <see cref="LoginCommand"/>
        /// </summary>
        public ICommand LoginCommand { get; }

        /// <summary>
        /// Creates a new instance of <see cref="ViewModelLogin"/> and initialize the fields.
        /// </summary>
        /// <param name="loginCommand"></param>
        public ViewModelLogin(LoginCommand loginCommand)
        {
            LoginCommand = loginCommand;
        }

    }
}
