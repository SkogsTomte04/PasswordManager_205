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
using WpfApp1.scripts;

namespace PWMProject
{
    /// <summary>
    /// Interaction logic for accountdetails.xaml
    /// </summary>
    public partial class accountdetails : Window
    {
        private Credential _credential; 
        private ActiveUser _activeUser;
        public accountdetails(Credential credential, ActiveUser user)
        {
            InitializeComponent();
            this._credential = credential;
            this._activeUser = user;

            ServiceNameBox.Text = _credential.ServiceName;
            FullUrl.Text = _credential.ServiceName;
            UsernameTextBox.Text = _credential.Username;
            PasswordBox.Password = "XXXXXXXXX";
            NotesBox.Text = _credential.Notes;
        }

        private void PasswordVisibility_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void PasswordVisibility_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void updateValues()
        {

        }
    }
}
