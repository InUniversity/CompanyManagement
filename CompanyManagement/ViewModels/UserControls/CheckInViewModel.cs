using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Services;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;
using CompanyManagement.Views.Dialogs;

namespace CompanyManagement.ViewModels.UserControls
{
    public class CheckInViewModel : BaseViewModel, IDialogViewModel
    {
        private CheckInOut checkInOut;
        
        public string ID { get => checkInOut.ID; set { checkInOut.ID = value?.Trim(); OnPropertyChanged(); } }
        public string EmployeeID { get => checkInOut.EmployeeID; set { checkInOut.EmployeeID = value?.Trim(); OnPropertyChanged(); } }
        public DateTime CheckInTime { get => checkInOut.CheckInTime; set { checkInOut.CheckInTime = value; OnPropertyChanged(); } }
        public string TaskID { get => checkInOut.TaskID; set { checkInOut.TaskID = value?.Trim(); OnPropertyChanged(); } }
        
        private ICommand CheckInCommand { get; set; }
        
        public IEditDBViewModel ParentDataContext { get; set; }

        public CheckInViewModel()
        {
            CheckInCommand = new RelayCommand<Window>(CheckIn);
        }

        private void CheckIn(Window window)
        {
            AlertDialogService dialog = new AlertDialogService(
                "Check in",
                "Bạn chắc chắn muốn check in không?",
                () =>
                {
                    ParentDataContext.AddToDB(checkInOut);
                }, () => { });
            window.Close();
        }
        
        public void Retrieve(object obj)
        {
            checkInOut = obj as CheckInOut;
        }
    }
}
