using BillServiceUI.ViewModels;
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

namespace BillServiceUI
{
    /// <summary>
    /// Logika interakcji dla klasy UI.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
            DataContext = new DashboardViewModel();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            var screen = new MainWindow();
            screen.Show();
            App.appUser = null;
            this.Close();
        }
    }
}
