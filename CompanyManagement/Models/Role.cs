﻿using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Enums;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class Role
    {
        private string id;
        private EPermission perms;
        private string title;
        private decimal salary;

        public string ID
        {
            get => id;
            set => id = value;
        }

        public EPermission Perms
        {
            get => perms;
            set => perms = value;
        }

        public string Title
        {
            get => title;
            set => title = value;
        }

        public decimal Salary
        {
            get => Decimal.Round(salary, 0);
            set => salary = value;
        }

        public Role() { }
        
        public Role(IDataRecord record)
        {
            try
            {
                id = Utils.GetString(record, BaseDao.roleID);
                perms = (EPermission)Utils.GetInt(record, BaseDao.rolePerms);
                title = Utils.GetString(record, BaseDao.roleTitle);
                salary = Utils.GetDecimal(record, BaseDao.roleSalary);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Role), "CAST ERROR: " + ex.Message);
            }
        }
    }
}