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

    public interface IDepartmentInput
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

    public class DepartmentInputViewModel : BaseViewModel, IDepartmentInput
    {

        public string ID { get => dept.ID; set { dept.ID = value; OnPropertyChanged(); } }
        public string Name { get => dept.Name; set { dept.Name = value; OnPropertyChanged(); } }
        public string DeptHeadID { get => dept.DeptHeadID; set { dept.DeptHeadID = value; OnPropertyChanged(); } }
        public ObservableCollection<Employee> EmplsInDept 
        { get => dept.Empls; set { dept.Empls = value; OnPropertyChanged(); } }

        private string name = "";
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }

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

        public Department CreateDepartmentInstance()
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
            if (employee.RoleID == BaseDao.deptHeadRole)
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
            var listDeptHead = from empl in listallempl 
                               where empl.RoleID == BaseDao.deptHeadRole 
                               select empl;
            GetRoleForListEmployees(listDeptHead.ToList());
            employeesCanbeDeptHead = new List<Employee>(listDeptHead.ToList());
        }

        private void LoadEmployeesCanAddInDept()
        {
            var listallempl = employeesDao.GetAllWithoutManagers();
            var listempl = from empl in listallempl 
                           where empl.RoleID != BaseDao.hrRole && empl.DepartmentID == "" 
                           select empl;
            GetRoleForListEmployees(listempl.ToList());
            employeesCanAddInDept = new List<Employee>(listempl.ToList());
            SearchedEmployeesCanAddInDept = new ObservableCollection<Employee>(employeesCanAddInDept);
        }

        private void LoadEmployeeInDepartment()
        {
            var list = employeesDao.SearchByDepartmentID(dept.ID);
            GetRoleForListEmployees(list);
            EmplsInDept = new ObservableCollection<Employee>(list);         
        }

        private void GetRoleForListEmployees(List<Employee> employees)
        {
            foreach (Employee empl in employees)
                empl.EmplRole = rolesDao.SearchByID(empl.RoleID);
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
            id = id.Trim();
            name = name.Trim();
            managerID = managerID.Trim();
        }

        public void Receive(Department department)
        {
            ID = department.ID;
            Name = department.Name;
            managerID = department.DeptHeadID;
        }
    }
}