using CompanyManagement.Models;
using System.Collections.Generic;
using System;
using CompanyManagement.Database;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface IEmployeeInput
    {
        string ID { get; }
        string IdentifyCard { get; }
        string PhoneNumber { get; }
        string ErrorMessage { set; }
        Employee CreateEmployeeInstance();
        bool CheckAllFields();
        void TrimAllTexts();
        void Receive(Employee employee);
    }

    public class EmployeeInputViewModel : BaseViewModel, IEmployeeInput
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

        private int salary = 0;
        public int Salary { get => salary; set { salary = value; OnPropertyChanged(); } }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        public List<PositionInCompany> Positions { get; set; }
        public List<Department> Departments { get; set; }

        private PositionDao positionDao;
        private DepartmentDao departmentDao;

        public EmployeeInputViewModel()
        {
            positionDao = new PositionDao();
            departmentDao = new DepartmentDao();
            SetAllComboBox();
        }

        private void SetAllComboBox()
        {
            Positions = positionDao.GetAll();
            Departments = departmentDao.GetAll();
        }

        public Employee CreateEmployeeInstance()
        {
            return new Employee(ID, Name, Gender, Birthday.Date, IdentifyCard,
                Email, PhoneNumber, Address, DepartmentID, PositionID, Salary);
        }

        public bool CheckAllFields()
        {
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Gender) ||
                string.IsNullOrWhiteSpace(IdentifyCard) || string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Address) ||
                string.IsNullOrWhiteSpace(departmentID) || string.IsNullOrWhiteSpace(positionID))
            {
                ErrorMessage = Utils.INVALIDATE_EMPTY_MESSAGE;
                return false;
            }
            if (!CheckFormat.ValidateBirthday(Birthday))
            {
                ErrorMessage = Utils.INVALIDATE_BIRTHDAY_MESSAGE;
                return false;
            }
            if (!CheckFormat.ValidateEmail(Email))
            {
                ErrorMessage = Utils.INVALIDATE_EMAIL_MESSAGE;
                return false;
            }
            if (!CheckFormat.ValidatePhoneNumber(PhoneNumber))
            {
                ErrorMessage = Utils.INVALIDATE_PHONE_NUMBER_MESSAGE;
                return false;
            }
            if (!CheckFormat.ValidateIdentifyCard(IdentifyCard))
            {
                ErrorMessage = Utils.INVALIDATE_IDENTIFY_CARD_MESSAGE;
                return false;
            }
            return true;
        }

        public void TrimAllTexts()
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

        public void Receive(Employee employee)
        {
            ID = employee.ID;
            Name = employee.Name;
            Gender = employee.Gender;
            Birthday = employee.Birthday;
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