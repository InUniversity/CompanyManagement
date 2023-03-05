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

        public TaskInProject(DataRow row)
        {
            try
            {
                this.id = row[TaskInProjectDao.ID].ToString();
                this.name = row[TaskInProjectDao.NAME].ToString();
                this.start = DateTime.ParseExact(row[TaskInProjectDao.START].ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                this.end = DateTime.ParseExact(row[TaskInProjectDao.END].ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                this.employeeID = row[TaskInProjectDao.EMPLOYEE_ID].ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
