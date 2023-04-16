using CompanyManagement.Models;
using System.Collections.Generic;
using System;
using CompanyManagement.Database;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.UserControls
{
    public class EmployeeInputViewModel : BaseViewModel
    {
        private Employee employee; 
        public string ID { get => employee.ID; set { employee.ID = value; OnPropertyChanged(); } }
        public string Name { get => employee.Name; set { employee.Name = value; OnPropertyChanged(); } }
        public string Gender { get => employee.Gender; set { employee.Gender = value; OnPropertyChanged(); } }
        public DateTime Birthday { get => employee.Birthday; set { employee.Birthday = value; OnPropertyChanged(); } }
        public string IdentifyCard { get => employee.IdentifyCard; set { employee.IdentifyCard = value; OnPropertyChanged(); } }
        public string Email { get => employee.Email; set { employee.Email = value; OnPropertyChanged(); } }
        public string PhoneNumber { get => employee.PhoneNumber; set { employee.PhoneNumber = value; OnPropertyChanged(); } }
        public string Address { get => employee.Address; set { employee.Address = value; OnPropertyChanged(); } }
        public string DepartmentID { get => employee.DepartmentID; set { employee.DepartmentID = value; OnPropertyChanged(); } }
        public string PositionID { get => employee.PositionID; set { employee.PositionID = value; OnPropertyChanged(); } }
        public int Salary { get => employee.Salary; set { employee.Salary = value; OnPropertyChanged(); } }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        public List<PositionInCompany> Positions { get; set; }
        public List<Department> Departments { get; set; }

        private PositionDao positionDao = new PositionDao();
        private DepartmentDao departmentDao = new DepartmentDao();

        public EmployeeInputViewModel()
        {
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
                string.IsNullOrWhiteSpace(DepartmentID) || string.IsNullOrWhiteSpace(PositionID))
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
            ID = ID.Trim();
            Name = Name.Trim();
            Gender = Gender.Trim();
            IdentifyCard = IdentifyCard.Trim();
            Email = Email.Trim();
            PhoneNumber = PhoneNumber.Trim();
            Address = Address.Trim();
            DepartmentID = DepartmentID.Trim();
            PositionID = PositionID.Trim();
        }

        public void Receive(Employee employee)
        {
            this.employee = employee;
        }
    }
}