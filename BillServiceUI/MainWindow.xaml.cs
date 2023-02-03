using BillServiceUI.BillsService;
using BillsServiceUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace BillServiceUI
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            using(BillsService.BillsServiceClient _client = new BillsService.BillsServiceClient())
            {
                var result = _client.Login(LoginBox.Text, PasswordBox.Password.ToString());
                var user = JsonSerializer.Deserialize<Users>(result);
                if (user == null) MessageBox.Show("Error");
                else
                {
                    App.appUser = user;
                    var screen = new Dashboard();
                    screen.Show();
                    this.Close();
                }
            }
      
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            var screen = new RegistrationScreen();
            screen.Show();
            this.Close();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}
