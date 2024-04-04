using Authentication.Interfaces;
using Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Repository
{
    /// <summary>
    /// Custom store to give an example of customization.
    /// </summary>
    /// <remarks>
    /// Not implementer it will not work. Created just for demonstration purpose.
    /// </remarks>
    public class MyCustomStore : IStore<User, Role>
    {
        public void DeleteRole(string roleKey)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRoleAsync(string roleKey)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string userName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public void Detach(string userName, string roleKey)
        {
            throw new NotImplementedException();
        }

        public Task DetachAsync(string userName, string roleKey)
        {
            throw new NotImplementedException();
        }

        public Role? FindRole(string roleKey)
        {
            throw new NotImplementedException();
        }

        public Task<Role?> FindRoleAsync(string roleKey)
        {
            throw new NotImplementedException();
        }

        public User? FindUser(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<User?> FindUserAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Role>> GetRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsSubscribed(string userName, params string[] roleKeys)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsSubscribedAsync(string userName, params string[] roleKeys)
        {
            throw new NotImplementedException();
        }

        public void Join(string userName, string roleKey)
        {
            throw new NotImplementedException();
        }

        public Task JoinAsync(string userName, string roleKey)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }

        public void Update(Role role)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
