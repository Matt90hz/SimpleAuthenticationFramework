using Example.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Example.Commands
{
    /// <summary>
    /// Command that wrap <see cref="ViewModelNavigationManager.NavigateTo{T}"/> method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NavigateToCommand<T> : ICommand where T : BaseViewModel
    {
        private readonly ViewModelNavigationManager _navigationManager;

        /// <inheritdoc/>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Create a ne instance of <see cref="NavigateToCommand{T}"/> and initialize the fields.
        /// </summary>
        /// <param name="navigationManager"></param>
        public NavigateToCommand(ViewModelNavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        /// <inheritdoc/>
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        /// <inheritdoc/>
        public void Execute(object? parameter)
        {
            _navigationManager.NavigateTo<T>();
        }
    }
}
