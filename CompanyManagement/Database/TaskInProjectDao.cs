﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class TaskInProjectDao : BaseDao
    {
        public void Add(TaskInProject task)
        {
            string sqlStr = $"INSERT INTO {TASK_TABLE}({TASK_ID}, {TASK_TITLE}, {TASK_DESCRIPTION}, " +
                            $"{TASK_ASSIGN_DATE}, {TASK_DEADLINE}, {TASK_CREATE_BY}, {TASK_PROGRESS}, " +
                            $"{TASK_EMPLOYEE_ID}, {TASK_PROJECT_ID}, {TASK_STATUS_ID}) VALUES ('{task.ID}', N'{task.Title}', " +
                            $"N'{task.Description}', '{task.AssignDate}', '{task.Deadline}', '{task.CreateBy}', " +
                            $"'{task.Progress}', '{task.EmployeeID}', '{task.ProjectID}', {task.Status})";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {TASK_TABLE} WHERE {TASK_ID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(TaskInProject task)
        {
            string sqlStr = $"UPDATE {TASK_TABLE} SET {TASK_TITLE}=N'{task.Title}', " +
                            $"{TASK_DESCRIPTION}=N'{task.Description}', {TASK_ASSIGN_DATE}='{task.AssignDate}', " +
                            $"{TASK_DEADLINE}='{task.Deadline}', {TASK_CREATE_BY}='{task.CreateBy}', " +
                            $"{TASK_PROGRESS}='{task.Progress}', {TASK_EMPLOYEE_ID}='{task.EmployeeID}', " +
                            $"{TASK_PROJECT_ID}='{task.ProjectID}', {TASK_STATUS_ID} = {task.Status} WHERE {TASK_ID}='{task.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public TaskInProject SearchByID(string taskInProjectID)
        {
            string sqlStr = $"SELECT * FROM {TASK_TABLE} WHERE {TASK_ID}='{taskInProjectID}'";
            List<TaskInProject> tasks = dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
            if (tasks.Count == 0)
                return null;
            return tasks[0];
        }

        public List<TaskInProject> SearchByProjectID(string projectID)
        {
            string sqlStr = $"SELECT * FROM {TASK_TABLE} WHERE {TASK_PROJECT_ID}='{projectID}'";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }

        public List<TaskInProject> SearchByEmployeeID(string projectID, string employeeID)
        {
            string sqlStr = $"SELECT * FROM {TASK_TABLE} WHERE {TASK_EMPLOYEE_ID} = '{employeeID}' AND {TASK_PROJECT_ID} = '{projectID}'";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader));
        }
    }
}