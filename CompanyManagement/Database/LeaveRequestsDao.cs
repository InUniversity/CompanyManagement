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
            string sqlStr = $"INSERT INTO {leavTbl}({leavID},{leavReason},{leavNotes},{leavCreated}," +
                            $"{leavStart},{leavEnd},{leavStatusID},{leavEmplID}, " +
                            $"{leavApproverID}) VALUES ('{request.ID}',N'{request.Reason}',N'{request.Notes}'," +
                            $"'{Utils.ToSQLFormat(request.CreatedDate)}','{Utils.ToSQLFormat(request.StartDate)}'," +
                            $"'{Utils.ToSQLFormat(request.EndDate)}','{request.StatusID}'," +
                            $"'{request.EmployeeID}','{request.ApproverID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string leaveID)
        {
            string sqlStr = $"DELETE FROM {leavTbl} WHERE {leavID}='{leaveID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(LeaveRequest request)
        {
            string sqlStr = $"UPDATE {leavTbl} SET " +
                            $"{leavReason}=N'{request.Reason}', {leavNotes}=N'{request.Notes}', " +
                            $"{leavCreated}='{Utils.ToSQLFormat(request.CreatedDate)}', " +
                            $"{leavStart}='{Utils.ToSQLFormat(request.StartDate)}', " +
                            $"{leavEnd}='{Utils.ToSQLFormat(request.EndDate)}', " +
                            $"{leavStatusID}='{request.StatusID}', {leavEmplID}='{request.EmployeeID}', " +
                            $"{leavApproverID}='{request.ApproverID}' WHERE {leavID} = '{request.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<LeaveRequest> GetAll()
        {
            string sqlStr = $"SELECT * FROM {leavTbl}";
            return dbConnection.GetList(sqlStr, reader => new LeaveRequest(reader));
        }

        public LeaveRequest SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {leavTbl} WHERE {leavID}='{id}'";
            return (LeaveRequest)dbConnection.GetSingleObject(sqlStr, reader => new LeaveRequest(reader));
        }

        public List<LeaveRequest> SearchByEmployeeID(string id)
        {
            string sqlStr = $"SELECT * FROM {leavTbl} WHERE {leavEmplID}='{id}'";
            return dbConnection.GetList(sqlStr, reader => new LeaveRequest(reader));
        }

        public List<LeaveRequest> SearchByApproverID(string id)
        {
            string sqlStr = $"SELECT * FROM {leavTbl} WHERE {leavApproverID}='{id}'";
            return dbConnection.GetList(sqlStr, reader => new LeaveRequest(reader));
        }
    }
}
