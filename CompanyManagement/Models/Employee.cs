using System;
using System.Data;
using System.Data.SqlClient;
using CompanyManagement.Database;

namespace CompanyManagement
{
    public class Employee
    {
        
        private string id;
        private string name;
        private string gender;
        private string birthday;
        private string identifyCard;
        private string email;
        private string phoneNumber;
        private string address;
        private string departmentID;
        private string positionID;
        private int salary;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public string Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        public string IdentifyCard
        {
            get { return identifyCard; }
            set { identifyCard = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string DepartmentID
        {
            get { return departmentID; }
            set { departmentID = value; }
        }

        public string PositionID
        {
            get { return positionID; }
            set { positionID = value; }
        }

        public int Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        public Employee() { }

        public Employee(string id, string name, string gender, string birthday, string identifyCard, string email, 
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
                id = (string)reader[EmployeeDao.ID];
                name = (string)reader[EmployeeDao.NAME];
                gender = (string)reader[EmployeeDao.GENDER];
                birthday = (string)reader[EmployeeDao.BIRTHDAY];
                identifyCard = (string)reader[EmployeeDao.IDENTIFY_CARD];
                email = (string)reader[EmployeeDao.EMAIL];
                phoneNumber = (string)reader[EmployeeDao.PHONE_NUMBER];
                address = (string)reader[EmployeeDao.ADDRESS];
                departmentID = (string)reader[EmployeeDao.DEPARTMENT_ID];
                positionID = (string)reader[EmployeeDao.POSITION_ID];
                salary = (int)reader[EmployeeDao.SALARY];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}