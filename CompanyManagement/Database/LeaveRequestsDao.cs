using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using System.Collections.Generic;
using CompanyManagement.Utilities;

namespace CompanyManagement.Database
{
    public class LeaveRequestsDao : BaseDao
    {
        public void Add(LeaveRequest request)
        {
            string sqlStr = $"INSERT INTO {LEAVES_TABLE}({LEAVES_ID},{LEAVES_REASON},{LEAVES_NOTES},{LEAVES_CREATED_DATE}," +
                            $"{LEAVES_START_DATE},{LEAVES_END_DATE},{LEAVES_STATUS_ID},{LEAVES_EMPLOYEE_ID}, " +
                            $"{LEAVES_APPROVER_ID}) VALUES ('{request.ID}',N'{request.Reason}',N'{request.Notes}'," +
                            $"'{Utils.ToSQLFormat(request.CreatedDate)}','{Utils.ToSQLFormat(request.StartDate)}'," +
                            $"'{Utils.ToSQLFormat(request.EndDate)}','{request.StatusID}'," +
                            $"'{request.EmployeeID}','{request.ApproverID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string leaveID)
        {
            string sqlStr = $"DELETE FROM {LEAVES_TABLE} WHERE {LEAVES_ID}='{leaveID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(LeaveRequest request)
        {
            string sqlStr = $"UPDATE {LEAVES_TABLE} SET " +
                            $"{LEAVES_REASON}=N'{request.Reason}', {LEAVES_NOTES}=N'{request.Notes}', " +
                            $"{LEAVES_CREATED_DATE}='{Utils.ToSQLFormat(request.CreatedDate)}', " +
                            $"{LEAVES_START_DATE}='{Utils.ToSQLFormat(request.StartDate)}', " +
                            $"{LEAVES_END_DATE}='{Utils.ToSQLFormat(request.EndDate)}', " +
                            $"{LEAVES_STATUS_ID}='{request.StatusID}', {LEAVES_EMPLOYEE_ID}='{request.EmployeeID}', " +
                            $"{LEAVES_APPROVER_ID}='{request.ApproverID}' WHERE {LEAVES_ID} = '{request.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<LeaveRequest> GetAll()
        {
            string sqlStr = $"SELECT * FROM {LEAVES_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new LeaveRequest(reader));
        }

        public LeaveRequest SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {LEAVES_TABLE} WHERE {LEAVES_ID}='{id}'";
            return (LeaveRequest)dbConnection.GetSingleObject(sqlStr, reader => new LeaveRequest(reader));
        }

        public List<LeaveRequest> SearchByEmployeeID(string id)
        {
            string sqlStr = $"SELECT * FROM {LEAVES_TABLE} WHERE {LEAVES_EMPLOYEE_ID}='{id}'";
            return dbConnection.GetList(sqlStr, reader => new LeaveRequest(reader));
        }

        public List<LeaveRequest> SearchByApproverID(string id)
        {
            string sqlStr = $"SELECT * FROM {LEAVES_TABLE} WHERE {LEAVES_APPROVER_ID}='{id}'";
            return dbConnection.GetList(sqlStr, reader => new LeaveRequest(reader));
        }
    }
}
