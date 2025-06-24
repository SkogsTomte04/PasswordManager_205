using PWMProject;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Text;
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

namespace WpfApp1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string muser = "";
    private string mpass = "";
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Login_btn_pressed(object sender, RoutedEventArgs e)
    {
        //This is currently testing database handling, will work as a normal button once finished
        DataHandler handler = new DataHandler();
        //handler.AddUser("skogstomte101", "Buster05"); // this adds user based on username and password
        muser = UserIdTextBox.Text;
        mpass = PasswordBox.Password;

        //Debug_credentials(muser, mpass);
        handler.PrintAllUsers();

        string[] userCredentials = handler.LogInUser(muser, mpass);

        if(userCredentials != null)
        {
            Homepage homepage = new Homepage(userCredentials);
            homepage.Show();
            this.Close();
        }
        
        


    }

    private void NewUser_btn_clicked(object sender, RoutedEventArgs e)
    {
        Create_User create_User = new Create_User();
        create_User.Show();

        this.Close();

    }

    private void Debug_credentials(string x, string y)
    {
        Debug.WriteLine("Username " + x);
        Debug.WriteLine("Password " + y);
    }

    private string Validate_input(string str)
    {
        return str;
    }



    /*
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string input = txtboxIn.Text;
            bool passCheck = false;

            HashService hash = new HashService();
            input = hash.ComputetoHash(input);

            if(masterpass.Length < 1)
            {
                masterpass = input;
            }
            else
            {
                passCheck = hash.CheckHash(input,masterpass);
            }


            HashCheck.Text = passCheck.ToString();

            txtboxOut.Text = input;
        }*/

    /*    private void ButtonGenerate_Click(object sender, RoutedEventArgs e)
        {
            PasswordHandler password = new PasswordHandler();
            PassOutput.Text = password.GenerateStrongPassword();
        }

        private void Hash_nav(object sender, RoutedEventArgs e)
        {
            PasswordCheck.Visibility = Visibility.Visible;
            PasswordGen.Visibility = Visibility.Hidden;
        }

        private void Pass_nav(object sender, RoutedEventArgs e)
        {
            PasswordCheck.Visibility = Visibility.Hidden;
            PasswordGen.Visibility = Visibility.Visible; ;

        }*/
}