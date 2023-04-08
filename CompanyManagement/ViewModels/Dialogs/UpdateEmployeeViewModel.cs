using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.Views.Dialogs;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateEmployeeViewModel : BaseViewModel, IDialogViewModel
    {
        
        public ICommand UpdateEmployeeCommand { get; }

        public IEditDBViewModel ParentDataContext { get; set; }
        public IEmployeeInput EmployeeInputDataContext { get; }

        public UpdateEmployeeViewModel()
        {
            EmployeeInputDataContext = new EmployeeInputViewModel();
            UpdateEmployeeCommand = new RelayCommand<Window>(UpdateCommand);
        }

        private void UpdateCommand(Window inputWindow)
        {
            if (!CheckAllFields()) return;
            AlertDialog alertDialog = new AlertDialog();
            ((AlertDialogViewModel)alertDialog.DataContext).Message = "Bạn chắc chắn muốn \n cập nhật dữ liệu nhân viên !";
            alertDialog.ShowDialog();
            if (((AlertDialogViewModel)alertDialog.DataContext).YesSelection)
            {
                EmployeeInputDataContext.TrimAllTexts();
                Employee empl = EmployeeInputDataContext.CreateEmployeeInstance();
                ParentDataContext.UpdateToDB(empl);
            }            
            inputWindow.Close();
        }

        private bool CheckAllFields()
        {
            return EmployeeInputDataContext.CheckAllFields();
        }
        
        public void Retrieve(object employee)
        {
            EmployeeInputDataContext.Retrieve(employee as Employee);
        }
    }
}
