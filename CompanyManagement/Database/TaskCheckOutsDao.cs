using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class TaskCheckOutsDao : BaseDao
    {
        public void Add(TaskCheckOut taskCheckOut)
        {
            string sqlStr = $"INSERT INTO {taskOutTbl} ({taskOutTimeShtID}, {taskOutTaskID}," +
                            $" {taskOutUpdate}, {taskOutProgress}) " +
                            $"VALUES ('{taskCheckOut.CheckInOutID}', '{taskCheckOut.TaskID}', " +
                            $"'{taskCheckOut.UpdateDate}', '{taskCheckOut.Progress}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<TaskCheckOut> SearchByProjectID(string projectID)
        {
            string sqlStr = $"SELECT * FROM {taskOutTbl} WHERE {taskOutTaskID} IN " +
                            $"(SELECT {taskID} FROM {taskTbl} T WHERE T.{taskProjID} LIKE '{projectID}')";
            return dbConnection.GetList(sqlStr, reader => new TaskCheckOut(reader));
        }
    }
}