using System;
using System.Collections.ObjectModel;
using System.Data;
using CompanyManagement.Database;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class EmployeeViewModel : BaseViewModel
    {

        private string id;
        public string ID { get => id; set { id = value; OnPropertyChanged(); } }
        
        private string fullName;
        public string FullName { get => fullName; set { fullName = value; OnPropertyChanged(); } }
        
        private string gender;
        public string Gender { get => gender; set { gender = value; OnPropertyChanged(); } }
        
        private DateTime birthday;
        public DateTime Birthday { get => birthday; set { birthday = value; OnPropertyChanged(); } }
        
        private string identifyCard;
        public string IdentifyCard { get => identifyCard; set { identifyCard = value; OnPropertyChanged(); } }
        
        private string phoneNumber;
        public string PhoneNumber { get => phoneNumber; set { phoneNumber = value; OnPropertyChanged(); } }
        
        private string managerID;
        public string ManagerID { get => managerID; set { managerID = value; OnPropertyChanged(); } }
        
        private int salary;
        public int Salary { get => salary; set { salary = value; OnPropertyChanged(); } }
        
        private string address;
        public string Address { get => address; set { address = value; OnPropertyChanged(); } }

        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees { get => employees; set { employees = value; OnPropertyChanged(); } }

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand UpdateEmployeeCommand { get; set; }

        private EmployeeDao employeeDao = new EmployeeDao();

        public EmployeeViewModel()
        {
            LoadGVEmployees();
            SetCommands();
        }

        private void LoadGVEmployees()
        {
            employees = new ObservableCollection<Employee>();
            DataTable dataTable = employeeDao.GetDataTable();
            foreach (DataRow row in dataTable.Rows)
            {
                Employee employee = new Employee(row);
                employees.Add(employee);
            }
        }

        private void SetCommands()
        {
            AddEmployeeCommand = new ReplayCommand<object>((p) => Add(p), (p) => CheckAllFields(p));
            DeleteEmployeeCommand = new ReplayCommand<object>((p) => Delete(p), (p) => id != null && employeeDao.SearchByID(id) == null);
            UpdateEmployeeCommand = new ReplayCommand<object>((p) => Update(p));
        }

        private void Add(object p)
        {
            Employee employee = new Employee();
            employeeDao.Add(employee);
        }

        private void Delete(object p)
        {
            employeeDao.Delete(id); 
        }

        private void Update(object p)
        {
            Employee employee = new Employee();
            employeeDao.Save(employee); 
        }

        private bool CheckAllFields(object p)
        {
            return true;
        }
    }
}
