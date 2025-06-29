﻿using System;
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
    /// Interaction logic for MasterPasswordConfirmation.xaml
    /// </summary>
    public partial class MasterPasswordConfirmation : Window
    {
        ActiveUser _user; 
        public string EnteredPassword { get; private set; }

        public MasterPasswordConfirmation(ActiveUser user)
        {
            InitializeComponent();
            this._user = user;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            //i need to check its valid 
            EnteredPassword = PasswordInput.Password;
            DialogResult = true;
        }
    }
}
