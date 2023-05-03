using System.Collections.ObjectModel;
using System.Windows.Input;
using CompanyManagement.Database;
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
            set => dept = value;
        }

        public string ID { get => dept.ID; set { dept.ID = value; OnPropertyChanged(); } }
        public string Name { get => dept.Name; set { dept.Name = value; OnPropertyChanged(); } }
        public string DeptHeadID { get => dept.DeptHeadID; set { dept.DeptHeadID = value; OnPropertyChanged(); } }
        public ObservableCollection<Employee> EmplsInDept 
        { get => dept.Empls; set { dept.Empls = value; OnPropertyChanged(); } }

        public string errorMessage;
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }
        
        public ICommand DeleteEmplCommand { get; private set; }

        private DepartmentsDao departmentsDao = new DepartmentsDao();
        private EmployeesDao employeesDao = new EmployeesDao();

        public DepartmentInputViewModel()
        {
            SetCommands();
            LoadEmployeeInDepartment();
        }

        private void SetCommands()
        {
        }

        private void LoadEmployeeInDepartment()
        {
            var list = employeesDao.SearchByDepartmentID(dept.ID);
            EmplsInDept = new ObservableCollection<Employee>(list);
        }

        public void TrimAllTexts()
        {
            Name = Name.Trim();
        }
    }
}