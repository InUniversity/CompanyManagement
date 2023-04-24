using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class TaskCheckOutDao : BaseDao
    {
        public void Add(TaskCheckOut taskCheckOut)
        {
            string sqlStr = $"INSERT INTO {TASK_CHECK_OUT_TABLE} ({TASK_CHECK_OUT_CHECK_OUT_IN_ID}, {TASK_CHECK_OUT_TASK_ID}," +
                            $" {TASK_CHECK_OUT_UPDATE_DATE}, {TASK_CHECK_OUT_PROGRESS}) " +
                            $"VALUES ('{taskCheckOut.CheckInOutID}', '{taskCheckOut.TaskID}', " +
                            $"'{taskCheckOut.UpdateDate}', '{taskCheckOut.Progress}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(TaskCheckOut taskCheckOut)
        {
            string sqlStr = $"DELETE FROM {TASK_CHECK_OUT_TABLE} " +
                            $"WHERE {TASK_CHECK_OUT_CHECK_OUT_IN_ID} = '{taskCheckOut.CheckInOutID}' " +
                            $"AND {TASK_CHECK_OUT_TASK_ID} = '{taskCheckOut.TaskID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(TaskCheckOut taskCheckOut)
        {
            string sqlStr = $"UPDATE {TASK_CHECK_OUT_TABLE} " +
                            $"SET {TASK_CHECK_OUT_UPDATE_DATE} = '{taskCheckOut.UpdateDate}', " +
                            $"{TASK_CHECK_OUT_PROGRESS} = '{taskCheckOut.Progress}' " +
                            $"WHERE {TASK_CHECK_OUT_CHECK_OUT_IN_ID} = '{taskCheckOut.CheckInOutID}' " +
                            $"AND {TASK_CHECK_OUT_TASK_ID} = '{taskCheckOut.TaskID}'";
            dbConnection.ExecuteNonQuery(sqlStr); 
        }

        public TaskCheckOut SearchByID(TaskCheckOut taskCheckOut)
        {
            string sqlStr = $"SELECT * FROM {TASK_CHECK_OUT_TABLE}  " +
                            $"WHERE {TASK_CHECK_OUT_CHECK_OUT_IN_ID}='{taskCheckOut.CheckInOutID}' " +
                            $"AND {TASK_CHECK_OUT_TASK_ID} = '{taskCheckOut.TaskID}'";
            return (TaskCheckOut)dbConnection.GetSingleObject(sqlStr, reader => new TaskCheckOut(reader));
        }
    }
}