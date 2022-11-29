using Authentication.Extensions;
using Example.Models;
using Example.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Example
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //dependency injection configuration
            var serviceProvider = new ServiceCollection()
                //register view models
                .AddSingleton<ViewModels.ViewModelMainWindow>()
                .AddSingleton<ViewModels.ViewModelNavigationManager>()
                .AddSingleton<ViewModels.ViewModelMessageBox>()
                .AddTransient<ViewModels.ViewModelLogin>()
                .AddTransient<ViewModels.ViewModelNoteBoard>()
                .AddTransient<ViewModels.ViewModelUsers>()
                .AddTransient<ViewModels.ViewModelRegister>()
                //register commands
                .AddTransient<Commands.LoginCommand>()
                .AddTransient<Commands.LogoutCommand>()
                .AddTransient<Commands.AddNoteCommand>()
                .AddTransient<Commands.RegisterUserCommand>()
                .AddTransient<Commands.UnregisterUserCommand>()
                .AddTransient<Commands.NavigateToCommand<ViewModels.ViewModelLogin>>()
                .AddTransient<Commands.NavigateToCommand<ViewModels.ViewModelUsers>>()
                .AddTransient<Commands.NavigateToCommand<ViewModels.ViewModelRegister>>()
                .AddTransient<Commands.NavigateToCommand<ViewModels.ViewModelNoteBoard>>()
                //register services
                .AddSingleton<Services.INoteService, Services.NoteService>()
                .AddUserManagerWithDbContextStore();

            //start the application
            new MainWindow() { DataContext = serviceProvider.BuildServiceProvider().GetRequiredService<ViewModels.ViewModelMainWindow>() }
                .Show();
        }
                 
    }

    /// <summary>
    /// Examples of how configure <see cref="Authentication.UserManager{TUser, TRole}"/> through dipendency injection.
    /// </summary>
    public static partial class ExamplesOfUserManagementConfigurations
    {
        /// <summary>
        /// Shows the simplest way to configure <see cref="Authentication.UserManager{TUser, TRole}"/> to use a database as repository.
        /// </summary>
        /// <remarks>
        /// Are added to <see cref="IServiceCollection"/>: <see cref="Authentication.Interfaces.IUserManager{TUser, TRole}"/> implemented with <see cref="Authentication.UserManager{TUser, TRole}"/> and
        /// <see cref="IDbContextFactory{TContext}"/> that is required for <see cref="Authentication.Stores.DbContextStore.DbContextStore{TUser, TRole, TDbContext}"/> to work.
        /// </remarks>
        /// <param name="services"></param>
        /// <returns>
        /// The same service collection so that multiple calls can be chained.
        /// </returns>
        public static IServiceCollection AddUserManagerWithDbContextStore(this IServiceCollection services)
        {
            //add the framework to the services
            services.AddUserManager<User, Role, AppDbContext>();
            
            //create the factory needed for the framework to work
            services.AddDbContextFactory<AppDbContext>(opt =>
            {
                opt.UseSqlite("Datasource=NoteBoardAppDataBase");
            });
        
            return services;
        }

        /// <summary>
        /// Shows how to add <see cref="Authentication.Interfaces.IUserManager{TUser, TRole}"/> to the services using an in memory database.
        /// This configuration is useful only for testing scenarios.
        /// </summary>
        /// <param name="services"></param>
        /// <returns>
        /// The same service collection so that multiple calls can be chained.
        /// </returns>
        public static IServiceCollection AddUserManagerInMemory(this IServiceCollection services)
        {
            //The in memory storage is default no need of further configuarations
            services.AddUserManager<User, Role>();

            return services;
        }

        /// <summary>
        /// Shows an example of how customize <see cref="Authentication.UserManager{TUser, TRole}"/> with a custom component.
        /// In this case a custom <see cref="Authentication.Interfaces.IStore{TUser, TRole}"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <returns>
        /// The same service collection so that multiple calls can be chained.
        /// </returns>
        public static IServiceCollection AddCustomUserManager(this IServiceCollection services)
        {
            services.AddUserManager<User, Role>((services, builder) =>
            {
                builder.AddStore(services.GetRequiredService<MyCustomStore>());
            });

            return services;
        }

    }

}
