﻿using CompanyManagement.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CompanyManagement.Models
{
    public class ProjectStatus
    {
        private string id;
        private string name;

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

        public ProjectStatus() { }

        public ProjectStatus(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public ProjectStatus(SqlDataReader reader)
        {
            try
            {
                id = (string)reader[BaseDao.PROJECT_STATUS_ID];
                name = (string)reader[BaseDao.PROJECT_STATUS_NAME];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}