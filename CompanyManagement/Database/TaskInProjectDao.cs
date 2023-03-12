using CompanyManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
namespace CompanyManagement.Database
{
    public class TaskInProjectDao
    {
        public static string TABLE_NAME = "Task";
        public static string ID = "task_id";
        public static string NAME = "task_name";
        public static string START = "date_start";
        public static string END = "date_end";
        public static string EMPLOYEE_ID = "employee_id";

        DBConnection dbConnection = new DBConnection();

        public void Add(TaskInProject task)
        {
            string sqlStr = $"INSERT INTO {TABLE_NAME}({ID}, {NAME}, {START}, {END}, {EMPLOYEE_ID})" +
                $"VALUES ('{task.ID}', '{task.Name}', {task.Start}, {task.End}, '{task.EmployeeID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(TaskInProject task)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {ID} = {task.ID}";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Save(TaskInProject task)
        {
            string sqlStr = $"UPDATE {TABLE_NAME} SET {NAME} = '{task.Name}', {START} = {task.Start}, {END}= {task.End}, {EMPLOYEE_ID}= '{task.EmployeeID}'" +
                $"WHERE {ID} = '{task.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public DataTable GetDataTable()
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME}";
            return dbConnection.GetDataTable(sqlStr);
        }

        public TaskInProject SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {ID} = '{id}'";
            DataTable dtTask = dbConnection.GetDataTable(sqlStr);
            if (dtTask.Rows.Count == 0)
                return null;
            return new TaskInProject(dtTask.Rows[0]);
        }
    }
}
