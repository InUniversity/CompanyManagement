using System;
using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class CompletedTaskDao : BaseDao
    {
        public void Add(CompletedTask completedTask)
        {
            string sqlStr = $"INSERT INTO {COMPLETED_TASK_TABLE} ({COMPLETED_TASK_ID}, {COMPLETED_TASK_TASK_ID})" +
                            $"VALUES ('{completedTask.ID}', '{completedTask.TaskID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<TaskInProject> GetOpenAssignedTasks(string employeeID, string checkInTime)
        {
            string sqlStr = $"SELECT * FROM {TASK_TABLE} T WHERE T.{TASK_EMPLOYEE_ID}='{employeeID}' AND T.{TASK_STATUS_ID}='1' " +
                            $"AND T.{TASK_ASSIGN_DATE} <= {checkInTime} AND T.{TASK_DEADLINE} >= {checkInTime}";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader)); 
        }
        
        public List<TaskInProject> GetCompletedTasksWithoutTimeTracking(string employeeID, string checkInTime)
        {
            // TODO
            string sqlStr = $"SELECT * FROM {TASK_TABLE} T WHERE T.{TASK_EMPLOYEE_ID}='{employeeID}' AND T.{TASK_STATUS_ID}='1'";
            return dbConnection.GetList(sqlStr, reader => new TaskInProject(reader)); 
        }    
    }
}
