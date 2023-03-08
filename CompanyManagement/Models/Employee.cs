using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CompanyManagement.Database;
using CompanyManagement.ViewModels;

namespace CompanyManagement
{
    public class Employee
    {
        private string id;
        private string name;
        private string gender;
        private string birthday;
        private string identifyCard;
        private string phoneNumber;
        private string managerID;
        private int salary;
        private string address;

        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Gender
        {
            get { return this.gender; }
            set { this.gender = value; }
        }

        public string Birthday
        {
            get { return this.birthday; }
            set { this.birthday = value; }
        }

        public string IndentifyCard
        {
            get { return this.identifyCard; }
            set { this.identifyCard = value; }
        }

        public string PhoneNumber
        {
            get { return this.phoneNumber; }
            set { this.phoneNumber = value; }
        }

        public string ManagerID
        {
            get { return this.managerID; }
            set { this.managerID = value; }
        }

        public int Salary
        {
            get { return this.salary; }
            set { this.salary = value; }
        }

        public string Address
        {
            get { return this.address; }
            set { this.address = value; }
        }

        public Employee() { }

        public Employee(string id, string name, string gender, string birthday, string identifyCard, string phone, string managerID, int salary, string address)
        {
            this.id = id;
            this.name = name;
            this.gender = gender;
            this.birthday = birthday;
            this.identifyCard = identifyCard;
            this.phoneNumber = phone;
            this.managerID = managerID;
            this.salary = salary;
            this.address = address;
        }

        public Employee(DataRow row)
        {
            try
            {
                this.id = (string)row[EmployeeDao.ID];
                this.name = (string)row[EmployeeDao.NAME];
                this.gender = (string)row[EmployeeDao.GENDER];
                this.birthday = (string)row[EmployeeDao.BIRTHDAY];
                this.identifyCard = (string)row[EmployeeDao.IDENTIFY_CARD];
                this.phoneNumber = (string)row[EmployeeDao.PHONE_NUMBER];
                this.managerID = (string)row[EmployeeDao.MANAGER_ID];
                this.salary = (int)row[EmployeeDao.SALARY];
                this.address = (string)row[EmployeeDao.ADDRESS];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}