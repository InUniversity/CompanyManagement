using CompanyManagement.Models;
using System.Collections.Generic;
using System.Windows.Input;
using System;
using System.Windows.Controls;
using CompanyManagement.Database.Implementations;
using CompanyManagement.Utilities;
using CompanyManagement.Database.Interfaces;
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
        void Retrieve(Employee employee);
    }

    public class EmployeeInputViewModel : BaseViewModel, IEmployeeInput
    {
        private string id = "";
        public string ID { get => id; set { id = value; OnPropertyChanged(); } }

        private string name = "";
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }

        private string gender = "";
        public string Gender { get => gender; set { gender = value; OnPropertyChanged(); } }

        private DateOnly birthday = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly Birthday { get => birthday; set { birthday = value; OnPropertyChanged(); } }

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

        private IPositionDao positionDao;
        private IDepartmentDao departmentDao;

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
            return new Employee(ID, Name, Gender, Birthday, IdentifyCard,
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
            int age = Birthday.Year - DateOnly.FromDateTime(DateTime.Now).Year;
            if (age < 18 || (age == 18 && (Birthday.Month)<(DateOnly.FromDateTime(DateTime.Now).Month)))
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

        public void Retrieve(Employee employee)
        {
            if (employee == null) 
                return;
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