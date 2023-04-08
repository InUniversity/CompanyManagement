using CompanyManagement.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AlertDialogViewModel : BaseViewModel
    {
        private string tile = "Confirmation";
        public string Tile { get => tile; set { tile = value; OnPropertyChanged(); } }

        private string message;
        public string Message { get => message; set { message = value; OnPropertyChanged(); } }

        private bool yesSelection = false;
        public bool YesSelection { get => yesSelection; set { yesSelection = value; } }

        public ICommand CloseCommand { get; set; }
        public ICommand YesCommand { get; set; }

        public AlertDialogViewModel() 
        {
            CloseCommand = new RelayCommand<Window>(ExecuteCloseCommand);
            YesCommand = new RelayCommand<Window>(ExecuteYesCommand);
        }

        private void ExecuteYesCommand(Window window)
        {
            YesSelection = true;
            window.Close();
        }

        private void ExecuteCloseCommand(Window window)
        {
            window.Close(); 
        }
    }
}
