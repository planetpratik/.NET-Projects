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

namespace DOT_Logs
{
    /// <summary>
    /// Interaction logic for AuthenticationPage.xaml
    /// </summary>
    public partial class AuthenticationPage : Page
    {
        public AuthenticationPage()
        {
            InitializeComponent();
        }

        public void loginButtonClicked(object sender, RoutedEventArgs e)
        {
            if (userNameTextBox.Text=="Admin"&&passwordTextBox.Password=="DOTSUK123")
            {
                WelcomePage welcome = new WelcomePage();
                this.NavigationService.Navigate(welcome);
            }
            else
            {
                statusLabel.Content = "Username or Password Incorrect. Please Try Again !";
            }
        }
    }
}
