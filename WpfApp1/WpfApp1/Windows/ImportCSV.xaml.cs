using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
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
using System.Xml.Linq;
using WpfApp1.scripts;

namespace WpfApp1.Windows
{
    /// <summary>
    /// Interaction logic for ImportCSV.xaml
    /// </summary>
    public partial class ImportCSV : Window
    {
        ActiveUser _user;
        public ImportCSV(ActiveUser activeUser)
        {
            InitializeComponent();
            this._user = activeUser;
        }
        private void Window_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0 && System.IO.Path.GetExtension(files[0]).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    e.Effects = DragDropEffects.Copy;
                }
                else
                {
                    e.Effects = DragDropEffects.None;
                }
            }
            e.Handled = true;
        }

        private async void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    string filePath = files[0];
                    await ImportCsvAsync(filePath);
                }
            }
        }

        private async Task ImportCsvAsync(string filePath)
        {
            DataHandler dataHandler = new DataHandler();

            var lines = await File.ReadAllLinesAsync(filePath);
            var importedCredentials = new List<Credential>();

            for (int i = 1; i < lines.Length; i++) // Skip header
            {
                string[] parts = lines[i].Split(',');

                if (parts.Length < 4) continue; // Skip invalid rows

                var cred = new Credential
                {
                    ServiceName = parts[0],
                    Username = parts[2],
                    EncryptedPassword = parts[3],
                    Notes = "", // You could add more fields here if needed
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    Email = parts[2], // If same as username
                                      // Set UserID if needed
                };

                importedCredentials.Add(cred);
            }
            foreach (var cred in importedCredentials)
            {
                string[] temp = { cred.ServiceName, cred.Email, cred.Username, cred.EncryptedPassword, cred.Notes };
                dataHandler.AddCredentials(_user, temp);
            }
             // refresh UI
            
            MessageBox.Show("Credentials imported successfully!");
            this.Close();
        }
    }
}
