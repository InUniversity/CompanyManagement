using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CompanyManagement.Dialogs;
using CompanyManagement.Database.Interfaces;

namespace CompanyManagement.ViewModels
{
    public interface IEmployees
    {
        void Add(EmployeeAccount employeeAccount);
        void Update(EmployeeAccount employeeAccount);
    }
    
    public class EmployeesViewModel : BaseViewModel, IEmployees
    {

        private ObservableCollection<EmployeeAccount> employees;
        public ObservableCollection<EmployeeAccount> Employees { get => employees; set { employees = value; OnPropertyChanged(); } }

        public ICommand OpenInputDialogCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand UpdateEmployeeCommand { get; set; }

        private IEmployeeAccountDao employeeAccountDao;

        public EmployeesViewModel(IEmployeeAccountDao employeeAccountDao)
        {
            this.employeeAccountDao = employeeAccountDao;
            LoadEmployees();
            SetCommands();
        }

        private void LoadEmployees()
        {
            Employees = new ObservableCollection<EmployeeAccount>(employeeAccountDao.GetAll());
        }

        private void SetCommands()
        {
            OpenInputDialogCommand = new RelayCommand<object>(OpenAddEmployeeDialog);
            DeleteEmployeeCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateEmployeeCommand = new RelayCommand<EmployeeAccount>(OpenUpdateEmployeeDialog);
        }

        private void OpenAddEmployeeDialog(object p)
        {
            AddEmployeeDialog addEmployeeDialog = new AddEmployeeDialog();
            AddEmployeeViewModel addEmployeeVM = (AddEmployeeViewModel)addEmployeeDialog.DataContext;
            addEmployeeVM.ParentDataContext = this;
            addEmployeeVM.EmployeeInputDataContext.Retrieve(new EmployeeAccount(AutoGenerateID()));
            addEmployeeDialog.ShowDialog();
        }
        
        private string AutoGenerateID()
        {
            string employeeID;
            Random random = new Random();
            do
            {
                int number = random.Next(10000);
                employeeID = $"EM{number:0000}";
            } while (employeeAccountDao.SearchByID(employeeID) != null);
            return employeeID;
        }

        private void ExecuteDeleteCommand(string id)
        {
            employeeAccountDao.Delete(id);
            LoadEmployees();
        }

        private void OpenUpdateEmployeeDialog(EmployeeAccount employeeAccount)
        {
            UpdateEmployeeDialog updateEmployeeDialog = new UpdateEmployeeDialog();
            UpdateEmployeeViewModel updateEmployeeVM = (UpdateEmployeeViewModel)updateEmployeeDialog.DataContext;
            updateEmployeeVM.ParentDataContext = this;
            updateEmployeeVM.EmployeeInputDataContext.Retrieve(employeeAccount);
            updateEmployeeDialog.ShowDialog();
        }

        public void Add(EmployeeAccount employeeAccount)
        {
            employeeAccountDao.Add(employeeAccount);
            LoadEmployees();
        }

        public void Update(EmployeeAccount employeeAccount)
        {
            employeeAccountDao.Update(employeeAccount);
            LoadEmployees();
        }
    }
}