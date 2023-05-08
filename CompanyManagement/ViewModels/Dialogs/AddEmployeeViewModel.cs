using System;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddEmployeeViewModel : BaseViewModel, IInputViewModel<Employee>
    {
        public ICommand AddEmployeeCommand { get; }

        public EmployeeInputViewModel EmployeeInputDataContext { get; set; }
        private Action<Employee> submitObjectAction;

        private EmployeesDao employeesDao = new EmployeesDao();

        public AddEmployeeViewModel()
        {
            EmployeeInputDataContext = new EmployeeInputViewModel();
            AddEmployeeCommand = new RelayCommand<Window>(AddCommand);
        }

        private void AddCommand(Window inputWindow)
        {
            EmployeeInputDataContext.TrimAllTexts();
            if (!CheckAllFields()) 
                return;
            var dialog = new AlertDialogService(
                "Thêm nhân viên",
                "Bạn chắc chắn muốn thêm nhân viên !",
                () =>
                {
                    var employee = EmployeeInputDataContext.EmployeeIns;
                    submitObjectAction?.Invoke(employee);
                    inputWindow.Close();
                }, null);
            dialog.Show();
        }

        private bool CheckAllFields()
        {
            if (!EmployeeInputDataContext.CheckAllFields())
                return false;
            if (employeesDao.SearchByID(EmployeeInputDataContext.ID) != null)
            {
                EmployeeInputDataContext.ErrorMessage = Utils.invalidIDMess;
                return false;
            }
            if (employeesDao.SearchByIdentifyCard(EmployeeInputDataContext.IdentifyCard) != null)
            {
                EmployeeInputDataContext.ErrorMessage = Utils.invalidIdentCardMess;
                return false;
            }
            if (employeesDao.SearchByPhoneNumber(EmployeeInputDataContext.PhoneNumber) != null)
            {
                EmployeeInputDataContext.ErrorMessage = Utils.invalidPhoneNoMess;
                return false;
            }
            return true;
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
