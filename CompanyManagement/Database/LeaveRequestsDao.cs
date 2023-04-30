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
            string sqlStr = $"INSERT INTO {LEAVE_TABLE}({LEAVE_ID},{LEAVE_REASON},{LEAVE_NOTES},{LEAVE_CREATED_DATE}," +
                            $"{LEAVE_START_DATE},{LEAVE_END_DATE},{LEAVE_STATUS_ID},{LEAVE_EMPLOYEE_ID}, " +
                            $"{LEAVE_APPROVER_ID}) VALUES ('{request.ID}',N'{request.Reason}',N'{request.Notes}'," +
                            $"'{Utils.ToSQLFormat(request.CreatedDate)}','{Utils.ToSQLFormat(request.StartDate)}'," +
                            $"'{Utils.ToSQLFormat(request.EndDate)}','{request.StatusID}'," +
                            $"'{request.EmployeeID}','{request.ApproverID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string leaveID)
        {
            string sqlStr = $"DELETE FROM {LEAVE_TABLE} WHERE {LEAVE_ID}='{leaveID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(LeaveRequest request)
        {
            string sqlStr = $"UPDATE {LEAVE_TABLE} SET " +
                            $"{LEAVE_REASON}=N'{request.Reason}', {LEAVE_NOTES}=N'{request.Notes}', " +
                            $"{LEAVE_CREATED_DATE}='{Utils.ToSQLFormat(request.CreatedDate)}', " +
                            $"{LEAVE_START_DATE}='{Utils.ToSQLFormat(request.StartDate)}', " +
                            $"{LEAVE_END_DATE}='{Utils.ToSQLFormat(request.EndDate)}', " +
                            $"{LEAVE_STATUS_ID}='{request.StatusID}', {LEAVE_EMPLOYEE_ID}='{request.EmployeeID}', " +
                            $"{LEAVE_APPROVER_ID}='{request.ApproverID}' WHERE {LEAVE_ID} = '{request.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<LeaveRequest> GetAll()
        {
            string sqlStr = $"SELECT * FROM {LEAVE_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new LeaveRequest(reader));
        }

        public LeaveRequest SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {LEAVE_TABLE} WHERE {LEAVE_ID}='{id}'";
            return (LeaveRequest)dbConnection.GetSingleObject(sqlStr, reader => new LeaveRequest(reader));
        }

        public List<LeaveRequest> SearchByEmployeeID(string id)
        {
            string sqlStr = $"SELECT * FROM {LEAVE_TABLE} WHERE {LEAVE_EMPLOYEE_ID}='{id}'";
            return dbConnection.GetList(sqlStr, reader => new LeaveRequest(reader));
        }

        public List<LeaveRequest> SearchByApproverID(string id)
        {
            string sqlStr = $"SELECT * FROM {LEAVE_TABLE} WHERE {LEAVE_APPROVER_ID}='{id}'";
            return dbConnection.GetList(sqlStr, reader => new LeaveRequest(reader));
        }
    }
}
