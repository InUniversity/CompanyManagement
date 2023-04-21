using System;
using System.Data.SqlClient;
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
        private string departmentID = "";
        private string positionID = "";
        private int salary;
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

        public string DepartmentID
        {
            get => departmentID;
            set => departmentID = value;
        }

        public string PositionID
        {
            get => positionID;
            set => positionID = value;
        }

        public int Salary
        {
            get => salary;
            set => salary = value;
        }

        public Account MyAccount
        {
            get => account;
            set => account = value;
        }

        public Employee() { }

        public Employee(string id, string name, string gender, DateTime birthday, string identifyCard, string email, 
            string phoneNumber, string address, string departmentID, string positionID, int salary)
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
            this.positionID = positionID;
            this.salary = salary;
        }
        
        public Employee(SqlDataReader reader)
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
                departmentID = (string)reader[BaseDao.EMPLOYEE_DEPARTMENT_ID];
                positionID = (string)reader[BaseDao.EMPLOYEE_POSITION_ID];
                salary = (int)reader[BaseDao.EMPLOYEE_SALARY];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Employee), "CAST ERROR: " + ex.Message);
            }
        }
    }
}