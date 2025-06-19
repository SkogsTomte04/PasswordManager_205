using System.Configuration;
using System.Data;
using System.Windows;
using System.IO;

namespace WpfApp1;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Set DataDirectory to your project root + \Data
        string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data");
        AppDomain.CurrentDomain.SetData("DataDirectory", dataDir);
    }
}

