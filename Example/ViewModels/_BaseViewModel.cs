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
    /// Base class for the view models.
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Calling this method will force the update of the binding to the specified property.
        /// </summary>
        /// <param name="propName"></param>
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
