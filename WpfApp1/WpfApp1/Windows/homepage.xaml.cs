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
using WpfApp1.Controls;
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
        List<Credential> _credentials;

        public Homepage(ActiveUser user)
        {
            InitializeComponent();
            this._user = user;

            ApplyCredentials();
            GetCredentials();
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
            NewCredential newCredential = new NewCredential(_user);
            newCredential.ShowDialog();
            GetCredentials();
        }

        private void GetCredentials()
        {
            DataHandler handler = new DataHandler();
            List<Credential> credentials = handler.GetCredentials(_user);
            _credentials = credentials;
            CredentialsContainer.Children.Clear();

            foreach (Credential credential in credentials)
            {
                AccountElement accountElement = new AccountElement(credential, _user);
                CredentialsContainer.Children.Add(accountElement);
                
            }
        }
        private void UserIcon_Click(object sender, RoutedEventArgs e)
        {
            UserPopup.IsOpen = !UserPopup.IsOpen;
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Environment.ProcessPath);
            Application.Current.Shutdown();
        }

        private void ImportPasswords_button_Click(object sender, RoutedEventArgs e)
        {
            ImportCSV importCSV = new ImportCSV(_user);
            importCSV.ShowDialog();
            GetCredentials();
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (SearchBox.Text.Length > 0)
            {
                CredentialsContainer.Children.Clear();
                foreach (Credential credential in _credentials)
                {
                    if (credential.ServiceName.ToLower().Contains(SearchBox.Text.ToLower()) || credential.Username.ToLower().Contains(SearchBox.Text.ToLower()) || credential.Email.ToLower().Contains(SearchBox.Text.ToLower()))
                    {
                        AccountElement accountElement = new AccountElement(credential, _user);
                        CredentialsContainer.Children.Add(accountElement);
                    }
                }

            }
            else { GetCredentials(); }

        }

        private void Checkpoint_btn(object sender, RoutedEventArgs e)
        {
            Checkpoint checkpoint = new Checkpoint(_user,_credentials);
            checkpoint.ShowDialog();
        }
    }
}
