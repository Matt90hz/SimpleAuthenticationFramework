using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.ViewModels
{
	/// <summary>
	/// View model to display messages to the user.
	/// </summary>
    public sealed class ViewModelMessageBox : BaseViewModel
    {
		private bool _isOpen;
		private string _message = string.Empty;

		/// <summary>
		/// <c>true</c> the message is displayed. <c>false</c> the message is hidden.
		/// </summary>
		public bool IsOpen
		{
			get => _isOpen;
			set 
			{ 
				_isOpen = value;
				OnPropertyChanged();
			}
		}	

		/// <summary>
		/// Message to show.
		/// </summary>
		public string Message
		{
			get => _message;
			set 
			{ 
				_message = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Open the message box and shows the <paramref name="message"/>.
		/// </summary>
		/// <param name="message"></param>
		public void Show(string message)
		{
			Message = message;
			IsOpen = true;
		}

	}
}
