using System;
using System.Data;
using System.Windows;
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

        public string Birthday
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

        public Employee(DataRow row)
        {
            try
            {
                id = (string)row[EmployeeDao.ID];
                name = (string)row[EmployeeDao.NAME];
                gender = (string)row[EmployeeDao.GENDER];
                birthday = (string)row[EmployeeDao.BIRTHDAY];
                identifyCard = (string)row[EmployeeDao.IDENTIFY_CARD];
                email = (string)row[EmployeeDao.EMAIL];
                phoneNumber = (string)row[EmployeeDao.PHONE_NUMBER];
                address = (string)row[EmployeeDao.ADDRESS];
                departmentID = (string)row[EmployeeDao.DEPARTMENT_ID];
                positionID = (string)row[EmployeeDao.POSITION_ID];
                salary = (int)row[EmployeeDao.SALARY];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}