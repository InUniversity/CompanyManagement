﻿using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;
using System;
using System.Data;

namespace CompanyManagement.Models
{
    public class ProjectStatus
    {
        private string id;
        private string name;

        public string ID => id;
        public string Name => name;

        public ProjectStatus(IDataRecord reader)
        {
            try
            {
                id = (string)reader[BaseDao.projStasID];
                name = (string)reader[BaseDao.projStasName];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(ProjectStatus), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
