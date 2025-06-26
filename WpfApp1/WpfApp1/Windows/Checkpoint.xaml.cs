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
    /// Interaction logic for Checkpoint.xaml
    /// </summary>
    public partial class Checkpoint : Window
    {
        ActiveUser _user;
        List<Credential> _credentials;
        public Checkpoint(ActiveUser user,List<Credential> credentials)
        {
            InitializeComponent();
            this._user = user;
            this._credentials = credentials;

            var evaluated = credentials
            .Select(c =>
            {
                DataHandler handler = new DataHandler();
                byte[] unencryptedbytes = KeyLoader.DecryptToBytes(c.EncryptedPassword, _user.GetDataKey());
                string unencryptedstr = Encoding.UTF8.GetString(unencryptedbytes);
                int score = PasswordHandler.EstimatePasswordStrength(unencryptedstr);
                return new
                {
                    c.ServiceName,
                    c.Username,
                    c.EncryptedPassword,
                    StrengthLabel = StrengthToLabel(score)
                };
            })
            .OrderBy(c => PasswordHandler.EstimatePasswordStrength(c.EncryptedPassword))
            .ToList();

            CredentialListView.ItemsSource = evaluated;
        }
        private static string StrengthToLabel(int score)
        {
            return score switch
            {
                >= 5 => "Very Strong",
                4 => "Strong",
                3 => "Medium",
                2 => "Weak",
                _ => "Very Weak"
            };
        }

    }
}
