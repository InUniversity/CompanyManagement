using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using CompanyManagement.Database;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Dialogs;
using Microsoft.VisualBasic;

namespace CompanyManagement.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {

        // private string id;
        // public string ID { get => id; set { id = value; OnPropertyChanged(); } }
        //
        // private string name;
        // public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        //
        // private string gender;
        // public string Gender { get => gender; set { gender = value; OnPropertyChanged(); } }
        //
        // private DateTime birthday;
        // public DateTime Birthday { get => birthday; set { birthday = value; OnPropertyChanged(); } }
        //
        // private string identifyCard;
        // public string IdentifyCard { get => identifyCard; set { identifyCard = value; OnPropertyChanged(); } }
        //
        // private string email;
        // public string Email { get => email; set { email = value; OnPropertyChanged(); } }
        //
        // private string phoneNumber;
        // public string PhoneNumber { get => phoneNumber; set { phoneNumber = value; OnPropertyChanged(); } }
        //
        // private string address;
        // public string Address { get => address; set { address = value; OnPropertyChanged(); } }
        //
        // private string departmentID;
        // public string DepartmentID { get => departmentID; set { departmentID = value; OnPropertyChanged(); } }
        //
        // private string positionID;
        // public string PositionID { get => positionID; set { positionID = value; OnPropertyChanged(); } }
        //
        // private int salary;
        // public int Salary { get => salary; set { salary = value; OnPropertyChanged(); } }
        

        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees { get => employees; set { employees = value; OnPropertyChanged(); } }

        public ICommand OpenEmployeeInputDialogCommand { get; set; }
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand UpdateEmployeeCommand { get; set; }
        public ICommand SayHelloCommand { get; set; }

        private EmployeeDao employeeDao = new EmployeeDao();

        public EmployeesViewModel()
        {
            LoadDGEmployees();
            SetCommands();
        }

        private void LoadDGEmployees()
        {
            Employees = new ObservableCollection<Employee>();
            DataTable dataTable = employeeDao.GetDataTable();
            foreach (DataRow row in dataTable.Rows)
            {
                Employee employee = new Employee(row);
                Employees.Add(employee);
            }
        }

        private void SetCommands()
        {
            OpenEmployeeInputDialogCommand = new ReplayCommand<object>(OpenEmployeeInputDialog);
            AddEmployeeCommand = new ReplayCommand<object>(OpenEmployeeInputDialog);
            //DeleteEmployeeCommand = new ReplayCommand<object>(Delete, employeeDao.SearchByID(id) == null);
            UpdateEmployeeCommand = new ReplayCommand<object>(Update);
            SayHelloCommand = new ReplayCommand<object>((p) => MessageBox.Show("SayHello DataGrid"));
        }

        private void OpenEmployeeInputDialog(object p)
        {
            EmployeeInputViewModel viewModel = new EmployeeInputViewModel();
            EmployeeInputWindow employeeInputWindow = new EmployeeInputWindow();
            employeeInputWindow.DataContext = viewModel;
            employeeInputWindow.Owner = App.Current.MainWindow;
            employeeInputWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            employeeInputWindow.ShowDialog();
            //KeyValuePair<bool, Employee> result = viewModel.ShowDialog();

            //if (result.Key)
            //{
            //    Employees.Add(result.Value);
            //}
        }

        private void Delete()
        {
            string id = "";
            employeeDao.Delete(id);
        }

        private void Update(object p)
        {
            employeeDao.Save(null);
        }

        private bool CheckAllFields(object p)
        {
            //if (string.Equals(id, "") || string.Equals(Name, "") || string.Equals(gender, "") ||
            //    string.Equals(identifyCard, "") || string.Equals(phoneNumber, ""))
            //{
            //    MessageBox.Show("All Fields can't empty !!!");
            //    return false;
            //}
            return true;
        }
    }
}
