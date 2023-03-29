using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddEmployeeViewModel : BaseViewModel
    {
        public ICommand AddEmployeeCommand { get; set; }

        public IEmployees ParentDataContext { get; set; }
        public IEmployeeInput EmployeeInputDataContext { get; set; }

        private IEmployeeAccountDao employeeAccountDao;

        public AddEmployeeViewModel(IEmployeeInput employeeInputDataContext, IEmployeeAccountDao employeeAccountDao)
        {
            EmployeeInputDataContext = employeeInputDataContext;
            this.employeeAccountDao = employeeAccountDao;
            AddEmployeeCommand = new RelayCommand<Window>(AddCommand);
        }

        private void AddCommand(Window inputWindow)
        {
            if (!CheckAllFields()) return;
            EmployeeInputDataContext.TrimAllTexts();
            EmployeeAccount empl = EmployeeInputDataContext.CreateEmployeeInstance();
            ParentDataContext.Add(empl);
            inputWindow.Close();
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
    }
}
