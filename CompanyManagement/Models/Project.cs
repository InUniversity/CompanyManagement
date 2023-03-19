using System;
using System.Data;
using System.Data.SqlClient;
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

        public Project(SqlDataReader reader)
        {
            try
            {
                id = (string)reader[ProjectDao.ID];
                name = (string)reader[ProjectDao.NAME];
                start = (string)reader[ProjectDao.START];
                end = (string)reader[ProjectDao.END];
                progress = (string)reader[ProjectDao.PROPRESS];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
