using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CompanyManagement.Database;

namespace CompanyManagement.Models
{
    public class TaskInProject
    {
        private string id;
        private string name;
        private string start;
        private string end;
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

        public string Start
        {
            get { return this.start; }
            set { this.start = value; }
        }

        public string End
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

        public TaskInProject(string id, string name, string start, string end, string employeeID)
        {
            this.id = id;
            this.name = name;
            this.start = start;
            this.end = end;
            this.employeeID = employeeID;
        }

        public TaskInProject(DataRow row)
        {
            try
            {
                this.id = (string)row[TaskInProjectDao.ID];
                this.name = (string)row[TaskInProjectDao.NAME];
                this.start = (string)row[TaskInProjectDao.START];
                this.end = (string)row[TaskInProjectDao.END];
                this.employeeID = (string)row[TaskInProjectDao.EMPLOYEE_ID];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
