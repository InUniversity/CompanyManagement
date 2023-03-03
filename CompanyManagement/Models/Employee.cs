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
        private string mgr_id;
        private int salary;
        private string address;

        public string ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string Ssn { get; set; }
        public string Phone { get; set; }
        public string Mgr_ID { get; set; }
        public int Salary { get; set; }
        public string Address { get; set; }
    }
}