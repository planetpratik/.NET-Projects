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

namespace DOT_Logs
{
    // Created by Pratik Chougule - Final Year B.Tech CST , DOT-SUK - Year 2015-16

    public partial class AcknowledgementsPage : Page
    {
        public AcknowledgementsPage()
        {
            InitializeComponent();
        }
        public void backButtonClicked(object sender, RoutedEventArgs e)
        {
            WelcomePage welcome = new WelcomePage();
            this.NavigationService.Navigate(welcome);
        }
    }
}
