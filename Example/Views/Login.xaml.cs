using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Example.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();

            //HORRIBLE DO NOT DO THIS AT HOME
            //A nice walk around is create a custom control that have a password box inside but expose a Password dependency property.
            //If you are really concerned about safety you need a more robust strategy.
            stupidPasswordBox.PasswordChanged += (o, e) =>
            {
                ((ViewModels.ViewModelLogin)DataContext).Password = stupidPasswordBox.Password;
            };
        }
    }
}
