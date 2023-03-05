using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Models
{
    public class TaskInProject
    {
        private string id;
        private string name;
        private DateTime start;
        private DateTime end;
        private string employeeID;

        public string ID
        {
            get{ return this.id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public DateTime Start
        {
            get { return this.start; }
            set { this.start = value; }
        }

        public DateTime End
        {
            get { return this.end; }
            set { this.end = value; }
        }

        public string EmployeeID
        {
            get { return this.employeeID; }
            set { this.employeeID = value; }
        }

        public TaskInProject() { }

        public TaskInProject(string id, string name, DateTime start, DateTime end, string employeeID)
        {
            this.id = id;
            this.name = name;
            this.start = start;
            this.end = end;
            this.employeeID = employeeID;
        }
    }
}
