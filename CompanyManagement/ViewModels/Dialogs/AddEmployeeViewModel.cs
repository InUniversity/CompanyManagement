using System;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.UserControls.Interfaces;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddEmployeeViewModel : BaseViewModel, IDialogViewModel
    {
        
        public ICommand AddEmployeeCommand { get; set; }

        public IEditDBViewModel ParentDataContext { get; set; }
        public IEmployeeInput EmployeeInputDataContext { get; }

        private EmployeeDao employeeAccountDao;

        public AddEmployeeViewModel()
        {
            EmployeeInputDataContext = new EmployeeInputViewModel();
            employeeAccountDao = new EmployeeDao();
            AddEmployeeCommand = new RelayCommand<Window>(AddCommand);
        }

        private void AddCommand(Window inputWindow)
        {
            if (!CheckAllFields()) return;
            EmployeeInputDataContext.TrimAllTexts();
            Employee empl = EmployeeInputDataContext.CreateEmployeeInstance();
            ParentDataContext.AddToDB(empl);
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
        
        public void Retrieve(object employee)
        {
            EmployeeInputDataContext.Retrieve(employee as Employee);
        }
    }
}
