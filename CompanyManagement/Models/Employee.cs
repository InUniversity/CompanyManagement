using System;
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
                id = (string)reader[BaseDao.EMPLOYEE_ID];
                name = (string)reader[BaseDao.EMPLOYEE_NAME];
                gender = (string)reader[BaseDao.EMPLOYEE_GENDER];
                birthday = (string)reader[BaseDao.EMPLOYEE_BIRTHDAY];
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
                Console.WriteLine(ex.Message);
            }
        }
    }
}