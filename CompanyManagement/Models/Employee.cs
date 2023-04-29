using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Utilities;

namespace CompanyManagement
{
    public class Employee
    {
        private string id = "";
        private string name = "";
        private string gender = "";
        private DateTime birthday = DateTime.Now;
        private string identifyCard = "";
        private string email = "";
        private string phoneNumber = "";
        private string address = "";
        private decimal salary;
        private string departmentID = "";
        private string roleID = "";
        private Account account = new Account();
        private Role role = new Role();

        public string ID
        {
            get => id;
            set => id = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Gender
        {
            get => gender;
            set => gender = value;
        }

        public DateTime Birthday
        {
            get => birthday;
            set => birthday = value;
        }

        public string IdentifyCard
        {
            get => identifyCard;
            set => identifyCard = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set => phoneNumber = value;
        }

        public string Address
        {
            get => address;
            set => address = value;
        }
        
        public decimal Salary
        {
            get => salary;
            set => salary = value;
        }

        public string DepartmentID
        {
            get => departmentID;
            set => departmentID = value;
        }

        public string RoleID
        {
            get => roleID;
            set => roleID = value;
        }

        public Account MyAccount
        {
            get => account;
            set => account = value;
        }

        public Role EmployeeRole
        {
            get => role;
            set => role = value;
        }

        public Employee() { }

        public Employee(string id, string name, string gender, DateTime birthday, string identifyCard, string email, 
            string phoneNumber, string address, string departmentID, string roleID, decimal salary)
        {
            this.id = id;
            this.name = name;
            this.gender = gender;
            this.birthday = birthday;
            this.identifyCard = identifyCard;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.departmentID = departmentID;
            this.roleID = roleID;
            this.salary = salary;
        }
        
        public Employee(IDataRecord reader)
        {
            try
            {
                id = Utils.GetString(reader, BaseDao.EMPLOYEES_ID);
                name = Utils.GetString(reader, BaseDao.EMPLOYEES_NAME);
                gender = Utils.GetString(reader, BaseDao.EMPLOYEES_GENDER);
                birthday = Utils.GetDateTime(reader, BaseDao.EMPLOYEES_BIRTHDAY);
                identifyCard = Utils.GetString(reader, BaseDao.EMPLOYEES_IDENTIFY_CARD);
                email = Utils.GetString(reader, BaseDao.EMPLOYEES_EMAIL);
                phoneNumber = Utils.GetString(reader, BaseDao.EMPLOYEES_PHONE_NUMBER);
                address = Utils.GetString(reader, BaseDao.EMPLOYEES_ADDRESS);
                salary = Utils.GetDecimal(reader, BaseDao.EMPLOYEES_SALARY);
                departmentID = Utils.GetString(reader, BaseDao.EMPLOYEES_DEPARTMENT_ID);
                roleID = Utils.GetString(reader, BaseDao.EMPLOYEES_POSITION_ID);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Employee), "CAST ERROR: " + ex.Message);
            }
        }
    }
}