using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement
{
    public class Employee
    {
        private string id;
        private string name;
        private string gender;
        private string birthday;
        private string ssn;
        private string phone;
        private string mgrid;
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
        public string Ssn
        {
            get { return this.ssn; }
            set { this.ssn = value; }
        }
        public string Phone
        {
            get { return this.phone; }
            set { this.phone = value; }
        }
        public string MgrID
        {
            get { return this.mgrid; }
            set { this.mgrid = value; }
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
        public Employee(string id, string name, string gender, string birthday, string ssn, string phone, string mgrid, int salary, string address)
        {
            this.id = id;
            this.name = name;
            this.gender = gender;
            this.birthday = birthday;
            this.ssn = ssn;
            this.phone = phone;
            this.mgrid = mgrid;
            this.salary = salary;
            this.address = address;
        }
        ~Employee() { }
    }
}