using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class TasksDao : BaseDao
    {
        public void Add(TaskInProject task)
        {
            string sqlStr = $"INSERT INTO {taskTbl}({taskID}, {taskTitle}, {taskExplanation}, " +
                            $"{taskStart}, {taskDeadline}, {taskOwnerID}, {taskProgress}, " +
                            $"{taskEmplID}, {taskProjID}, {taskStatusID}) " +
                            $"VALUES ('{task.ID}', N'{task.Title}', N'{task.Explanation}', " +
                            $"'{task.StartDate}', '{task.Deadline}', '{task.OwnerID}', " +
                            $"'{task.Progress}', '{task.EmployeeID}', '{task.ProjectID}', '{task.StatusID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {taskTbl} WHERE {taskID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(TaskInProject task)
        {
            string sqlStr = $"UPDATE {taskTbl} SET {taskTitle}=N'{task.Title}', " +
                            $"{taskExplanation}=N'{task.Explanation}', {taskStart}='{task.StartDate}', " +
                            $"{taskDeadline}='{task.Deadline}', {taskOwnerID}='{task.OwnerID}', " +
                            $"{taskProgress}='{task.Progress}', {taskEmplID}='{task.EmployeeID}', " +
                            $"{taskProjID}='{task.ProjectID}', {taskStatusID}='{task.StatusID}' " +
                            $"WHERE {taskID}='{task.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void UpdateProgress(string taskID, string progress)
        {
            string sqlStr = $"UPDATE {taskTbl} SET {taskProgress}='{progress}' WHERE {BaseDao.taskID}='{taskID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public TaskInProject SearchByID(string taskInProjectID)
        {
            string sqlStr = $"SELECT * FROM {taskTbl} WHERE {taskID}='{taskInProjectID}'";           
            return (TaskInProject)dbConnection.GetSingleObject(sqlStr, reader => new TaskInProject(reader));
        }

        public List<TaskInProject> SearchByProjectID(string projectID)
        {
            string sqlStr = $"SELECT * FROM {taskTbl} WHERE {taskProjID}='{projectID}'";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }

        public List<TaskInProject> SearchByEmployeeID(string projectID, string employeeID)
        {
            string sqlStr = $"SELECT * FROM {taskTbl} WHERE {taskEmplID} = '{employeeID}' AND {taskProjID} = '{projectID}'";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }

        public List<TaskInProject> SearchCurrentTasksByEmployeeID(string employeeID)
        {
            string sqlStr = $"SELECT * FROM {taskTbl} WHERE {taskEmplID} = '{employeeID}'" +
                            $"AND {taskDeadline} >= '{DateTime.Now}' AND {taskStart} <= '{DateTime.Now}' " +
                            $"AND {taskProgress} != '{completed}'";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }
    }
}
