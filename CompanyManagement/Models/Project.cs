using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CompanyManagement.Database;

namespace CompanyManagement
{
    public class Project
    {
        private string id;
        private string name;
        private string start;
        private string end;
        private int budget;
        private string status;

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

        public int Budget
        {
            get { return this.budget; }
            set { this.budget = value; }
        }

        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public Project() { }

        public Project(string id, string name, string start, string end, int budget, string status)
        {
            this.id = id;
            this.name = name;
            this.start = start;
            this.end = end;
            this.budget = budget;
            this.status = status;
        }

        public Project(DataRow row)
        {
            try
            {
                this.id = (string)row[ProjectDao.ID];
                this.name = (string)row[ProjectDao.NAME];
                this.start = (string)row[ProjectDao.START];
                this.end = (string)row[ProjectDao.END];
                this.budget = (int)row[ProjectDao.BUDGET];
                this.status = (string)row[ProjectDao.STATUS];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
