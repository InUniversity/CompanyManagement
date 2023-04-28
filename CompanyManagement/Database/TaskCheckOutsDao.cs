using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class TaskCheckOutsDao : BaseDao
    {
        public void Add(TaskCheckOut taskCheckOut)
        {
            string sqlStr = $"INSERT INTO {TASK_CHECK_OUTS_TABLE} ({TASK_CHECK_OUTS_TIME_SHEET_ID}, {TASK_CHECK_OUTS_TASK_ID}," +
                            $" {TASK_CHECK_OUTS_UPDATE_DATE}, {TASK_CHECK_OUTS_PROGRESS}) " +
                            $"VALUES ('{taskCheckOut.CheckInOutID}', '{taskCheckOut.TaskID}', " +
                            $"'{taskCheckOut.UpdateDate}', '{taskCheckOut.Progress}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<TaskCheckOut> SearchByProjectID(string projectID)
        {
            string sqlStr = $"SELECT * FROM {TASK_CHECK_OUTS_TABLE} WHERE {TASK_CHECK_OUTS_TASK_ID} IN " +
                            $"(SELECT {TASKS_ID} FROM {TASKS_TABLE} T WHERE T.{TASKS_PROJECT_ID} LIKE '{projectID}')";
            return dbConnection.GetList(sqlStr, reader => new TaskCheckOut(reader));
        }
    }
}