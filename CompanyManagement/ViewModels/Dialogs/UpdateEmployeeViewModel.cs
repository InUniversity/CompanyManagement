using System.Windows.Input;
using System.Windows;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.Dialogs
{
    public interface IUpdateEmployee
    {
        IEmployees ParentDataContext { set; } 
        IEmployeeInput EmployeeInputDataContext { get; } 
    }
    
    public class UpdateEmployeeViewModel : BaseViewModel, IUpdateEmployee
    {
        
        public ICommand UpdateEmployeeCommand { get; }

        public IEmployees ParentDataContext { get; set; }
        public IEmployeeInput EmployeeInputDataContext { get; }

        public UpdateEmployeeViewModel()
        {
            EmployeeInputDataContext = new EmployeeInputViewModel();
            UpdateEmployeeCommand = new RelayCommand<Window>(UpdateCommand);
        }

        private void UpdateCommand(Window inputWindow)
        {
            if (!CheckAllFields()) return;
            EmployeeInputDataContext.TrimAllTexts();
            Employee empl = EmployeeInputDataContext.CreateEmployeeInstance();
            ParentDataContext.Update(empl);
            inputWindow.Close();
        }

        private bool CheckAllFields()
        {
            return EmployeeInputDataContext.CheckAllFields();
        }
    }
}
