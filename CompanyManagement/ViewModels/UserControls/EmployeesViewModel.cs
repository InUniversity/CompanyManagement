using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface IEmployees
    {
        void Add(Employee employee);
        void Update(Employee employee);
    }

    public class EmployeesViewModel : BaseViewModel, IEmployees
    {
        private List<Employee> employees;

        private ObservableCollection<Employee> searchedEmployees;
        public ObservableCollection<Employee> SearchedEmployees { get => searchedEmployees; set { searchedEmployees = value; OnPropertyChanged(); } }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        public ICommand OpenAddDialogCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand OpenUpdateDialogCommand { get; set; }

        private IEmployeeDao employeeAccountDao;

        public EmployeesViewModel(IEmployeeDao employeeAccountDao)
        {
            this.employeeAccountDao = employeeAccountDao;
            LoadEmployees();
            SetCommands();
        }

        private void LoadEmployees()
        {
            employees = employeeAccountDao.GetAll();
            SearchedEmployees = new ObservableCollection<Employee>(employees);
        }

        private void SetCommands()
        {
            OpenAddDialogCommand = new RelayCommand<object>(OpenAddEmployeeDialog);
            DeleteEmployeeCommand = new RelayCommand<string>(DeleteEmployee);
            OpenUpdateDialogCommand = new RelayCommand<Employee>(OpenUpdateEmployeeDialog);
        }

        private void SearchByName()
        {
            var searchedItems = employees;
            if (!string.IsNullOrEmpty(textToSearch))
            {
                searchedItems = employees
                    .Where(item => item.Name.Contains(textToSearch, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            SearchedEmployees = new ObservableCollection<Employee>(searchedItems);
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
            } while (employeeAccountDao.SearchByID(employeeID) != null);
            return employeeID;
        }

        private void DeleteEmployee(string id)
        {
            employeeAccountDao.Delete(id);
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
            employeeAccountDao.Add(employee);
            LoadEmployees();
        }

        public void Update(Employee employee)
        {
            employeeAccountDao.Update(employee);
            LoadEmployees();
        }
    }
}