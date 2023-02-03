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

namespace BillServiceUI.Views
{
    /// <summary>
    /// Logika interakcji dla klasy SplashScreenView.xaml
    /// </summary>
    public partial class SplashScreenView : UserControl
    {
        public SplashScreenView()
        {
            InitializeComponent();
            Hello.Text = "Hello " + App.appUser.FirstName + " " + App.appUser.LastName + "!";
        }
    }
}
