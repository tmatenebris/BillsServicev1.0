
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

namespace BillServiceUI.Views
{
    /// <summary>
    /// Logika interakcji dla klasy UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        public UserView()
        {
            InitializeComponent();
            Loaded += UserView_OnLoad;
        }

        public void UserView_OnLoad(object sender, RoutedEventArgs e)
        {
            Username.Text = App.appUser.Username;
            FirstName.Text = App.appUser.FirstName;
            LastName.Text = App.appUser.LastName;
            EMail.Text = App.appUser.Email;
            PhoneNumber.Text = App.appUser.PhoneNumber;
        }
        private async void UpdateUser(object sender, RoutedEventArgs e)
        {
            using(BillsService.BillsServiceClient _client = new BillsService.BillsServiceClient())
            {
                Users newData = new Users();
                newData = App.appUser;
                newData.Username = Username.Text;
                newData.FirstName = FirstName.Text;
                newData.LastName = LastName.Text;
                newData.Email = EMail.Text;
                newData.PhoneNumber = PhoneNumber.Text;
                var serialized = JsonSerializer.Serialize(newData);
                var result = await _client.UpdateUserAsync(serialized);
                if (JsonSerializer.Deserialize<string>(result) == "error") MessageBox.Show("Error");
                else
                {
                    MessageBox.Show("Ok");
                    App.appUser.Username = Username.Text;
                    App.appUser.FirstName = FirstName.Text;
                    App.appUser.LastName = LastName.Text;
                    App.appUser.Email = EMail.Text;
                    App.appUser.PhoneNumber = PhoneNumber.Text;
                }
            }
        }

        private async void DeleteUser(object sender, RoutedEventArgs e)
        {
            using(BillsService.BillsServiceClient _client = new BillsService.BillsServiceClient())
            {
                if (MessageBox.Show("Do you really want delete your account?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var result = await _client.DeleteUserAsync(App.appUser.UserId);
                    if (result == "error") MessageBox.Show("Error");
                    else App.Current.Shutdown();
                }
                    

            }
        }
    }
}
