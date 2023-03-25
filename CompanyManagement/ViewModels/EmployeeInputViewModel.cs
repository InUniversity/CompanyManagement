using CompanyManagement.Models;
using System.Collections.Generic;
using System.Windows.Input;
using System;
using System.Windows.Controls;
using CompanyManagement.Utilities;
using CompanyManagement.Database.Interfaces;

namespace CompanyManagement.ViewModels
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

        private DateTime birthday = DateTime.Now;
        public DateTime Birthday { get => birthday; set { birthday = value; OnPropertyChanged(); } }

        private string identifyCard = "";
        public string IdentifyCard { get => identifyCard; set { identifyCard = value; OnPropertyChanged(); } }

        private string username = "";
        public string Username { get => username; set { username = value; OnPropertyChanged(); } }

        private string password = "";
        public string Password { get => password; set { password = value; OnPropertyChanged(); } }

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

        public ICommand PasswordChangedCommand { get; set; }

        public List<PositionInCompany> Positions { get; set; }
        public List<Department> Departments { get; set; }

        private IPositionDao positionDao;
        private IDepartmentDao departmentDao;

        public EmployeeInputViewModel(IPositionDao positionDao, IDepartmentDao departmentDao)
        {
            this.positionDao = positionDao;
            this.departmentDao = departmentDao;
            PasswordChangedCommand = new RelayCommand<PasswordBox>(passwordBox => Password = passwordBox.Password);
            SetAllComboBox();
        }

        private void SetAllComboBox()
        {
            Positions = positionDao.GetAll();
            Departments = departmentDao.GetAll();
        }

        public Employee CreateEmployeeInstance()
        {
            return new Employee(ID, Name, Gender, Utils.DateToString(Birthday), IdentifyCard,
                Email, PhoneNumber, Address, DepartmentID, PositionID, Salary, new Account(Username, Password));
        }

        public bool CheckAllFields()
        {
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(ID) || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Gender) ||
                string.IsNullOrWhiteSpace(IdentifyCard) || string.IsNullOrWhiteSpace(Username) || 
                string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(Address) ||
                string.IsNullOrWhiteSpace(departmentID) || string.IsNullOrWhiteSpace(positionID))
            {
                ErrorMessage = Utils.INVALIDATE_EMPTY_MESSAGE;
                return false;
            }
            if (Birthday >= DateTime.Now)
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
            if (!CheckFormat.ValidatePassword(Password))
            {
                ErrorMessage = Utils.INVALIDATE_PASSWORK_MESSAGE;
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
            username = username.Trim();
            password = password.Trim();
        }

        public void Retrieve(Employee employee)
        {
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
            Username = employee.EmplAccount.Username;
            Password = employee.EmplAccount.Password;           
        }
    }
}