using System;
using System.Buffers;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Models;

namespace CompanyManagement.ViewModels
{
    public class EmployeeInputViewModel : BaseViewModel
    {

        private string id = "";
        public string ID { get => id; set { id = value; OnPropertyChanged(); } }

        private string name = "";
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }

        private string gender = "";
        public string Gender { get => gender; set { gender = value; OnPropertyChanged(); } }

        private DateTime birthday;
        public DateTime Birthday { get => birthday; set { birthday = value; OnPropertyChanged(); } }

        private string identifyCard = "";
        public string IdentifyCard { get => identifyCard; set { identifyCard = value; OnPropertyChanged(); } }

        private string email = "";
        public string Email { get => email; set { email = value; OnPropertyChanged(); } }

        private string phoneNumber = "";
        public string PhoneNumber { get => phoneNumber; set { phoneNumber = value; OnPropertyChanged(); } }

        private string address = "";
        public string Address { get => address; set { address = value; OnPropertyChanged(); } }

        private string departmentID = "";
        public string DepartmentID { get => departmentID; set { departmentID = value; OnPropertyChanged(); } }

        private string positionID = "";
        public string PositionID { get => positionID; set { positionID = value; OnPropertyChanged(); } }

        private int salary = 0;
        public int Salary { get => salary; set { salary = value; OnPropertyChanged(); } }
        
        public ICommand AddEmployeeCommand { get; set; }

        public List<PositionInCompany> Positions { get; set; }
        public List<Department> Departments { get; set; }

        private PositionDao positionDao = new PositionDao();
        private DepartmentDao departmentDao = new DepartmentDao();

        public EmployeeInputViewModel()
        {
            AddEmployeeCommand = new ReplayCommand<object>(AddEmployee);
            SetAllComboBox();
        }

        private void SetAllComboBox()
        {
            DataTable positionTable = positionDao.GetDataTable();
            Positions = new List<PositionInCompany>();
            foreach (DataRow row in positionTable.Rows)
            {
                PositionInCompany pos = new PositionInCompany(row);
                Positions.Add(pos);
            }
            
            DataTable departmentTable = departmentDao.GetDataTable();
            Departments = new List<Department>();
            foreach (DataRow row in departmentTable.Rows)
            {
                Department dep = new Department(row);
                Departments.Add(dep);
            }
        }

        private void AddEmployee(object p)
        {
            TrimAllTexts();
            if (!CheckAllFields())
                return;
            Employee empl = CreateEmployeeInstance();
            SendEmployeeInstance(new EmployeesViewModel(), empl);
            CloseWindow(null);
        }

        private Employee CreateEmployeeInstance()
        {
            return new Employee(ID, Name, Gender, Utils.DateToString(Birthday), IdentifyCard, 
                Email, PhoneNumber, Address, DepartmentID, PositionID, Salary);
        }

        private void SendEmployeeInstance(IRetrieveEmployee retriever, Employee employee)
        {
            retriever.Retrieve(employee);
        }

        private bool CheckAllFields()
        {
            if (string.Equals(ID, "") || string.Equals(Name, "") || string.Equals(Gender, "") ||
                string.Equals(IdentifyCard, "") || string.Equals(Email, "") || string.Equals(PhoneNumber, "") ||
                string.Equals(Address, "") || string.Equals(departmentID, "") || string.Equals(positionID, ""))
            {
                MessageBox.Show("Các thông tin không được để trống!!!");
                return false;
            }
            if (Birthday >= DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ!!!");
                return false;
            }
            return true;
        }

        private void TrimAllTexts()
        {
            id = id.Trim();
            name = name.Trim();
            gender = gender.Trim();
            identifyCard = identifyCard.Trim();
            email = email.Trim();
            phoneNumber = phoneNumber.Trim();
            address = address.Trim();
            departmentID = departmentID.Trim();
            positionID = positionID.Trim();
        }
    }
}
