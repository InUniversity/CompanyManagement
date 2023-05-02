using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class Milestone
    {
        private string id;
        private string title;
        private string explanation;
        private DateTime start;
        private DateTime end;
        private DateTime completed;
        private string ownerID;
        private string projID;

        public string ID
        {
            get => id;
            set => id = value;
        }

        public string Title
        {
            get => title;
            set => title = value;
        }

        public string Explanation
        {
            get => explanation;
            set => explanation = value;
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

        public string OwnerID
        {
            get => ownerID;
            set => ownerID = value;
        }

        public string ProjID
        {
            get => projID;
            set => projID = value;
        }
        
        public Milestone() { }

        public Milestone(string id, string title, string explanation, DateTime start, DateTime end, 
            DateTime completed, string ownerID, string projID)
        {
            this.id = id;
            this.title = title;
            this.explanation = explanation;
            this.start = start;
            this.end = end;
            this.completed = completed;
            this.ownerID = ownerID;
            this.projID = projID;
        }

        public Milestone(IDataRecord record)
        {
            try
            {
                id = Utils.GetString(record, BaseDao.mileID);
                title = Utils.GetString(record, BaseDao.mileTitle);
                explanation = Utils.GetString(record, BaseDao.mileExplanation);
                start = Utils.GetDateTime(record, BaseDao.mileStart);
                end = Utils.GetDateTime(record, BaseDao.mileEnd);
                completed = Utils.GetDateTime(record, BaseDao.mileCompleted);
                ownerID = Utils.GetString(record, BaseDao.mileOwnerID);
                projID = Utils.GetString(record, BaseDao.mileProjID);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Milestone), "CAST ERROR: " + ex.Message);
            }
        }
    }
}