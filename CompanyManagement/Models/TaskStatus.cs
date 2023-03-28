﻿using CompanyManagement.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CompanyManagement.Models
{
    public class TaskStatus
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

        public TaskStatus() { }

        public TaskStatus(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public TaskStatus(SqlDataReader reader)
        {
            try
            {
                id = (string)reader[BaseDao.TASK_STATUS_ID];
                name = (string)reader[BaseDao.TASK_STATUS_NAME];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}