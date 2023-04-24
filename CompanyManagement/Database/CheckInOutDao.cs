using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class CheckInOutDao : BaseDao
    {
        public void Add(CheckInOut checkInOut)
        {
            string sqlStr = $"INSERT INTO {CHECK_IN_OUT_TABLE} ({CHECK_IN_OUT_ID}, {CHECK_IN_OUT_EMPLOYEE_ID}," +
                            $" {CHECK_IN_OUT_CHECK_IN_TIME}, {CHECK_IN_OUT_CHECK_OUT_TIME}, {CHECK_IN_OUT_TASK_CHECK_IN_ID})" +
                            $"VALUES ('{checkInOut.ID}', '{checkInOut.EmployeeID}', '{checkInOut.CheckInTime}', " +
                            $"'{checkInOut.CheckOutTime}', '{checkInOut.TaskCheckInID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {CHECK_IN_OUT_TABLE} WHRERE {CHECK_IN_OUT_ID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(CheckInOut checkInOut)
        {
            string sqlStr = $"UPDATE {CHECK_IN_OUT_TABLE} " +
                            $"SET {CHECK_IN_OUT_EMPLOYEE_ID} = '{checkInOut.EmployeeID}', " +
                            $"{CHECK_IN_OUT_CHECK_IN_TIME} = '{checkInOut.CheckInTime}', " +
                            $"{CHECK_IN_OUT_CHECK_OUT_TIME} = '{checkInOut.CheckOutTime}', " +
                            $"{CHECK_IN_OUT_TASK_CHECK_IN_ID} = '{checkInOut.TaskCheckInID}' " +
                            $"WHERE {CHECK_IN_OUT_ID} = '{checkInOut.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr); 
        }

        public List<CheckInOut> GetAll()
        {
            string sqlStr = $"SELECT * FROM {CHECK_IN_OUT_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new CheckInOut(reader)); 
        }

        public CheckInOut SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {CHECK_IN_OUT_TABLE} C WHERE C.{CHECK_IN_OUT_ID}='{id}'";
            return (CheckInOut)dbConnection.GetSingleObject(sqlStr, reader => new CheckInOut(reader));
        }
    }
}
