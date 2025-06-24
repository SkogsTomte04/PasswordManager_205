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
        private string[] _usercredentials;

        public Homepage(string[] usercredentials)
        {
            InitializeComponent();
            _usercredentials = usercredentials;
        }

        //Get credentals from user and display on homepage
        private void GetCredentials()
        {

        }


        //Add new credential to user
        private void NewCredential_btn_pressed(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
