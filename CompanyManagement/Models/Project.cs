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
        private string progress;

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

        public string Progress
        {
            get { return this.progress; }
            set { this.progress = value; }
        }

        public Project() { }

        public Project(string id, string name, string start, string end, string progress)
        {
            this.id = id;
            this.name = name;
            this.start = start;
            this.end = end;
            this.progress = progress;
        }

        public Project(DataRow row)
        {
            try
            {
                this.id = (string)row[ProjectDao.ID];
                this.name = (string)row[ProjectDao.NAME];
                this.start = (string)row[ProjectDao.START];
                this.end = (string)row[ProjectDao.END];
                this.progress = (string)row[ProjectDao.PROPRESS];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
