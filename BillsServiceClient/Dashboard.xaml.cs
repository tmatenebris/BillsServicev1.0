using BillsServiceLibrary.Models;
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
using BillsServiceClient.BillsService;
namespace BillsServiceClient
{
    /// <summary>
    /// Logika interakcji dla klasy Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        private static BillsService.BillsServiceClient _client = new BillsService.BillsServiceClient();
        private static int userId;
        private static List<Bills> _items;

        public Dashboard()
        {
            InitializeComponent();
           
        }

        public Dashboard(int uid)
        {
            InitializeComponent();
            userId = uid;
            Loaded += Dashboard_Loaded;
    

        }
        private async void Dashboard_Loaded(object sender, RoutedEventArgs e)
        {
            var serialized = await _client.GetBillsAsync(1);
            _items = await JsonSerializer.DeserializeAsync<List<Bills>>(new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(serialized)));
            Bills.ItemsSource = _items;

        }



        private void Close_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }

        private async void RefreshDatagrid(object sender, RoutedEventArgs e)
        {
            var serialized = await _client.GetBillsAsync(1);
            _items = await JsonSerializer.DeserializeAsync<List<Bills>>(new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(serialized)));
            Bills.ItemsSource = _items;
        }
    }
}
