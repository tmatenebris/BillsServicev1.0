
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
using System.Windows.Shapes;

namespace BillServiceUI
{
    /// <summary>
    /// Logika interakcji dla klasy RegistrationScreen.xaml
    /// </summary>
    public partial class RegistrationScreen : Window
    {
        public RegistrationScreen()
        {
            InitializeComponent();
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            using (BillsService.BillsServiceClient _client = new BillsService.BillsServiceClient())
            {
              var result = _client.Register(Username.Text, FirstName.Text, LastName.Text, Password.Password.ToString(), EMail.Text, PhoneNumber.Text);
                if (JsonSerializer.Deserialize<string>(result) == "error") MessageBox.Show("Error");
                else MessageBox.Show("Success");
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            var screen = new MainWindow();
            screen.Show();
            this.Close();
           
        }
    }
}
