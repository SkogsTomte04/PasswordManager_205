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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PWMProject
{
    /// <summary>
    /// Interaction logic for accountverify.xaml
    /// </summary>
    public partial class Accountverify : Window
    {
        public Accountverify()
        {
            InitializeComponent();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // show password
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // hide password
        }
    }
}
