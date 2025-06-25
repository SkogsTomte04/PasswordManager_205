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
using WpfApp1.Windows;

namespace PWMProject
{
    /// <summary>
    /// Interaction logic for homepage.xaml
    /// </summary>
    public partial class Homepage : Window
    {
        // stored as [username, id]
        private ActiveUser _user;

        public Homepage(ActiveUser user)
        {
            InitializeComponent();
            this._user = user;

            ApplyCredentials();
        }

        //Get credentals from user and display on homepage
        private void ApplyCredentials()
        {
            User_Name_Txt.Text = $"Username: {_user._username}";
            User_Email_Txt.Text = $"Email: {_user._userId}.gmail.com";
        }


        //Add new credential to user
        private void NewCredential_btn_pressed(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is the redirect");
            NewCredential newCredential = new NewCredential(_user);
            newCredential.Show();
        }

        private void UserIcon_Click(object sender, RoutedEventArgs e)
        {
            UserPopup.IsOpen = !UserPopup.IsOpen;
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            UserPopup.IsOpen = !UserPopup.IsOpen;
        }
    }
}
