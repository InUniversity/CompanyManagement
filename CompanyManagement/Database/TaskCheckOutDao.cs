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

        public List<TaskCheckOut> SearchByProjectID(string projectID)
        {
            string sqlStr = $"SELECT * FROM {TASK_CHECK_OUT_TABLE} WHERE {TASK_CHECK_OUT_TASK_ID} IN " +
                            $"(SELECT {TASK_ID} FROM {TASK_TABLE} T WHERE T.{TASK_PROJECT_ID} LIKE {projectID})";
            return dbConnection.GetList(sqlStr, reader => new TaskCheckOut(reader));
        }
    }
}