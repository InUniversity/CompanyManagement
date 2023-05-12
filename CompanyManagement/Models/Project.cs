using System;
using System.Collections.ObjectModel;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Enums;
using CompanyManagement.Models;
using CompanyManagement.Utilities;

namespace CompanyManagement
{
    public class Project
    {
        private string id;
        private string name;
        private string details;
        private DateTime createdDate;
        private DateTime startDate;
        private DateTime endDate;
        private DateTime completedDate;
        private string progress;
        private EProjStatus status;
        private string ownerID;
        private decimal bonusSalary;
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

        public string Details
        {
            get => details;
            set => details = value;
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

        public EProjStatus Status
        {
            get => status;
            set => status = value;
        }

        public string OwnerID
        {
            get => ownerID;
            set => ownerID = value;
        }

        public decimal BonusSalary
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

        public Project(string id, string name, string details, DateTime createdDate, DateTime startDate, 
            DateTime endDate, DateTime completedDate, string progress, EProjStatus status, string ownerID, 
            decimal bonusSalary, ObservableCollection<Department> departments)
        {
            this.id = id;
            this.name = name;
            this.details = details;
            this.createdDate = createdDate;
            this.startDate = startDate;
            this.endDate = endDate;
            this.completedDate = completedDate;
            this.progress = progress;
            this.status = status;
            this.ownerID = ownerID;
            this.bonusSalary = bonusSalary;
            this.departments = departments;
        }

        public Project(IDataRecord reader)
        {
            try
            {
                id = Utils.GetString(reader, BaseDao.projID);
                name = Utils.GetString(reader, BaseDao.projName);
                details = Utils.GetString(reader, BaseDao.projDetails);
                createdDate = Utils.GetDateTime(reader, BaseDao.projCreated);
                startDate = Utils.GetDateTime(reader, BaseDao.projStart);
                endDate = Utils.GetDateTime(reader, BaseDao.projEnd);
                completedDate = Utils.GetDateTime(reader, BaseDao.projCompleted);
                progress = Utils.GetString(reader, BaseDao.projProgress);
                status = (EProjStatus)Utils.GetInt(reader, BaseDao.projStatusID);
                ownerID = Utils.GetString(reader, BaseDao.projOwnerID);
                bonusSalary = Utils.GetDecimal(reader, BaseDao.projBonus);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Project), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
