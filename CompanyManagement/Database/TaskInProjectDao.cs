using CompanyManagement.Models;
using System.Data;

namespace CompanyManagement.Database
{
    public class TaskInProjectDao
    {
        private const string TABLE_NAME = "Task";
        public const string ID = "task_id";
        public const string TILE = "title";
        public const string DESCRIPTION = "task_description";
        public const string ASSIGN_DATE = "assign_date";
        public const string DEADLINE = "deadline";
        public const string CREATEBY = "create_by";
        public const string PROGRESS = "progress";
        public const string EMPLOYEE_ID = "employee_id";
        public const string PROJECT_ID = "project_id";

        DBConnection dbConnection = new DBConnection();

        public void Add(TaskInProject task)
        {
            string sqlStr = $"INSERT INTO {TABLE_NAME}({ID}, {TILE}, {DESCRIPTION}, {ASSIGN_DATE}, {DEADLINE}, " +
                            $"{CREATEBY}, {PROGRESS}, {EMPLOYEE_ID}, {PROJECT_ID}) VALUES ('{task.ID}', " +
                            $"N'{task.Title}', N'{task.Description}', '{task.AssignDate}', '{task.Deadline}', " +
                            $"'{task.CreateBy}', '{task.Progress}', '{task.EmployeeID}', '{task.ProjectID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(TaskInProject task)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {ID} = '{task.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(TaskInProject task)
        {
            string sqlStr = $"UPDATE {TABLE_NAME} SET {TILE}=N'{task.Title}', {DESCRIPTION}=N'{task.Description}', " +
                            $"{ASSIGN_DATE}='{task.AssignDate}', {DEADLINE}='{task.Deadline}', " +
                            $"{CREATEBY}='{task.CreateBy}', {PROGRESS}='{task.Progress}', {EMPLOYEE_ID}='{task.EmployeeID}', " +
                            $"{PROJECT_ID}='{task.ProjectID}' WHERE {ID}='{task.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public DataTable GetDataTable(string projectID)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {PROJECT_ID}='{projectID}'";
            return dbConnection.GetDataTable(sqlStr);
        }
    }
}
