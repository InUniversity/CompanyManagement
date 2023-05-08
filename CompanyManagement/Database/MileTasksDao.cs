using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Utilities;

namespace CompanyManagement.Database
{
    public class MileTasksDao : BaseDao
    {
        public void Add(MileTask mileTsk)
        {
            string sqlStr = $"INSERT INTO {mileTsksTbl} ({mileTskID}, {mileTskTskID}) " +
                            $"VALUES ('{mileTsk.MileID}', '{mileTsk.TskID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(MileTask mileTsk)
        {
            string sqlStr = $"DELETE FROM {mileTsksTbl} " +
                            $"WHERE {mileTskID}='{mileTsk.MileID}' AND {mileTskTskID}='{mileTsk.TskID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void DeleteByMileID(string mileID)
        {
            string sqlStr = $"DELETE FROM {mileTsksTbl} WHERE {mileTskID}='{mileID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<TaskInProject> SearchByMileID(string mileID)
        {
            string sqlStr = $"SELECT * FROM {taskTbl} WHERE {taskID} IN " +
                            $"(SELECT {mileTskTskID} FROM {mileTsksTbl} WHERE {mileTskID}='{mileID}')";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }

        public List<TaskInProject> SearchTaskCompletedByMileID(string mileID)
        {
            string sqlStr = $"SELECT * FROM {taskTbl} WHERE {taskID} IN " +
                            $"(SELECT {mileTskTskID} FROM {mileTsksTbl} WHERE {mileTskID}='{mileID}' AND {taskProgress} = '{completed}')";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }

        public List<TaskInProject> SearchTasksCanAddToMile(Milestone milestone)
        {
            string sqlStr = $"SELECT * FROM {taskTbl} WHERE {taskID} NOT IN " +
                            $"(SELECT {mileTskTskID} FROM {mileTsksTbl} WHERE {mileTskID} IN " +
                            $"(SELECT {mileID} FROM {mileTbl} WHERE {mileProjID} = '{milestone.ProjID}')) " +
                            $"AND {taskProjID} = '{milestone.ProjID}' " +
                            $"AND {taskDeadline} <= '{Utils.ToSQLFormat(milestone.End)}' " +
                            $"AND {taskStart} >= '{Utils.ToSQLFormat(milestone.Start)}'";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }
    }
}
