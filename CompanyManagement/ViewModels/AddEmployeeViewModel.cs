using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.Utilities;

namespace CompanyManagement.ViewModels
{
    public class AddEmployeeViewModel : BaseViewModel
    {
        public ICommand AddEmployeeCommand { get; set; }

        public IEmployees ParentDataContext { get; set; }
        public IEmployeeInput EmployeeInputDataContext { get; set; }

        private IEmployeeDao employeeDao;
        
        public AddEmployeeViewModel(IEmployeeInput employeeInputDataContext, IEmployeeDao employeeDao)
        {
            EmployeeInputDataContext = employeeInputDataContext;
            this.employeeDao = employeeDao;
            AddEmployeeCommand = new RelayCommand<Window>(AddCommand);
        }

        private void AddCommand(Window inputWindow)
        {
            if (!CheckAllFields()) return;
            EmployeeInputDataContext.TrimAllTexts();
            Employee empl = EmployeeInputDataContext.CreateEmployeeInstance();
            ParentDataContext.Add(empl);
            inputWindow.Close();
        }

        private bool CheckAllFields()
        {
            if (!EmployeeInputDataContext.CheckAllFields())
                return false;
            if(employeeDao.SearchByID(EmployeeInputDataContext.ID) != null)
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
    }
}
