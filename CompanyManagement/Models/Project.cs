using System;
using System.Data.SqlClient;
using CompanyManagement.Database;
using CompanyManagement.Utilities;

namespace CompanyManagement
{
    public class Project
    {
        private string id = "";
        private string name = "";
        private DateTime start = DateTime.Now;
        private DateTime end = DateTime.Now;
        private DateTime completed = DateTime.Now;
        private string progress = "0";
        private string statusID;

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

        public DateTime Start
        {
            get { return start; }
            set { start = value; }
        }

        public DateTime End
        {
            get { return end; }
            set { end = value; }
        }

        public DateTime Completed
        {
            get { return completed; }
            set { completed = value; }
        }

        public string Progress
        {
            get { return progress; }
            set { progress = value; }
        }

        public string StatusID
        {
            get { return statusID; }
            set { statusID = value; }
        }

        public Project() { }

        public Project(string id, string name, DateTime start, DateTime end, 
            DateTime completed, string progress, string statusID)
        {
            this.id = id;
            this.name = name;
            this.start = start;
            this.end = end;
            this.completed = completed;
            this.progress = progress;
            this.statusID = statusID;
        }

        public Project(string id)
        {
            this.id = id;
        }

        public Project(SqlDataReader reader)
        {
            try
            {
                id = (string)reader[BaseDao.PROJECT_ID];
                name = (string)reader[BaseDao.PROJECT_NAME];
                start = reader.GetDateTime(reader.GetOrdinal(BaseDao.PROJECT_START));
                end = reader.GetDateTime(reader.GetOrdinal(BaseDao.PROJECT_END));
                completed = reader.GetDateTime(reader.GetOrdinal(BaseDao.PROJECT_COMPLETED));
                progress = (string)reader[BaseDao.PROJECT_PROPRESS];
                statusID = (string)reader[BaseDao.PROJECT_STATUS_ID];
            }
            catch(Exception ex)
            {
                Log.Instance.Error(nameof(Project), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
