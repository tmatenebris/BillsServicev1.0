
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BillServiceUI.Commands;
using BillServiceUI.Views;
using BillServiceUI.ViewModels;

namespace BillServiceUI.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel = new SplashScreenViewModel();



        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }


        public DashboardViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);

        }
    }
}
