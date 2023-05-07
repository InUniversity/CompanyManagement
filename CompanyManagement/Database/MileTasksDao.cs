using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class MileTasksDao : BaseDao
    {
        public void Add(MileTask mileTsk)
        {
            string sqlStr = $"INSERT INTO {mileTsksTbl} ({mileTskID}, {mileTskTskID}) " +
                            $"VALUES ('{mileTsk.ID}', '{mileTsk.TskID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(MileTask mileTsk)
        {
            string sqlStr = $"DELETE FROM {mileTsksTbl} " +
                            $"WHERE {mileTskID}='{mileTsk.ID}' AND {mileTskTskID}='{mileTsk.TskID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<TaskInProject> SearchByMileID(string mileID)
        {
            string sqlStr = $"SELECT * FROM {taskTbl} WHERE {taskID} IN " +
                            $"(SELECT {mileTskTskID} FROM {mileTsksTbl} WHERE {mileTskID}='{mileID}')";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }
    }
}
