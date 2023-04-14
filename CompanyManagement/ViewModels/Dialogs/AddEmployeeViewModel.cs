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
        public ICommand AddEmployeeCommand { get; set; }

        public EmployeeInputViewModel EmployeeInputDataContext;
        private Action<Employee> submitObjectAction;

        private EmployeeDao employeeAccountDao;

        public AddEmployeeViewModel()
        {
            EmployeeInputDataContext = new EmployeeInputViewModel();
            employeeAccountDao = new EmployeeDao();
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
                    Employee empl = EmployeeInputDataContext.CreateEmployeeInstance();
                    submitObjectAction?.Invoke(empl);
                    inputWindow.Close();
                }, () => {});
            dialog.Show();
        }

        private bool CheckAllFields()
        {
            if (!EmployeeInputDataContext.CheckAllFields())
                return false;
            if (employeeAccountDao.SearchByID(EmployeeInputDataContext.ID) != null)
            {
                EmployeeInputDataContext.ErrorMessage = Utils.EXIST_ID_MESSAGE;
                return false;
            }
            if (employeeAccountDao.SearchByIdentifyCard(EmployeeInputDataContext.IdentifyCard) != null)
            {
                EmployeeInputDataContext.ErrorMessage = Utils.EXIST_IDENTIFY_CARD_MESSAGE;
                return false;
            }
            if (employeeAccountDao.SearchByPhoneNumber(EmployeeInputDataContext.PhoneNumber) != null)
            {
                EmployeeInputDataContext.ErrorMessage = Utils.EXIST_PHONE_NUMBER_MESSAGE;
                return false;
            }
            return true;
        }

        public void RetrieveObject(Employee employee)
        {
            EmployeeInputDataContext.Retrieve(employee);
        }

        public void RetrieveSubmitAction(Action<Employee> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
