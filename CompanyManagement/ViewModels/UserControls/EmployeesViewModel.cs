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
        void Add(EmployeeAccount employeeAccount);
        void Update(EmployeeAccount employeeAccount);
    }

    public class EmployeesViewModel : BaseViewModel, IEmployees
    {
        private List<EmployeeAccount> employees;

        private ObservableCollection<EmployeeAccount> searchedEmployees;
        public ObservableCollection<EmployeeAccount> SearchedEmployees { get => searchedEmployees; set { searchedEmployees = value; OnPropertyChanged(); } }

        private EmployeeAccount selectedEmployee;
        public EmployeeAccount SelectedEmployee { get => selectedEmployee; set { selectedEmployee = value; OnPropertyChanged(); } }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        public ICommand OpenAddDialogCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand OpenUpdateDialogCommand { get; set; }

        private IEmployeeAccountDao employeeAccountDao;

        public EmployeesViewModel(IEmployeeAccountDao employeeAccountDao)
        {
            this.employeeAccountDao = employeeAccountDao;
            LoadEmployees();
            SetCommands();
        }

        private void LoadEmployees()
        {
            employees = employeeAccountDao.GetAll();
            SearchedEmployees = new ObservableCollection<EmployeeAccount>(employees);
        }

        private void SetCommands()
        {
            OpenAddDialogCommand = new RelayCommand<object>(OpenAddEmployeeDialog);
            DeleteEmployeeCommand = new RelayCommand<string>(DeleteEmployee);
            OpenUpdateDialogCommand = new RelayCommand<EmployeeAccount>(OpenUpdateEmployeeDialog);
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
            SearchedEmployees = new ObservableCollection<EmployeeAccount>(searchedItems);
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

        private void DeleteEmployee(string id)
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