using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Utilities;

namespace CompanyManagement.ViewModels
{
    public class AddEmployeeViewModel : BaseViewModel
    {

        public ICommand AddEmployeeCommand { get; set; }

        public IEmployees ParentDataContext { get; set; }
        public EmployeeInputViewModel EmployeeInputDataContext { get; set; }

        private EmployeeDao employeeDao = new EmployeeDao();
        
        public AddEmployeeViewModel()
        {
            EmployeeInputDataContext = new EmployeeInputViewModel();
            AddEmployeeCommand = new RelayCommand<Window>(AddCommand, CheckAllFields);
        }

        private void AddCommand(Window inputWindow)
        {
            EmployeeInputDataContext.TrimAllTexts();
            Employee empl = EmployeeInputDataContext.CreateEmployeeInstance();
            ParentDataContext.Add(empl);
            inputWindow.Close();
        }

        private bool CheckAllFields(object p)
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
