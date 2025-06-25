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
using System.Windows.Shapes;
using WpfApp1.scripts;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Interaction logic for NewCredential.xaml
    /// </summary>
    public partial class NewCredential : Window
    {
        private string _domain = "";
        private string _email = "";
        private string _username = "";
        private string _password = "";
        private string _note = "";
        public NewCredential()
        {
            InitializeComponent();
        }

        private void GeneratePassButton_Click(object sender, RoutedEventArgs e)
        {

            string strong_pass = PasswordHandler.GenerateStrongPassword();

            // goal right here
            PasswordBox.Password = strong_pass;
            PasswordVisible.Text = strong_pass;

        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            // save info to database
            _domain = domainTxtBox.Text;
            _email = emailTxtBox.Text;
            _username = UsernameTextBox.Text;
            _password = PasswordBox.Password;
            _note = noteTx
        }

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
