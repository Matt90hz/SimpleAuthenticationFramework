using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.ViewModels
{
    /// <summary>
    /// Is more a service than a view model but I wanted to keep it simple. For the sake of the example is more than fine.
    /// </summary>
    public sealed class ViewModelNavigationManager : BaseViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        private BaseViewModel? _currentViewModel;

        /// <summary>
        /// App title.
        /// </summary>
        public string Title { get; } = "Note Board App";

        /// <summary>
        /// Held the view model currently displayed.
        /// </summary>
        public BaseViewModel? CurrentViewModel
        {
            get => _currentViewModel; 
            private set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="ViewModelNavigationManager"/> and initializes the fields.
        /// </summary>
        /// <param name="serviceProvider"></param>
        public ViewModelNavigationManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Changes the current view model to <typeparamref name="TViewModel"/>.
        /// </summary>
        /// <remarks>
        /// Uses the <see cref="IServiceProvider"/> to get the instance of <typeparamref name="TViewModel"/> to put into <see cref="CurrentViewModel"/>.
        /// </remarks>
        /// <typeparam name="TViewModel"></typeparam>
        public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
        {
            CurrentViewModel = _serviceProvider.GetRequiredService<TViewModel>();
        }

    }
}
