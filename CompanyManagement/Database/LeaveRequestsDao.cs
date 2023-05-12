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
                            $"{leavStart},{leavEnd},{leavStatusID},{leavEmplID}, {leavApproverID}, {leavResponse}) " +
                            $"VALUES ('{request.ID}',N'{request.Reason}',N'{request.Notes}'," +
                            $"'{Utils.ToSQLFormat(request.Created)}','{Utils.ToSQLFormat(request.Start)}'," +
                            $"'{Utils.ToSQLFormat(request.End)}','{(int)request.Status}'," +
                            $"'{request.RequesterID}','{request.ApproverID}', N'{request.Response}')";
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
                            $"{leavCreated}='{Utils.ToSQLFormat(request.Created)}', " +
                            $"{leavStart}='{Utils.ToSQLFormat(request.Start)}', " +
                            $"{leavEnd}='{Utils.ToSQLFormat(request.End)}', " +
                            $"{leavStatusID}='{(int)request.Status}', {leavEmplID}='{request.RequesterID}', " +
                            $"{leavApproverID}='{request.ApproverID}', {leavResponse} = N'{request.Response}'" +
                            $"WHERE {leavID} = '{request.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public LeaveRequest SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {leavTbl} WHERE {leavID}='{id}'";
            return (LeaveRequest)dbConnection.GetSingleObject(sqlStr, reader => new LeaveRequest(reader));
        }

        public List<LeaveRequest> GetMyRequests(string emplID)
        {
            string sqlStr = $"SELECT * FROM {leavTbl} WHERE {leavEmplID}='{emplID}'";
            return dbConnection.GetList(sqlStr, reader => new LeaveRequest(reader));
        }

        public List<LeaveRequest> SearchByApproverID(string emplID)
        {
            string sqlStr = $"SELECT * FROM {leavTbl} WHERE {leavApproverID}='{emplID}'";
            return dbConnection.GetList(sqlStr, reader => new LeaveRequest(reader));
        }
    }
}
