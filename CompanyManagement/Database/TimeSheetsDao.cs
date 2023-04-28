using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class TimeSheetsDao : BaseDao
    {
        public void Add(TimeSheet timeSheet)
        {
            string sqlStr = $"INSERT INTO {TIME_SHEETS_TABLE} ({TIME_SHEETS_ID}, {TIME_SHEETS_EMPLOYEE_ID}," +
                            $" {TIME_SHEETS_CHECK_IN_TIME}, {TIME_SHEETS_CHECK_OUT_TIME}, {TIME_SHEETS_TASK_CHECK_IN_ID})" +
                            $"VALUES ('{timeSheet.ID}', '{timeSheet.EmployeeID}', '{timeSheet.CheckInTime}', " +
                            $"'{timeSheet.CheckOutTime}', '{timeSheet.TaskCheckInID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {TIME_SHEETS_TABLE} WHRERE {TIME_SHEETS_ID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(TimeSheet timeSheet)
        {
            string sqlStr = $"UPDATE {TIME_SHEETS_TABLE} " +
                            $"SET {TIME_SHEETS_EMPLOYEE_ID} = '{timeSheet.EmployeeID}', " +
                            $"{TIME_SHEETS_CHECK_IN_TIME} = '{timeSheet.CheckInTime}', " +
                            $"{TIME_SHEETS_CHECK_OUT_TIME} = '{timeSheet.CheckOutTime}', " +
                            $"{TIME_SHEETS_TASK_CHECK_IN_ID} = '{timeSheet.TaskCheckInID}' " +
                            $"WHERE {TIME_SHEETS_ID} = '{timeSheet.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr); 
        }

        public List<TimeSheet> GetAll()
        {
            string sqlStr = $"SELECT * FROM {TIME_SHEETS_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new TimeSheet(reader)); 
        }

        public TimeSheet SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {TIME_SHEETS_TABLE} C WHERE C.{TIME_SHEETS_ID}='{id}'";
            return (TimeSheet)dbConnection.GetSingleObject(sqlStr, reader => new TimeSheet(reader));
        }
    }
}
