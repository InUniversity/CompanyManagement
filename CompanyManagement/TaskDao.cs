using CompanyManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
namespace CompanyManagement
{
    public class TaskDao
    {
        private const string TABLE_NAME = "Task";
        private const string ID = "task_id";
        private const string NAME = "task_name";
        private const string START = "date_start";
        private const string END = "date_end";
        private const string EMPLOYEE_ID = "employee_id";

        DBConnection dbconnection = new DBConnection();

        public void Add(TaskInProject task)
        {
            
            string sqlStr = $"INSERT INTO {TABLE_NAME}({ID}, {NAME}, {START}, {END}, {EMPLOYEE_ID} VALUES ('{task.ID}', {task.Name}, {task.Start}, {task.End}, {task.EmployeeID})";
            dbconnection.ExecuteNonQuery(sqlStr);
        }
        public void Delete(TaskInProject task)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {ID} = {task.ID}";
            dbconnection.ExecuteNonQuery(sqlStr);
        }
        public void Save(TaskInProject task)
        {
            string sqlStr = $"UPDATE {TABLE_NAME} SET {NAME} = '{task.Name}', {START} = '{task.Start}', {END}= '{task.End}', {EMPLOYEE_ID}= '{task.EmployeeID}' WHERE {ID} = '{task.ID}'";
            dbconnection.ExecuteNonQuery(sqlStr);
        }
        public DataTable GetDataTable()
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME}";
            return dbconnection.GetDataTable(sqlStr);
        }

        public TaskInProject SearchByID(string id)
        {
            DataTable dttask = new DataTable();
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {ID} = '{id}'";
            dttask = dbconnection.GetDataTable(sqlStr);
            TaskInProject task = new TaskInProject(
            dttask.Columns[0].ToString(),
            dttask.Columns[1].ToString(),
            DateTime.ParseExact(dttask.Columns[2].ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
            DateTime.ParseExact(dttask.Columns[3].ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
            dttask.Columns[4].ToString()
                );
            return task;
        }
    }
}
