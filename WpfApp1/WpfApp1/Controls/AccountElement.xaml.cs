using PWMProject;
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

namespace WpfApp1.Controls
{
    /// <summary>
    /// Interaction logic for AccountElement.xaml
    /// </summary>
    public partial class AccountElement : UserControl
    {
        Credential _credential;
        ActiveUser _user;
        public AccountElement(Credential credential, ActiveUser activeUser)
        {
            InitializeComponent();
            this._credential = credential;
            _user = activeUser;
            SiteName = credential.ServiceName;
        }

        public string SiteName
        {
            get => (string)GetValue(SiteNameProperty);
            set => SetValue(SiteNameProperty, value);
        }
        public static readonly DependencyProperty SiteNameProperty =
        DependencyProperty.Register("SiteName", typeof(string), typeof(AccountElement), new PropertyMetadata(""));

        private void OpenCredential_button_Click(object sender, RoutedEventArgs e)
        {
            accountdetails accountdetails = new accountdetails(_credential, _user);
            accountdetails.ShowDialog();
        }
    }
}
