using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CompanyManagement.Database
{
    public class ProjectDao
    {
        public static string TABLE_NAME = "Project";
        public static string ID = "project_id";
        public static string NAME = "project_name";
        public static string START = "date_start";
        public static string END = "date_end";
        public static string BUDGET = "budget";
        public static string STATUS = "project_status";

        DBConnection dbconnection = new DBConnection();

        public void Add(Project project)
        {
            string sqlStr = $"INSERT INTO {TABLE_NAME}({ID}, {NAME}, {START}, {END}, {BUDGET}, {STATUS})" +
                $"VALUES ('{project.ID}', '{project.Name}', {project.Start}, {project.End}, {project.Budget}, '{project.Status}')";
            dbconnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(Project project)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {ID} = '{project.ID}'";
            dbconnection.ExecuteNonQuery(sqlStr);
        }

        public void Save(Project project)
        {
            string sqlStr = $"UPDATE {TABLE_NAME} SET {NAME} = '{project.ID}', {START}= {project.Start}, {END}= {project.End}, {BUDGET}= {project.Budget}," +
                $"{STATUS}= '{project.Status}' WHERE {ID} = '{project.ID}'";
            dbconnection.ExecuteNonQuery(sqlStr);
        }

        public DataTable GetDataTable()
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME}";
            return dbconnection.GetDataTable(sqlStr);
        }

        public Project SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {ID} = '{id}'";
            DataTable dtproject = dbconnection.GetDataTable(sqlStr);
            if (dtproject.Rows.Count == 0)
                return null;
            return new Project(dtproject.Rows[0]);
        }
    }
}
