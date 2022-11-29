using Authentication.Interfaces;
using Example.Models;
using Example.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.ViewModels
{
    /// <summary>
    /// View model for <see cref="MainWindow"/> view.
    /// </summary>
    public sealed class ViewModelMainWindow : BaseViewModel
    {
        /// <summary>
        /// Get <see cref="ViewModelNavigationManager"/>.
        /// </summary>
        public ViewModelNavigationManager NavigationManager { get; }

        /// <summary>
        /// Get <see cref="ViewModelMessageBox"/>.
        /// </summary>
        public ViewModelMessageBox MessageBox { get; }

        /// <summary>
        /// Creates a new instance of <see cref="ViewModelMainWindow"/> and initialize the fields.
        /// </summary>
        /// <param name="dbContextFactory"></param>
        /// <param name="userManager"></param>
        /// <param name="navigationManager"></param>
        /// <param name="viewModelMessageBox"></param>
        public ViewModelMainWindow(IDbContextFactory<AppDbContext> dbContextFactory, IUserManager<User, Role> userManager, ViewModelNavigationManager navigationManager, ViewModelMessageBox viewModelMessageBox)
        {
            NavigationManager = navigationManager;
            MessageBox = viewModelMessageBox;

            //migrate database
            using var context = dbContextFactory.CreateDbContext();
            context.Database.Migrate();

            //seed the users
            if (!context.Users.Any()) SeedDatabase(userManager);

            //start from login page
            navigationManager.NavigateTo<ViewModelLogin>();
            
        }

        /// <summary>
        /// Seed the database with few users just to showcase the framework in action.
        /// </summary>
        /// <param name="userManager"></param>
        private static void SeedDatabase(IUserManager<User, Role> userManager)
        {
            userManager.Registrator.Register(new User
            {
                UserName = "admin",
                Name = "admin",
                Surname = ""
            }, "admin!01");

            userManager.Subscriber.Subscribe("ADMIN", "admin");
            userManager.Subscriber.Subscribe("USER", "admin");
            userManager.Subscriber.Subscribe("GUEST", "admin");

            userManager.Registrator.Register(new User
            {
                UserName = "Pam51",
                Name = "Pamela",
                Surname = "Rogers"
            }, "pampam!23");

            userManager.Subscriber.Subscribe("USER", "Pam51");
            userManager.Subscriber.Subscribe("GUEST", "Pam51");

            userManager.Registrator.Register(new User
            {
                UserName = "Carl101",
                Name = "Carlos",
                Surname = "Sanchez"
            }, "sancarl%12");

            userManager.Subscriber.Subscribe("USER", "Carl101");
            userManager.Subscriber.Subscribe("GUEST", "Carl101");

            userManager.Registrator.Register(new User
            {
                UserName = "JohnDear56",
                Name = "John",
                Surname = "Smith"
            }, "johnthabest");

            userManager.Subscriber.Subscribe("GUEST", "JohnDear56");
        }
    }
}
