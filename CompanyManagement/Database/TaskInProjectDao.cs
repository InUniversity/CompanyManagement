using CompanyManagement.Models;
using System.Data;
namespace CompanyManagement.Database
{
    public class TaskInProjectDao
    {
        public static string TABLE_NAME = "Task";
        public static string ID = "task_id";
        public static string TILE = "tile";
        public static string DESCRIPTION = "task_description";
        public static string ASSIGN_DATE = "assign_date";
        public static string DEADLINE = "deadline";
        public static string CREATEBY = "create_by";
        public static string PROGRESS = "progress";
        public static string EMPLOYEE_ID = "employee_id";
        public static string PROJECT_ID = "project_id";

        DBConnection dbconnection = new DBConnection();

        public void Add(TaskInProject task)
        {
            string sqlStr = $"INSERT INTO {TABLE_NAME}({ID}, {TILE}, {DESCRIPTION}, {ASSIGN_DATE}, {DEADLINE}, {CREATEBY}, {PROGRESS}, {EMPLOYEE_ID}, {PROJECT_ID})" +
                $"VALUES ('{task.ID}', '{task.Tile}', {task.Description}, {task.AssignDate}, '{task.Deadline}', '{task.CreateBy}', '{task.Progress}', '{task.EmployeeID}', '{task.ProjectID}')";
            dbconnection.ExecuteNonQuery(sqlStr);
        }
        public void Delete(TaskInProject task)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {ID} = {task.ID}";
            dbconnection.ExecuteNonQuery(sqlStr);
        }
        public void Save(TaskInProject task)
        {
            string sqlStr = $"UPDATE {TABLE_NAME} SET {TILE} = '{task.Tile}', {DESCRIPTION} = {task.Description}, {ASSIGN_DATE}= {task.AssignDate}, {DEADLINE}= '{task.Deadline}', {CREATEBY}= '{task.CreateBy}', {PROGRESS}= '{task.Progress}', {EMPLOYEE_ID}= '{task.EmployeeID}', {PROJECT_ID}= '{task.ProjectID}'" +
                $"WHERE {ID} = '{task.ID}'";
            dbconnection.ExecuteNonQuery(sqlStr);
        }
        public DataTable GetDataTable(string project_id)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {PROJECT_ID}='{project_id}'";
            return dbconnection.GetDataTable(sqlStr);
        }

        public TaskInProject SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {ID} = '{id}'";
            DataTable dttask = dbconnection.GetDataTable(sqlStr);
            if (dttask.Rows.Count == 0)
                return null;
            return new TaskInProject(dttask.Rows[0]);
        }
    }
}
