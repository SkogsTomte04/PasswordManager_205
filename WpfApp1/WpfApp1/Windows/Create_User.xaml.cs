using PWMProject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Create_User.xaml
    /// </summary>
    public partial class Create_User : Window
    {
        string username = "";
        string password = "";
        string passwordconfirmation = "";
        public Create_User()
        {
            InitializeComponent();
        }

        private void NewUser_btn_pressed(object sender, RoutedEventArgs e)
        {
            //This is currently testing database handling, will work as a normal button once finished
            DataHandler handler = new DataHandler();
            //
            username = UserIdTextBox.Text;
            password = PasswordBox.Password;
            passwordconfirmation = ConfirmPasswordBox.Password;
            
            if(Passwordcheck(username, password, passwordconfirmation))
            {
                //check if user exists
                if (handler.UserExists(username))
                {
                    MessageBox.Show($"User \"{username}\" already exists");
                    return;
                }
                // generate keys and salt for encryption and decryption
                KeyLoader.FirstTimeSetup(username, password);

                // hash the password
                string hash = HashService.ComputetoHash(password);
                handler.AddUser(username, hash); // this adds user based on username and password
                
                handler.PrintAllUsers();

                ActiveUser user = handler.LogInUser(username, password);

                if (user != null)
                {
                    Homepage homepage = new Homepage(user);
                    homepage.Show();
                    this.Close();
                }
            }
            else
            {
                Debug.WriteLine("PasswordCheck returned false");
            }


        }
        private bool Passwordcheck(string usr, string pass, string conpass)
        {
            bool valid = false;
            //check also if user already exists
            string[] str = {usr,pass, conpass};

            foreach (string str2 in str)
            {
                if (str2.Length == 0)
                {
                    MessageBox.Show("Please Fill out all credentials");
                    valid = false;
                    
                    return valid;
                }
            }

            if (pass == conpass)
            {
                valid = true;
            }
            else
            {
                valid = false;
                PasswordBox.Password = "";
                ConfirmPasswordBox.Password = "";
                MessageBox.Show("Password Does not match with confirmation password");
            }

            return valid;
        }
    }
}
