using System;
using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Services;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateEmployeeViewModel : BaseViewModel, IInputViewModel<Employee>
    {
        public ICommand UpdateEmployeeCommand { get; private set; }
        public ICommand CloseDialogCommand { get; private set; }

        public EmployeeInputViewModel EmployeeInputDataContext { get; }

        private Action<Employee> submitObjectAction;

        public UpdateEmployeeViewModel()
        {
            EmployeeInputDataContext = new EmployeeInputViewModel();
            SetCommands();
        }

        private void SetCommands()
        {
            UpdateEmployeeCommand = new RelayCommand<Window>(UpdateCommand);
            CloseDialogCommand = new RelayCommand<Window>(CloseCommand);
        }

        private void CloseCommand(Window window)
        {
            window.Close();
        }

        private void UpdateCommand(Window inputWindow)
        {
            EmployeeInputDataContext.TrimAllTexts();
            if (!CheckAllFields()) return;
            var dialog = new AlertDialogService(
                "Cập nhật nhân viên", 
                "Bạn chắc chắn muốn cập nhật nhân viên !", 
                () => 
                { 
                    Employee empl = EmployeeInputDataContext.EmployeeIns; 
                    submitObjectAction?.Invoke(empl); 
                    inputWindow.Close(); 
                }, null);
            dialog.Show();              
        }

        private bool CheckAllFields()
        {
            return EmployeeInputDataContext.CheckAllFields();
        }

        public void ReceiveObject(Employee employee)
        {
            EmployeeInputDataContext.EmployeeIns = employee;
        }

        public void ReceiveSubmitAction(Action<Employee> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
