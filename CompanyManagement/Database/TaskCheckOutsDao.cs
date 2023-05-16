using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class TaskCheckOutsDao : BaseDao
    {
        public void Add(TaskCheckOut tskCheckOut)
        {
            string sqlStr = $"INSERT INTO {taskOutTbl} ({taskOutTimeShtID}, {taskOutTaskID}, {taskOutUpdate}, " +
                            $"{taskOutProgress}) VALUES ('{tskCheckOut.TimeShtID}', '{tskCheckOut.TaskID}', " +
                            $"'{tskCheckOut.Update}', '{tskCheckOut.Progress}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<TaskCheckOut> SearchByProjectID(string projID)
        {
            string sqlStr = $"SELECT * FROM {taskOutTbl} WHERE {taskOutTaskID} IN " +
                            $"(SELECT {taskID} FROM {taskTbl} T WHERE T.{taskProjID} LIKE '{projID}')";
            return dbConnection.GetList(sqlStr, reader => new TaskCheckOut(reader));
        }
    }
}