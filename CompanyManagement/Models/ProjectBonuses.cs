using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class ProjectBonuses
    {
        private string id;
        private decimal amount;
        private DateTime receivedDate;
        private string employeeID;
        private string projectID;
        private Employee receiver;
        private int percent = 0;

        public string ID
        {
            get => id;
            set => id = value;
        }

        public decimal Amount
        {
            get => amount;
            set => amount = value;
        }

        public DateTime ReceivedDate
        {
            get => receivedDate;
            set => receivedDate = value;
        }

        public string EmployeeID
        {
            get => employeeID;
            set => employeeID = value;
        }

        public string ProjectID
        {
            get => projectID;
            set => projectID = value;
        }

        public Employee Receiver
        {
            get => receiver;
            set => receiver = value;
        }

        public int Percent
        {
            get => percent;
            set => percent = value;
        }

        public ProjectBonuses(){}

        public ProjectBonuses(string id, decimal amount, DateTime receivedDate, string employeeID, string projectID)
        {
            this.id = id;
            this.amount = amount;
            this.receivedDate = receivedDate;
            this.employeeID = employeeID;
            this.projectID = projectID;
        }
        
        public ProjectBonuses(IDataRecord reader)
        {
            try
            {
                id = Utils.GetString(reader, BaseDao.projBonusID);
                amount = Utils.GetDecimal(reader, BaseDao.projBonusAmount);
                receivedDate = Utils.GetDateTime(reader, BaseDao.projBonusReceiveDate);
                employeeID = Utils.GetString(reader, BaseDao.projBonusEmplID);
                projectID = Utils.GetString(reader, BaseDao.projBonusProjID);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(ProjectStatus), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
