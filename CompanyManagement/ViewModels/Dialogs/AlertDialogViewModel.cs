using CompanyManagement.ViewModels.Base;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AlertDialogViewModel : BaseViewModel
    {
        private string title = "";
        public string Title { get => title; set { title = value; OnPropertyChanged(); } }

        private string message = "";
        public string Message { get => message; set { message = value; OnPropertyChanged(); } }

        public ICommand YesCommand { get; set; }
        public ICommand NoCommand { get; set; }
    }
}
