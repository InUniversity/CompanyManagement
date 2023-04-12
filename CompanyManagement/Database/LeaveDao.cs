using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Database
{
    public class LeaveDao: BaseDao
    {
        public void Add(Leave leave)
        {
            string sqlStr = $"INSERT INTO {LEAVE_TABLE} ({LEAVE_ID}, {LEAVE_EMPLOYEE_ID}, {LEAVE_TYPE_ID}, " +
                            $"{LEAVE_REASON}, {LEAVE_START_DATE}, {LEAVE_END_DATE}, {LEAVE_STATUS_ID}, " +
                            $"{LEAVE_CREATED_DATE}, {LEAVE_APPROVED_BY}, {LEAVE_NOTE})" +
                            $"VALUES ('{leave.ID}', '{leave.EmployeeID}', '{leave.LeaveTypeID}', " +
                            $"'{leave.LeaveReason}', '{leave.Start}', '{leave.End}', '{leave.LeaveStatusID}', " +
                            $"'{leave.CreateDate}', '{leave.ApprovedBy}', '{leave.Note})'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string leaveID)
        {
            string sqlStr = $"DELETE FROM {LEAVE_TABLE} WHERE {LEAVE_ID}='{leaveID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Leave leave)
        {
            string sqlStr = $"UPDATE {LEAVE_TABLE} SET {LEAVE_EMPLOYEE_ID}='{leave.EmployeeID}', " +
                            $"{LEAVE_TYPE_ID} = '{leave.LeaveTypeID}', {LEAVE_REASON} = '{leave.LeaveReason}', " +
                            $"{LEAVE_START_DATE} = '{leave.Start}', {LEAVE_END_DATE} = '{leave.End}', " +
                            $"{LEAVE_STATUS_ID} = '{leave.LeaveStatusID}', {LEAVE_CREATED_DATE} = '{leave.CreateDate}', " +
                            $"{LEAVE_APPROVED_BY} = '{leave.ApprovedBy}', {LEAVE_NOTE} = '{LEAVE_NOTE}'" +
                            $"WHERE {LEAVE_ID} = '{leave.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<Leave> GetAll()
        {
            string sqlStr = $"SELECT * FROM {LEAVE_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new Leave(reader));
        }
    }
}
