using System;
using System.Data.SqlClient;
using CompanyManagement.Database;
using CompanyManagement.Models;

namespace CompanyManagement
{
    public class Employee
    {
        private string id = "";
        private string name = "";
        private string gender = "";
        private string birthday = "";
        private string identifyCard = "";
        private string email = "";
        private string phoneNumber = "";
        private string address = "";
        private string departmentID = "";
        private string positionID = "";
        private int salary;
        private Account account = new Account();

        public string ID => id;

        public string Name => name;

        public string Gender => gender;

        public string Birthday => birthday;

        public string IdentifyCard => identifyCard;

        public string Email => email;

        public string PhoneNumber => phoneNumber;

        public string Address => address;

        public string DepartmentID => departmentID;

        public string PositionID => positionID;

        public int Salary => salary;

        public Account EmplAccount => account;

        public Employee() { }

        public Employee(string id, string name, string gender, string birthday, string identifyCard, string email, 
            string phoneNumber, string address, string departmentID, string positionID, int salary, Account account)
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
            this.account = account;
        }

        public Employee(string id)
        {
            this.id = id;
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
                account = new Account((string)reader[BaseDao.ACCOUNT_USERNAME], 
                                    (string)reader[BaseDao.ACCOUNT_PASSWORD]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}