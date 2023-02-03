using Aspose.Pdf;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace BillServiceUI
{
    /// <summary>
    /// Logika interakcji dla klasy BillScreen.xaml
    /// </summary>
    public partial class BillScreen : Window
    {
        private static int BillId;
        public BillScreen()
        {

        }
        public BillScreen(int billId)
        {
            InitializeComponent();
            BillId = billId;
            Loaded += BillScreen_OnLoaded;
        }

        public async void BillScreen_OnLoaded(object sender, RoutedEventArgs e)
        {
            using(BillsService.BillsServiceClient _client = new BillsService.BillsServiceClient())
            {
                //pdfViewer.Visibility = Visibility.Hidden;
                var inputStream = await _client.GetPdfAsync(BillId);
                string path = ConfigurationManager.AppSettings["FolderPath"] + BillId.ToString() + ".pdf";
                MessageBox.Show(path);
                using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(inputStream, 0, inputStream.Length);

                }
                //pdfViewer.Source = new Uri(path);
                //pdfViewer.UpdateWindowPos();
                //pdfViewer.Visibility = Visibility.Visible;
               Process.Start(path);
            }
        }
    }
}
