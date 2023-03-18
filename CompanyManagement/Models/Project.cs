using System;
using System.Data;
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
            get { return id; }
            set { id = value; }
        } 
            
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Start
        {
            get { return start; }
            set { start = value; }
        }

        public string End
        {
            get { return end; }
            set { end = value; }
        }

        public string Progress
        {
            get { return progress; }
            set { progress = value; }
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
                id = (string)row[ProjectDao.ID];
                name = (string)row[ProjectDao.NAME];
                start = (string)row[ProjectDao.START];
                end = (string)row[ProjectDao.END];
                progress = (string)row[ProjectDao.PROPRESS];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
