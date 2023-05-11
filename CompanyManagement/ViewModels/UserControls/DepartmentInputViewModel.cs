using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Utilities;
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

        private ObservableCollection<Employee> searchedEmployeesCanBeDeptHead;
        public ObservableCollection<Employee> SearchedEmployeesCanBeDeptHead
        { get => searchedEmployeesCanBeDeptHead; set { searchedEmployeesCanBeDeptHead = value; OnPropertyChanged(); } }

        private string textToSearchDeptHead = "";
        public string TextToSearchDeptHead { get => textToSearchDeptHead; set { textToSearchDeptHead = value; OnPropertyChanged(); SearchByNameDeptHead(); } }

        public ICommand GetSelectedDeptHeadCommand { get; private set; }

        private EmployeesDao employeesDao = new EmployeesDao();
        private RolesDao rolesDao = new RolesDao();

        public DepartmentInputViewModel()
        {
            SetCommands();
        }

        private void SetCommands()
        {
            GetSelectedDeptHeadCommand = new RelayCommand<ListView>(ExecuteGetSelectedDeptHeadCommand);
        }

        private void ExecuteGetSelectedDeptHeadCommand(ListView listView) 
        {
            if (listView.SelectedItem == null) return;
            DeptHead = listView.SelectedItem as Employee;
        }
        
        private void LoadEmployeesCanBeDeptHead() 
        {
            var allEmpls = employeesDao.GetAllWithoutManagers();
            var deptHeadList = from e in allEmpls 
                where e.EmplRole.Perms == Permission.DepHead && (e.DepartmentID == "" || e.DepartmentID == DeptIns.ID)
                select e;
            GetRoleForListEmployees(deptHeadList.ToList());
            employeesCanbeDeptHead = new List<Employee>(deptHeadList.ToList());
        }

        private void LoadEmployeeInDepartment()
        {
            var list = employeesDao.SearchByDepartmentID(dept.ID);
            GetRoleForListEmployees(list);
            EmplsInDept = new ObservableCollection<Employee>(list);         
        }

        private void GetRoleForListEmployees(List<Employee> employees)
        {
            foreach (var empl in employees)
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

        public void TrimAllTexts()
        {
            Name = Name.Trim();
        }

        public bool CheckAllFields()
        {
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = Utils.invalidEmptyMess;
                return false;
            }
            if (DeptHead == null)
            {
                ErrorMessage = Utils.invalidDeptHead;
                return false;
            }
            return true;
        }
    }
}