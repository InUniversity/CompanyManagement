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

        public EmployeeInputViewModel EmployeeInputDataContext { get; }
        private Action<Employee> submitObjectAction;

        private EmployeeDao employeeDao = new EmployeeDao();

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
            AlertDialogService dialog = new AlertDialogService(
                "Thêm nhân viên", 
                "Bạn chắc chắn muốn thêm nhân viên !",
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
            if (!EmployeeInputDataContext.CheckAllFields())
                return false;
            if (employeeDao.SearchByID(EmployeeInputDataContext.ID) != null)
            {
                EmployeeInputDataContext.ErrorMessage = Utils.EXIST_ID_MESSAGE;
                return false;
            }
            if (employeeDao.SearchByIdentifyCard(EmployeeInputDataContext.IdentifyCard) != null)
            {
                EmployeeInputDataContext.ErrorMessage = Utils.EXIST_IDENTIFY_CARD_MESSAGE;
                return false;
            }
            if (employeeDao.SearchByPhoneNumber(EmployeeInputDataContext.PhoneNumber) != null)
            {
                EmployeeInputDataContext.ErrorMessage = Utils.EXIST_PHONE_NUMBER_MESSAGE;
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
