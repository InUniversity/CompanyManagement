﻿using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using System.Collections.Generic;

namespace CompanyManagement.Database
{
    public class TimeKeepingDao : BaseDao
    {
        public void Add(TimeKeeping timeKeeping)
        {
            string sqlStr = $"INSERT INTO {TIME_KEEPING_TABLE} ({TIME_KEEPING_TASK_ID}, {TIME_KEEPING_START_TIME}, " +
                            $"{TIME_KEEPING_END_TIME}, {TIME_KEEPING_EMPLOYEE_ID}, {TIME_KEEPING_NOTES}, " +
                            $"{TIME_KEEPING_CREATE_BY}) VALUES('{timeKeeping.TaskID}', '{timeKeeping.Start}', " +
                            $"'{timeKeeping.End}', '{timeKeeping.EmployeeID}', '{timeKeeping.Notes}', '{timeKeeping.CreateBy}')";

            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string taskID)
        {
            string sqlStr = $"DELETE FROM {TIME_KEEPING_TABLE} WHERE {TIME_KEEPING_TASK_ID} = '{taskID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(TimeKeeping timeKeeping)
        {
            string sqlStr = $"UPDATE {TIME_KEEPING_TABLE} SET {TIME_KEEPING_START_TIME} = '{timeKeeping.Start}', " +
                            $"{TIME_KEEPING_END_TIME} = '{timeKeeping.End}', {TIME_KEEPING_EMPLOYEE_ID} = '{timeKeeping.EmployeeID}', " +
                            $"{TIME_KEEPING_NOTES} = '{timeKeeping.Notes}', {TIME_KEEPING_CREATE_BY} = '{timeKeeping.CreateBy}' " +
                            $"WHERE {TIME_KEEPING_TASK_ID} = '{timeKeeping.TaskID}';";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<TimeKeeping> GetAll()
        {
            string sqlStr = $"SELECT * FROM {TIME_KEEPING_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new TimeKeeping(reader));
        }

        public TimeKeeping SearchByID(string timeKeepingID)
        {
            string sqlStr = $"SELECT * FROM {TIME_KEEPING_TABLE} WHERE {TIME_KEEPING_TASK_ID} = '{timeKeepingID}'";
            List<TimeKeeping> tasks = dbConnection.GetList(sqlStr, reader => new TimeKeeping(reader));
            if (tasks.Count == 0)
                return null;
            return tasks[0];
        }

        public List<TimeKeeping> SearchByProjectID(string projectID)
        {
            string sqlStr = $"SELECT * FROM {TIME_KEEPING_TABLE} WHERE {TIME_KEEPING_TASK_ID} IN" +
                            $"(SELECT {TASK_ID} FROM {TASK_TABLE} WHERE {TASK_PROJECT_ID}='{projectID}')";
            return dbConnection.GetList(sqlStr, reader => new TimeKeeping(reader));
        }

        public List<TimeKeeping> SearchByEmployeeID(string projectID, string employeeID)
        {
            string sqlStr = $"SELECT * FROM {TIME_KEEPING_TABLE} WHERE {TIME_KEEPING_TASK_ID} IN" +
                            $"(SELECT {TASK_ID} FROM {TASK_TABLE} WHERE {TASK_PROJECT_ID}='{projectID}' " +
                            $"AND {TASK_EMPLOYEE_ID} = '{employeeID}')";
            return dbConnection.GetList(sqlStr, reader => new TimeKeeping(reader));
        }
    }
}