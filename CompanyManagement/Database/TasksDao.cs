using System;
using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Utilities;

namespace CompanyManagement.Database
{
    public class TasksDao : BaseDao
    {
        public void Add(TaskInProject tsk)
        {
            string sqlStr = $"INSERT INTO {taskTbl}({taskID}, {taskTitle}, {taskExplanation}, {taskStart}, " +
                            $"{taskDeadline}, {taskOwnerID}, {taskProgress}, {taskEmplID}, {taskProjID}, " +
                            $"{taskStatusID}) VALUES ('{tsk.ID}', N'{tsk.Title}', N'{tsk.Explanation}', " +
                            $"'{tsk.StartDate}', '{tsk.Deadline}', '{tsk.OwnerID}', '{tsk.Progress}', " +
                            $"'{tsk.EmployeeID}', '{tsk.ProjectID}', '{(int)tsk.Status}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {taskTbl} WHERE {taskID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }
        
        public void DeleteByProjID(string projID)
        {
            string sqlStr = $"DELETE FROM {taskTbl} WHERE {taskProjID}='{projID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(TaskInProject tsk)
        {
            string sqlStr = $"UPDATE {taskTbl} SET {taskTitle}=N'{tsk.Title}', " +
                            $"{taskExplanation}=N'{tsk.Explanation}', {taskStart}='{tsk.StartDate}', " +
                            $"{taskDeadline}='{tsk.Deadline}', {taskOwnerID}='{tsk.OwnerID}', " +
                            $"{taskProgress}='{tsk.Progress}', {taskEmplID}='{tsk.EmployeeID}', " +
                            $"{taskProjID}='{tsk.ProjectID}', {taskStatusID}='{(int)tsk.Status}' " +
                            $"WHERE {taskID}='{tsk.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public TaskInProject SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {taskTbl} WHERE {taskID}='{id}'";           
            return (TaskInProject)dbConnection.GetSingleObject(sqlStr, reader => new TaskInProject(reader));
        }

        public List<TaskInProject> SearchByProjectID(string projID)
        {
            string sqlStr = $"SELECT * FROM {taskTbl} WHERE {taskProjID}='{projID}'";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }

        public List<TaskInProject> SearchByEmployeeID(string projID, string emplID)
        {
            string sqlStr = $"SELECT * FROM {taskTbl} WHERE {taskEmplID} = '{emplID}' AND {taskProjID} = '{projID}'";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }

        public List<TaskInProject> SearchTasksCheckOut(string emplID, DateTime curDate)
        {
            string sqlStr = $"SELECT * FROM {taskTbl} WHERE {taskEmplID} = '{emplID}'" +
                            $"AND {taskDeadline} >= '{Utils.ToSQLFormat(curDate)}' AND {taskStart} <= '{Utils.ToSQLFormat(curDate)}' " +
                            $"AND {taskProgress} != '{completed}'";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }
    }
}
