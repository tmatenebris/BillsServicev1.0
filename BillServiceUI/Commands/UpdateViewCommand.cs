
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BillServiceUI.ViewModels;


namespace BillServiceUI.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private DashboardViewModel viewModel;

        public UpdateViewCommand(DashboardViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "MyBills")
            {
                viewModel.SelectedViewModel = new MyBillsViewModel();
            }
            else if (parameter.ToString() == "MyLiabilities")
            {
                viewModel.SelectedViewModel = new MyLiabilitiesViewModel();
            }
            else if (parameter.ToString() == "User")
            {
                viewModel.SelectedViewModel = new UserViewModel();
            }
        }
    }
}

