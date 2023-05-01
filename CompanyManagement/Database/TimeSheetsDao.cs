using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class TimeSheetsDao : BaseDao
    {
        public void Add(TimeSheet timeSheet)
        {
            string sqlStr = $"INSERT INTO {timeShtTbl} ({timeShtID}, {timeShtEmplID}, {timeShtInTime}, " +
                            $"{timeShtOutTime}, {timeShtTaskInID}) VALUES ('{timeSheet.ID}', " +
                            $"'{timeSheet.EmployeeID}', '{timeSheet.CheckInTime}', " +
                            $"'{timeSheet.CheckOutTime}', '{timeSheet.TaskCheckInID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {timeShtTbl} WHERE {timeShtID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(TimeSheet timeSheet)
        {
            string sqlStr = $"UPDATE {timeShtTbl} SET {timeShtEmplID} = '{timeSheet.EmployeeID}', " +
                            $"{timeShtInTime} = '{timeSheet.CheckInTime}', {timeShtOutTime} = '{timeSheet.CheckOutTime}', " +
                            $"{timeShtTaskInID} = '{timeSheet.TaskCheckInID}' WHERE {timeShtID} = '{timeSheet.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr); 
        }

        public List<TimeSheet> GetAll()
        {
            string sqlStr = $"SELECT * FROM {timeShtTbl}";
            return dbConnection.GetList(sqlStr, reader => new TimeSheet(reader)); 
        }

        public TimeSheet SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {timeShtTbl} WHERE {timeShtID}='{id}'";
            return (TimeSheet)dbConnection.GetSingleObject(sqlStr, reader => new TimeSheet(reader));
        }
    }
}
