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
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {
        public WelcomePage()
        {
            InitializeComponent();
        }
        private void AddOrUpdateLogButtonClicked(object sender, RoutedEventArgs e)
        {
            AddOrUpdatePage addorupdatepage = new AddOrUpdatePage();
            this.NavigationService.Navigate(addorupdatepage);
        }
        private void ViewEntireLogButtonClicked(object sender, RoutedEventArgs e)
        {
            ViewLogPage viewLog = new ViewLogPage();
            this.NavigationService.Navigate(viewLog);
        }
    }
}
