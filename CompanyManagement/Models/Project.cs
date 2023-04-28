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
        private DateTime createdDate;
        private DateTime startDate;
        private DateTime endDate;
        private DateTime completedDate;
        private string progress;
        private string statusID;
        private string ownerID;
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

        public DateTime CreatedDate
        {
            get => createdDate;
            set => createdDate = value;
        }

        public DateTime StartDate
        {
            get => startDate;
            set => startDate = value;
        }

        public DateTime EndDate
        {
            get => endDate;
            set => endDate = value;
        }

        public DateTime CompletedDate
        {
            get => completedDate;
            set => completedDate = value;
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

        public string OwnerID
        {
            get => ownerID;
            set => ownerID = value;
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

        public Project(string id, string name, DateTime createdDate, DateTime startDate, DateTime endDate, 
            DateTime completedDate, string progress, string statusID, string ownerID, int bonusSalary,
            ObservableCollection<Department> departments)
        {
            this.id = id;
            this.name = name;
            this.createdDate = createdDate;
            this.startDate = startDate;
            this.endDate = endDate;
            this.completedDate = completedDate;
            this.progress = progress;
            this.statusID = statusID;
            this.ownerID = ownerID;
            this.bonusSalary = bonusSalary;
            this.departments = departments;
        }

        public Project(IDataRecord reader)
        {
            try
            {
                id = (string)reader[BaseDao.PROJECTS_ID];
                name = (string)reader[BaseDao.PROJECTS_NAME];
                createdDate = reader.GetDateTime(reader.GetOrdinal(BaseDao.PROJECTS_CREATED));
                startDate = reader.GetDateTime(reader.GetOrdinal(BaseDao.PROJECTS_START));
                endDate = reader.GetDateTime(reader.GetOrdinal(BaseDao.PROJECTS_END));
                completedDate = reader.GetDateTime(reader.GetOrdinal(BaseDao.PROJECTS_COMPLETED));
                progress = (string)reader[BaseDao.PROJECTS_PROPRESS];
                statusID = (string)reader[BaseDao.PROJECTS_STATUS_ID];
                ownerID = (string)reader[BaseDao.PROJECTS_OWNER_ID];
                bonusSalary = (int)reader[BaseDao.PROJECTS_BONUS_SALARY];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Project), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
