using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.UserControls
{
    public class DepartmentInputViewModel : BaseViewModel 
    {
        private Department dept = new Department();
        public Department DeptIns
        {
            get => dept;
            set 
            { 
                dept = value;
                LoadEmployeesCanBeDeptHead();
                LoadEmployeesCanAddInDept();
                SearchByNameDeptHead();
                LoadEmployeeInDepartment();
            }
        }

        public Employee DeptHead
        {
            get => dept.DeptHead;
            set
            {
                dept.DeptHead = value;
                dept.DeptHeadID = dept.DeptHead.ID;
                OnPropertyChanged();
            }
        }

        public string ID { get => dept.ID; set { dept.ID = value; OnPropertyChanged(); } }
        public string Name { get => dept.Name; set { dept.Name = value; OnPropertyChanged(); } }
        public string DeptHeadID { get => dept.DeptHeadID; set { dept.DeptHeadID = value; OnPropertyChanged(); } }
        public ObservableCollection<Employee> EmplsInDept 
        { get => dept.Empls; set { dept.Empls = value; OnPropertyChanged(); } }

        public string errorMessage;
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        private List<Employee> employeesCanbeDeptHead;

        private List<Employee> employeesCanAddInDept;

        private ObservableCollection<Employee> searchedEmployeesCanBeDeptHead;
        public ObservableCollection<Employee> SearchedEmployeesCanBeDeptHead
        { get => searchedEmployeesCanBeDeptHead; set { searchedEmployeesCanBeDeptHead = value; OnPropertyChanged(); } }

        private ObservableCollection<Employee> searchedEmployeesCanAddInDept;
        public ObservableCollection<Employee> SearchedEmployeesCanAddInDept
        { get => searchedEmployeesCanAddInDept; set { searchedEmployeesCanAddInDept = value; OnPropertyChanged(); } }

        private List<Employee> selectedEmployees = new();
        public List<Employee> SelectedEmployees
        { get => selectedEmployees; set { selectedEmployees = value; OnPropertyChanged(); } }

        private string textToSearchDeptHead = "";
        public string TextToSearchDeptHead { get => textToSearchDeptHead; set { textToSearchDeptHead = value; OnPropertyChanged(); SearchByNameDeptHead(); } }

        private string textToSearchEmpl = "";
        public string TextToSearchEmpl { get => textToSearchEmpl; set { textToSearchEmpl = value; OnPropertyChanged(); SearchByNameEmpl(); } }

        public ICommand GetAllSelectedEmployeesCommand { get; private set; }
        public ICommand GetSelectedDeptHeadCommand { get; private set; }
        public ICommand DeleteEmployeeCommand { get; private set; }
        public ICommand AddEmployeeCommand { get; private set; }

        private DepartmentsDao departmentsDao = new DepartmentsDao();
        private EmployeesDao employeesDao = new EmployeesDao();
        private RolesDao rolesDao = new RolesDao();

        public DepartmentInputViewModel()
        {
            SetCommands();
        }

        private void SetCommands()
        {
            GetSelectedDeptHeadCommand = new RelayCommand<ListView>(ExecuteGetSelectedDeptHeadCommand);
            GetAllSelectedEmployeesCommand = new RelayCommand<ListView>(ExecuteGetAllSelectedEmployeesCommand);
            AddEmployeeCommand = new RelayCommand<object>(ExecuteAddEmployeeCommand);
            DeleteEmployeeCommand = new RelayCommand<Employee>(ExecuteDeleteEmployeeCommand);

        }

        private void ExecuteAddEmployeeCommand(object obj)
        {
            if (SelectedEmployees != null)
            {
                foreach (var employee in SelectedEmployees)
                {
                    EmplsInDept.Add(employee);
                    employeesCanAddInDept.Remove(employee);
                    SearchedEmployeesCanAddInDept.Remove(employee);
                }
            }
            SelectedEmployees = null;
        }

        private void ExecuteDeleteEmployeeCommand(Employee employee)
        {
            if (employee.PermsID == BaseDao.deptHeadPermsID)
                return;
            EmplsInDept.Remove(employee);
            employeesCanAddInDept.Add(employee);
            SearchedEmployeesCanAddInDept.Add(employee);
        }

        private void ExecuteGetAllSelectedEmployeesCommand(ListView listView)
        {
            var selectedItems = listView.SelectedItems.Cast<Employee>().ToList();
            selectedEmployees = selectedItems;
        }

        private void ExecuteGetSelectedDeptHeadCommand(ListView listView) 
        {
            if (listView.SelectedItem == null) return;
            DeptHead = listView.SelectedItem as Employee;
        }

        private void LoadEmployeesCanBeDeptHead() 
        {
            var listallempl = employeesDao.GetAllWithoutManagers();
            var listDeptHead = (from empl in listallempl 
                               where empl.PermsID == BaseDao.deptHeadPermsID 
                               select empl).ToList();
            LoadEmplRole(listDeptHead);
            employeesCanbeDeptHead = new List<Employee>(listDeptHead);
        }

        private void LoadEmplRole(List<Employee> list)
        {
            foreach(Employee empl in list)
            {
                empl.EmplRole = rolesDao.SearchByID(empl.RoleID);
            }    
        }

        private void LoadEmployeesCanAddInDept()
        {
            var listallempl = employeesDao.GetAllWithoutManagers();
            var listempl = (from empl in listallempl 
                           where empl.PermsID != BaseDao.hrPermsID && empl.DepartmentID == "" 
                           select empl).ToList();
            LoadEmplRole(listempl);
            employeesCanAddInDept = new List<Employee>(listempl);
            SearchedEmployeesCanAddInDept = new ObservableCollection<Employee>(employeesCanAddInDept);
        }

        private void LoadEmployeeInDepartment()
        {
            var list = employeesDao.SearchByDepartmentID(dept.ID);
            EmplsInDept = new ObservableCollection<Employee>(list);         
        }

        private void SearchByNameDeptHead()
        {
            var searchedItems = employeesCanbeDeptHead;
            if (!string.IsNullOrEmpty(textToSearchDeptHead))
            {
                searchedItems = employeesCanbeDeptHead
                    .Where(item => item.Name.ToLower().Contains(textToSearchDeptHead.ToLower()))
                    .ToList();
            }
            SearchedEmployeesCanBeDeptHead = new ObservableCollection<Employee>(searchedItems);
        }

        private void SearchByNameEmpl()
        {
            var searchedItems = employeesCanAddInDept;
            if (!string.IsNullOrEmpty(textToSearchEmpl))
            {
                searchedItems = employeesCanAddInDept
                    .Where(item => item.Name.ToLower().Contains(textToSearchEmpl.ToLower()))
                    .ToList();
            }
            SearchedEmployeesCanAddInDept = new ObservableCollection<Employee>(searchedItems);
        }

        public void TrimAllTexts()
        {
            Name = Name.Trim();
        }

        public bool CheckAllFields()
        {

            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = "Các thông tin không được để trống!!!";
                return false;
            }
            if (DeptHead == null)
            {
                ErrorMessage = "Phải có trưởng phòng!!!";
                return false;
            }
            return true;
        }
    }
}