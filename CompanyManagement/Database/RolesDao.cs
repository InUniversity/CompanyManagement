﻿using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class RolesDao : BaseDao
    {
        public List<Role> GetAll()
        {
            string sqlStr = $"SELECT * FROM {roleTbl}";
            return dbConnection.GetList(sqlStr, reader => new Role(reader));
        }

        public Role SearchByID(string roleID)
        {
            string sqlStr = $"SELECT * FROM {roleTbl} WHERE {BaseDao.roleID}='{roleID}'";
            return (Role)dbConnection.GetSingleObject(sqlStr, reader => new Role(reader));
        }
    }
}
