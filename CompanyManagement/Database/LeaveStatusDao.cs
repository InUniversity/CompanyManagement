using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class LeaveStatusDao : BaseDao
    {

        public void Add(LeaveStatus leaveStatus)
        {
            string sqlStr = $"INSERT INTO {LEAVE_STATUS_TABLE} ({LEAVE_STATUS_ID}, {LEAVE_STATUS_NAME})" +
                            $"VALUES ({leaveStatus.ID}, {leaveStatus.Name})";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string leaveStatusID)
        {
            string sqlStr = $"DELETE FROM {LEAVE_STATUS_TABLE} WHERE {LEAVE_STATUS_ID}='{leaveStatusID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(LeaveStatus leaveStatus)
        {
            string sqlStr = $"UPDATE {LEAVE_STATUS_TABLE} SET {LEAVE_STATUS_NAME}='{leaveStatus.Name}'" +
                            $"WHERE {LEAVE_STATUS_ID}='{leaveStatus.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<LeaveStatus> GetAll()
        {
            string sqlStr = $"SELECT * FROM {LEAVE_STATUS_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new LeaveStatus(reader));
        }
    }
}
