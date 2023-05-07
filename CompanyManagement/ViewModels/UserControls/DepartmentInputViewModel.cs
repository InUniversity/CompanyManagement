using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Windows.Controls;
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
                SearchByName();
            }
        }

        public Employee DeptHead
        {
            get => dept.DeptHead;
            set
            {
                dept.DeptHead = value;
                dept.DeptHeadID = dept.DeptHead.ID;
                LoadEmployeesCanBeDeptHead();
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

        private List<Employee> employees;

        private List<Employee> searchedEmployeesCanBeDeptHead;
        public List<Employee> SearchedEmployeesCanBeDeptHead
        { get => searchedEmployeesCanBeDeptHead; set { searchedEmployeesCanBeDeptHead = value; OnPropertyChanged(); } }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        public ICommand GetSelectedDeptHeadCommand { get; private set; }

        private DepartmentsDao departmentsDao = new DepartmentsDao();
        private EmployeesDao employeesDao = new EmployeesDao();

        public DepartmentInputViewModel()
        {
            SetCommands();
            LoadEmployeeInDepartment();
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
            var listallempl = employeesDao.SearchByCurrentID(CurrentUser.Ins.EmployeeIns.ID);
            var listDeptHead = from empl in listallempl where empl.RoleID == BaseDao.deptHeadRole select empl;
            employees = new List<Employee>(listDeptHead.ToList());
        }

        private void LoadEmployeeInDepartment()
        {
            var list = employeesDao.SearchByDepartmentID(dept.ID);
            EmplsInDept = new ObservableCollection<Employee>(list);
        }

        private void SearchByName()
        {
            var searchedItems = employees;
            if (!string.IsNullOrEmpty(textToSearch))
            {
                searchedItems = employees
                    .Where(item => item.Name.ToLower().Contains(textToSearch.ToLower()))
                    .ToList();
            }
            SearchedEmployeesCanBeDeptHead = new List<Employee>(searchedItems);
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