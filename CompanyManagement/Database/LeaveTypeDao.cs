using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using System.Collections.Generic;

namespace CompanyManagement.Database
{
    public class LeaveTypeDao : BaseDao
    {
        public List<LeaveType> GetAll()
        {
            string sqlStr = $"SELECT * FROM {LEAVE_TYPE_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new LeaveType(reader));
        }
    }
}
