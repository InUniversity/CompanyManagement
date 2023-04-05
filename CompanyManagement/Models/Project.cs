﻿using System;
using System.Data;
using CompanyManagement.Database.Base;
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

        public string ID => id;

        public string Name => name;

        public DateTime Start => start;

        public DateTime End => end;

        public DateTime Completed => completed;

        public string Progress => progress;

        public string StatusID => statusID;

        public string CreateBy => createBy;

        public Project() { }

        public Project(string id, string name, DateTime start, DateTime end, 
            DateTime completed, string progress, string statusID, string createBy)
        {
            this.id = id;
            this.name = name;
            this.start = start;
            this.end = end;
            this.completed = completed;
            this.progress = progress;
            this.statusID = statusID;
            this.createBy = createBy;
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
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Project), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
