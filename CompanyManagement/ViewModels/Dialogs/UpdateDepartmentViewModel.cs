using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.UserControls;
using System.Windows.Input;
using System.Windows;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateDepartmentViewModel : BaseViewModel
    {

        public ICommand UpdateDepartmentCommand { get; set; }

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
            // ParentDataContext.UpdateToDB(department);
            inputWindow.Close();
        }

        public void Retrieve(object department)
        {
            DepartmentInputDataContext.Receive(department as Department);
        }
    }
}