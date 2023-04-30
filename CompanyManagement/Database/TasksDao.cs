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
            string sqlStr = $"INSERT INTO {TASKS_TABLE}({TASKS_ID}, {TASKS_TITLE}, {TASKS_EXPLANATION}, " +
                            $"{TASKS_START_DATE}, {TASKS_DEADLINE}, {TASKS_OWNER_ID}, {TASKS_PROGRESS}, " +
                            $"{TASKS_EMPLOYEE_ID}, {TASKS_PROJECT_ID}, {TASKS_STATUS_ID}) " +
                            $"VALUES ('{task.ID}', N'{task.Title}', N'{task.Explanation}', " +
                            $"'{task.StartDate}', '{task.Deadline}', '{task.OwnerID}', " +
                            $"'{task.Progress}', '{task.EmployeeID}', '{task.ProjectID}', '{task.StatusID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {TASKS_TABLE} WHERE {TASKS_ID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(TaskInProject task)
        {
            string sqlStr = $"UPDATE {TASKS_TABLE} SET {TASKS_TITLE}=N'{task.Title}', " +
                            $"{TASKS_EXPLANATION}=N'{task.Explanation}', {TASKS_START_DATE}='{task.StartDate}', " +
                            $"{TASKS_DEADLINE}='{task.Deadline}', {TASKS_OWNER_ID}='{task.OwnerID}', " +
                            $"{TASKS_PROGRESS}='{task.Progress}', {TASKS_EMPLOYEE_ID}='{task.EmployeeID}', " +
                            $"{TASKS_PROJECT_ID}='{task.ProjectID}', {TASKS_STATUS_ID}='{task.StatusID}' " +
                            $"WHERE {TASKS_ID}='{task.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void UpdateProgress(string taskID, string progress)
        {
            string sqlStr = $"UPDATE {TASKS_TABLE} SET {TASKS_PROGRESS}='{progress}' WHERE {TASKS_ID}='{taskID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public TaskInProject SearchByID(string taskInProjectID)
        {
            string sqlStr = $"SELECT * FROM {TASKS_TABLE} WHERE {TASKS_ID}='{taskInProjectID}'";           
            return (TaskInProject)dbConnection.GetSingleObject(sqlStr, reader => new TaskInProject(reader));
        }

        public List<TaskInProject> SearchByProjectID(string projectID)
        {
            string sqlStr = $"SELECT * FROM {TASKS_TABLE} WHERE {TASKS_PROJECT_ID}='{projectID}'";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }

        public List<TaskInProject> SearchByRequesterID(string projectID, string employeeID)
        {
            string sqlStr = $"SELECT * FROM {TASKS_TABLE} WHERE {TASKS_EMPLOYEE_ID} = '{employeeID}' AND {TASKS_PROJECT_ID} = '{projectID}'";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }

        public List<TaskInProject> SearchCurrentTasksByEmployeeID(string employeeID)
        {
            string sqlStr = $"SELECT * FROM {TASKS_TABLE} WHERE {TASKS_EMPLOYEE_ID} = '{employeeID}'" +
                            $"AND {TASKS_DEADLINE} >= '{DateTime.Now}' AND {TASKS_START_DATE} <= '{DateTime.Now}' " +
                            $"AND {TASKS_PROGRESS} != '{COMPLETED}'";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }
    }
}
