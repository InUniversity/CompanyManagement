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
        private int salary;
        private string departmentID = "";
        private string roleID = "";
        private Account account = new Account();

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
        
        public int Salary
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

        public Employee() { }

        public Employee(string id, string name, string gender, DateTime birthday, string identifyCard, string email, 
            string phoneNumber, string address, string departmentID, string roleID, int salary)
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
                id = (string)reader[BaseDao.EMPLOYEE_ID];
                name = (string)reader[BaseDao.EMPLOYEE_NAME];
                gender = (string)reader[BaseDao.EMPLOYEE_GENDER];
                birthday = reader.GetDateTime(reader.GetOrdinal(BaseDao.EMPLOYEE_BIRTHDAY));
                identifyCard = (string)reader[BaseDao.EMPLOYEE_IDENTIFY_CARD];
                email = (string)reader[BaseDao.EMPLOYEE_EMAIL];
                phoneNumber = (string)reader[BaseDao.EMPLOYEE_PHONE_NUMBER];
                address = (string)reader[BaseDao.EMPLOYEE_ADDRESS];
                salary = (int)reader[BaseDao.EMPLOYEE_SALARY];
                departmentID = (string)reader[BaseDao.EMPLOYEE_DEPARTMENT_ID];
                roleID = (string)reader[BaseDao.EMPLOYEE_POSITION_ID];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Employee), "CAST ERROR: " + ex.Message);
            }
        }
    }
}