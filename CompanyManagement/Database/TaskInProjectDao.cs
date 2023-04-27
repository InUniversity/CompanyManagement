using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class TaskInProjectDao : BaseDao
    {
        public void Add(TaskInProject task)
        {
            string sqlStr = $"INSERT INTO {TASKS_TABLE}({TASKS_ID}, {TASKS_TITLE}, {TASKS_DESCRIPTION}, " +
                            $"{TASKS_ASSIGN_DATE}, {TASKS_DEADLINE}, {TASKS_CREATE_BY}, {TASKS_PROGRESS}, " +
                            $"{TASKS_EMPLOYEE_ID}, {TASKS_PROJECT_ID}, {TASKS_STATUS_ID}) VALUES ('{task.ID}', N'{task.Title}', " +
                            $"N'{task.Description}', '{task.AssignDate}', '{task.Deadline}', '{task.CreateBy}', " +
                            $"'{task.Progress}', '{task.EmployeeID}', '{task.ProjectID}', {task.StatusID})";
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
                            $"{TASKS_DESCRIPTION}=N'{task.Description}', {TASKS_ASSIGN_DATE}='{task.AssignDate}', " +
                            $"{TASKS_DEADLINE}='{task.Deadline}', {TASKS_CREATE_BY}='{task.CreateBy}', " +
                            $"{TASKS_PROGRESS}='{task.Progress}', {TASKS_EMPLOYEE_ID}='{task.EmployeeID}', " +
                            $"{TASKS_PROJECT_ID}='{task.ProjectID}', {TASKS_STATUS_ID}='{task.StatusID}' WHERE {TASKS_ID}='{task.ID}'";
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

        public List<TaskInProject> SearchByEmployeeID(string projectID, string employeeID)
        {
            string sqlStr = $"SELECT * FROM {TASKS_TABLE} WHERE {TASKS_EMPLOYEE_ID} = '{employeeID}' AND {TASKS_PROJECT_ID} = '{projectID}'";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }

        public List<TaskInProject> SearchCurrentTasksByEmployeeID(string employeeID)
        {
            string sqlStr = $"SELECT * FROM {TASKS_TABLE} WHERE {TASKS_EMPLOYEE_ID} = '{employeeID}'" +
                            $"AND {TASKS_DEADLINE} >= '{DateTime.Now}' AND {TASKS_ASSIGN_DATE} <= '{DateTime.Now}' " +
                            $"AND {TASKS_PROGRESS} != '{COMPLETED}'";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }
    }
}
