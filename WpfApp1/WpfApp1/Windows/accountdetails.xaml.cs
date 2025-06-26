using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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
using System.Xml.Linq;
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
            UsernameTextBox.Text = _credential.Username;
            GetPassword();
            noteTxtbox.Text = _credential.Notes;

            created_date.Text = _credential.DateCreated.ToString();
            updated_date.Text = _credential.DateUpdated.ToString();
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
        private void GetPassword()
        {
            DataHandler handler = new DataHandler();
            byte[] unencryptedbytes = KeyLoader.DecryptToBytes(_credential.EncryptedPassword, _activeUser.GetDataKey());
            string unencryptedstr = Encoding.UTF8.GetString(unencryptedbytes);

            PasswordBox.Password = unencryptedstr;
        }
        private void Delete_button_Click(object sender, RoutedEventArgs e)
        {
            // confirmation
            MessageBoxResult confirm = MessageBox.Show($"Are you sure you want to delete credentials for {_credential.ServiceName} id:{_credential.Id}?","Confirm deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(confirm == MessageBoxResult.Yes)
            {
                DataHandler dataHandler = new DataHandler();
                dataHandler.DeleteCredential(_activeUser._userId, _credential.Id);
            }
        }


        private void Edit_button_Checked(object sender, RoutedEventArgs e)
        {
            ServiceNameBox.IsEnabled = true;
            emailTxtBox.IsEnabled = true;
            UsernameTextBox.IsEnabled = true;
            PasswordBox.IsEnabled = true;
            PasswordVisible.IsEnabled = true;
            noteTxtbox.IsEnabled = true;


        }

        private void Edit_button_Unchecked(object sender, RoutedEventArgs e)
        {
            DataHandler dataHandler = new DataHandler();
            ServiceNameBox.IsEnabled = false;
            emailTxtBox.IsEnabled = false;
            UsernameTextBox.IsEnabled = false;
            PasswordBox.IsEnabled = false;
            PasswordVisible.IsEnabled = false;
            noteTxtbox.IsEnabled = false;
            string encryptedpass = KeyLoader.Encrypt(System.Text.Encoding.UTF8.GetBytes(PasswordBox.Password), _activeUser.GetDataKey());

            _credential.ServiceName = ServiceNameBox.Text;
            _credential.Username = UsernameTextBox.Text;
            _credential.Email = emailTxtBox.Text;
            _credential.Notes = noteTxtbox.Text;
            _credential.EncryptedPassword = encryptedpass;

            dataHandler.UpdateCredential(_activeUser._userId, _credential.Id,_credential.ServiceName,_credential.Username,_credential.Email,_credential.EncryptedPassword,_credential.Notes);
        }

        private void Copy_pass_btn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(PasswordBox.Password))
            {
                Clipboard.SetText(PasswordBox.Password);
            }
        }

        /// dont worry about it
        

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void toggle_password_vis_Checked(object sender, RoutedEventArgs e)
        {
            PasswordVisible.Text = PasswordBox.Password;
            PasswordBox.Visibility = Visibility.Collapsed;
            PasswordVisible.Visibility = Visibility.Visible;
        }

        private void toggle_password_vis_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = PasswordVisible.Text;
            PasswordBox.Visibility = Visibility.Visible;
            PasswordVisible.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordVisible.Visibility == Visibility.Collapsed)
            {
                PasswordVisible.Text = PasswordBox.Password;
            }
        }

        private void PasswordVisible_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PasswordBox.Visibility == Visibility.Collapsed)
            {
                PasswordBox.Password = PasswordVisible.Text;
            }
        }
    }
}
