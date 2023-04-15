using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using System.Collections.Generic;

namespace CompanyManagement.Database
{
    public class LeaveTypeDao: BaseDao
    {
        public void Add(LeaveType leaveType)
        {
            string sqlStr = $"INSERT INTO {LEAVE_TYPE_TABLE} ({LEAVE_TYPE_ID}, {LEAVE_TYPE_NAME})" +
                            $"VALUES ({leaveType.ID}, {leaveType.Name})";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string leaveTypeID)
        {
            string sqlStr = $"DELETE FROM {LEAVE_TYPE_TABLE} WHERE {LEAVE_TYPE_ID}='{leaveTypeID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(LeaveType leaveType)
        {
            string sqlStr = $"UPDATE {LEAVE_TYPE_TABLE} SET {LEAVE_TYPE_NAME}='{leaveType.Name}'" +
                            $"WHERE {LEAVE_TYPE_ID}='{leaveType.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<LeaveType> GetAll()
        {
            string sqlStr = $"SELECT * FROM {LEAVE_TYPE_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new LeaveType(reader));
        }
    }
}
