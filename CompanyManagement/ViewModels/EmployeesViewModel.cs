using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CompanyManagement.Dialogs;
using CompanyManagement.Database.Interfaces;

namespace CompanyManagement.ViewModels
{
    public interface IEmployees
    {
        void Add(Employee employee);
        void Update(Employee employee);
    }
    
    public class EmployeesViewModel : BaseViewModel, IEmployees
    {

        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees { get => employees; set { employees = value; OnPropertyChanged(); } }

        public ICommand OpenInputDialogCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand UpdateEmployeeCommand { get; set; }

        private IEmployeeDao employeeDao;

        public EmployeesViewModel(IEmployeeDao employeeDao)
        {
            this.employeeDao = employeeDao;
            LoadEmployees();
            SetCommands();
        }

        private void LoadEmployees()
        {
            Employees = new ObservableCollection<Employee>(employeeDao.GetAll());
        }

        private void SetCommands()
        {
            OpenInputDialogCommand = new RelayCommand<object>(OpenAddEmployeeDialog);
            DeleteEmployeeCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateEmployeeCommand = new RelayCommand<Employee>(OpenUpdateEmployeeDialog);
        }

        private void OpenAddEmployeeDialog(object p)
        {
            AddEmployeeDialog addEmployeeDialog = new AddEmployeeDialog();
            AddEmployeeViewModel addEmployeeVM = (AddEmployeeViewModel)addEmployeeDialog.DataContext;
            addEmployeeVM.ParentDataContext = this;
            addEmployeeVM.EmployeeInputDataContext.Retrieve(new Employee(AutoGenerateID()));
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
            } while (employeeDao.SearchByID(employeeID) != null);
            return employeeID;
        }

        private void ExecuteDeleteCommand(string id)
        {
            employeeDao.Delete(id);
            LoadEmployees();
        }

        private void OpenUpdateEmployeeDialog(Employee employee)
        {
            UpdateEmployeeDialog updateEmployeeDialog = new UpdateEmployeeDialog();
            UpdateEmployeeViewModel updateEmployeeVM = (UpdateEmployeeViewModel)updateEmployeeDialog.DataContext;
            updateEmployeeVM.ParentDataContext = this;
            updateEmployeeVM.EmployeeInputDataContext.Retrieve(employee);
            updateEmployeeDialog.ShowDialog();
        }

        public void Add(Employee employee)
        {
            employeeDao.Add(employee);
            LoadEmployees();
        }

        public void Update(Employee employee)
        {
            employeeDao.Update(employee);
            LoadEmployees();
        }
    }
}