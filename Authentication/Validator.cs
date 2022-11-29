using Authentication.Models;
using Authentication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    /// <summary>
    /// Implementation of <see cref="IValidator"/>
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    /// <typeparam name="TRole"></typeparam>
    public class Validator<TUser, TRole> : IValidator
        where TUser : IUser
        where TRole : IRole
    {
        private readonly IStore<TUser, TRole> _store;

        /// <summary>
        /// Creates a new instance of <see cref="Validator{TUser, TRole}"/> and initialize the fields.
        /// </summary>
        /// <param name="store"></param>
        public Validator(IStore<TUser, TRole> store)
        {
            _store = store;
        }

        /// <inheritdoc/>
        public virtual bool IsValidEmail(string email)
        {
            try
            {
                _ = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public virtual bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;

            if (password.Length < 6) return false;
  
            return true;
            
        }

        /// <inheritdoc/>
        public virtual bool IsValidRoleKey(string roleKey)
        {
            if (string.IsNullOrWhiteSpace(roleKey)) return false;
            
            if (roleKey.Any(c => char.IsSymbol(c))) return false;

            if (_store.FindRole(roleKey) is not null) return false;

            return true;          
        }

        /// <inheritdoc/>
        public virtual bool IsValidUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) return false;

            if (userName.Any(c => char.IsSymbol(c))) return false;
            
            if (_store.FindUser(userName) is not null) return false;

            return true;
            
        }
    }
}
