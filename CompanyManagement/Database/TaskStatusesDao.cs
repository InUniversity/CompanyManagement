﻿using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using System.Collections.Generic;

namespace CompanyManagement.Database
{
    public class TaskStatusesDao : BaseDao
    {
        public List<TaskStatus> GetAll()
        {
            string sqlStr = $"SELECT * FROM {taskStasTbl}";
            return dbConnection.GetList(sqlStr, reader => new TaskStatus(reader));
        }
    }
}
