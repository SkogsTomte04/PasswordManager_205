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

namespace WpfApp1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string masterpass = "";
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Login_btn_pressed(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine("test");
        DataHandler handler = new DataHandler();
        handler.AddUser("rand", "rand");

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