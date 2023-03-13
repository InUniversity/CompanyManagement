using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.UserControls;

namespace CompanyManagement.ViewModels
{
    public class EmployeeInputViewModel : BaseViewModel, IRetrieveEmployee
    {

        private string id = "";
        public string ID { get => id; set { id = value; OnPropertyChanged(); } }

        private string name = "";
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }

        private string gender = "";
        public string Gender { get => gender; set { gender = value; OnPropertyChanged(); } }

        private DateTime birthday = DateTime.Now;
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

        private string contentAddUpdateButton;
        public string ContentAddUpdateButton { get => contentAddUpdateButton; set { contentAddUpdateButton = value; OnPropertyChanged(); } }

        private int salary = 0;
        public int Salary { get => salary; set { salary = value; OnPropertyChanged(); } }
        
        public ICommand AddEmployeeCommand { get; set; }

        public List<PositionInCompany> Positions { get; set; }
        public List<Department> Departments { get; set; }

        public EmployeesViewModel ParentViewModel { get; set; }

        private PositionDao positionDao = new PositionDao();
        private DepartmentDao departmentDao = new DepartmentDao();
        
        public EmployeeInputViewModel()
        {
            ContentAddUpdateButton = "Thêm";
            AddEmployeeCommand = new ReplayCommand<Window>(AddUpdateCommand);
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

        private void AddUpdateCommand(Window inputWindow)
        {
            TrimAllTexts();
            if (!CheckAllFields())
                return;
            Employee empl = CreateEmployeeInstance();
            SendEmployeeInstance(ParentViewModel, empl);
            inputWindow.Close();
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
            if (string.IsNullOrWhiteSpace(ID) || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Gender) ||
                string.IsNullOrWhiteSpace(IdentifyCard) || string.IsNullOrWhiteSpace(Email) || 
                string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Address) || 
                string.IsNullOrWhiteSpace(departmentID) || string.IsNullOrWhiteSpace(positionID))
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

        public void Retrieve(Employee employee)
        {
            ContentAddUpdateButton = "Lưu";
            ID = employee.ID;
            Name = employee.Name;
            Gender = employee.Gender;
            IdentifyCard = employee.IdentifyCard;
            Email = employee.Email;
            PhoneNumber = employee.PhoneNumber;
            Address = employee.Address;
            DepartmentID = employee.DepartmentID;
            PositionID = employee.PositionID;
            Salary = employee.Salary;
        }
    }
}
