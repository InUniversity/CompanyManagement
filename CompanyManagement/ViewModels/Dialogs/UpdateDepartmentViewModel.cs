using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.UserControls.Interfaces;
using CompanyManagement.ViewModels.UserControls;
using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.Dialogs.Interfaces;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateDepartmentViewModel : BaseViewModel, IDialogViewModel
    {

        public ICommand UpdateDepartmentCommand { get; set; }

        public IEditDBViewModel ParentDataContext { get; set; }
        public IDepartmentInput DepartmentInputDataContext { get; set; }

        public UpdateDepartmentViewModel()
        {
            DepartmentInputDataContext = new DepartmentInputViewModel();
            UpdateDepartmentCommand = new RelayCommand<Window>(UpdateCommand);
        }

        private void UpdateCommand(Window inputWindow)
        {
            DepartmentInputDataContext.TrimAllTexts();
            Department department = DepartmentInputDataContext.CreateDepartmentInstance();
            ParentDataContext.UpdateToDB(department);
            inputWindow.Close();
        }

        public void Retrieve(object department)
        {
            DepartmentInputDataContext.Retrieve(department as Department);
        }
    }
}