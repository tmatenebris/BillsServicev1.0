
using BillServiceUI.BillsService;
using BillsServiceUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
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

namespace BillServiceUI.Views
{
    /// <summary>
    /// Logika interakcji dla klasy MyBillsView.xaml
    /// </summary>
    public partial class MyLiabilitiesView : UserControl
    {
        ObservableCollection<Bills> bills = new ObservableCollection<Bills>();
        public MyLiabilitiesView()
        {
            InitializeComponent();
            Loaded += MyLiabilitiesView_OnLoaded;
        }

        private async void MyLiabilitiesView_OnLoaded(object sender, RoutedEventArgs e)
        {
            ProgressBar.Visibility = Visibility.Visible;
            using (BillsService.BillsServiceClient client = new BillsService.BillsServiceClient())
            {

                var data = await client.GetLiabilitiesAsync(App.appUser.UserId);
                var deserialized = await JsonSerializer.DeserializeAsync<List<Bills>>(new MemoryStream(Encoding.UTF8.GetBytes(data)));
                bills = new ObservableCollection<Bills>(deserialized);

            }

            ProgressBar.Visibility = Visibility.Collapsed;
            billsDataGrid.ItemsSource = bills;
        }

        private async void Row_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var clicked = billsDataGrid.SelectedItem as Bills;
            if (clicked != null)
            {
                using (BillsService.BillsServiceClient client = new BillsService.BillsServiceClient())
                {

                    var inputStream = await client.GetPdfAsync(clicked.BillId);
                    string path = ConfigurationManager.AppSettings["FolderPath"] + clicked.BillId.ToString() + ".pdf";
                    using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                    {
                        fs.Write(inputStream, 0, inputStream.Length);

                    }

                    Process.Start(path);
                }
            }
        }
    }
}
