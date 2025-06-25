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

namespace PWMProject
{
    /// <summary>
    /// Interaction logic for homepage.xaml
    /// </summary>
    public partial class Homepage : Window
    {
        // stored as [username, id]
        private string[] _usercredentials;

        public Homepage(string[] usercredentials)
        {
            InitializeComponent();
            _usercredentials = usercredentials;

            ApplyCredentials();
        }

        //Get credentals from user and display on homepage
        private void ApplyCredentials()
        {
            User_Name_Txt.Text = $"Username: {_usercredentials[0]}";
            User_Email_Txt.Text = $"Email: {_usercredentials[1]}.gmail.com";
        }


        //Add new credential to user
        private void NewCredential_btn_pressed(object sender, RoutedEventArgs e)
        {
            
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
