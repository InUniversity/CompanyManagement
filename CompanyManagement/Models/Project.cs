using System;
using System.Collections.ObjectModel;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Utilities;

namespace CompanyManagement
{
    public class Project
    {
        private string id;
        private string name;
        private DateTime start;
        private DateTime end;
        private DateTime completed;
        private string progress;
        private string statusID;
        private string createBy;
        private int bonusSalary;
        private ObservableCollection<Department> departments = new ObservableCollection<Department>();

        public string ID
        {
            get => id;
            set => id = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public DateTime Start
        {
            get => start;
            set => start = value;
        }

        public DateTime End
        {
            get => end;
            set => end = value;
        }

        public DateTime Completed
        {
            get => completed;
            set => completed = value;
        }

        public string Progress
        {
            get => progress;
            set => progress = value;
        }

        public string StatusID
        {
            get => statusID;
            set => statusID = value;
        }

        public string CreateBy
        {
            get => createBy;
            set => createBy = value;
        }

        public int BonusSalary
        {
            get => bonusSalary;
            set => bonusSalary = value;
        }

        public ObservableCollection<Department> Departments
        {
            get => departments;
            set => departments = value;
        }

        public Project() { }

        public Project(string id, string name, DateTime start, DateTime end, 
            DateTime completed, string progress, string statusID, string createBy, int bonusSalary,
            ObservableCollection<Department> departments)
        {
            this.id = id;
            this.name = name;
            this.start = start;
            this.end = end;
            this.completed = completed;
            this.progress = progress;
            this.statusID = statusID;
            this.createBy = createBy;
            this.bonusSalary = bonusSalary;
            this.departments = departments;
        }

        public Project(IDataRecord reader)
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
                createBy = (string)reader[BaseDao.PROJECT_CREATE_BY];
                bonusSalary = (int)reader[BaseDao.PROJECT_BONUS_SALARY];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Project), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
