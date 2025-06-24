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

namespace WpfApp1.Controls
{
    /// <summary>
    /// Interaction logic for AccountElement.xaml
    /// </summary>
    public partial class AccountElement : UserControl
    {
        public AccountElement()
        {
            InitializeComponent();
        }

        public string SiteName
        {
            get => (string)GetValue(SiteNameProperty);
            set => SetValue(SiteNameProperty, value);
        }
        public static readonly DependencyProperty SiteNameProperty =
        DependencyProperty.Register("SiteName", typeof(string), typeof(AccountElement), new PropertyMetadata(""));

    }
}
