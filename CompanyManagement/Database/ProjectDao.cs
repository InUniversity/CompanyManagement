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
        public static string START = "create_time";
        public static string END = "end_time";
        public static string PROPRESS = "progress";

        DBConnection dbconnection = new DBConnection();

        public void Add(Project project)
        {
            string sqlStr = $"INSERT INTO {TABLE_NAME}({ID}, {NAME}, {START}, {END}, {PROPRESS})" +
                $"VALUES ('{project.ID}', N'{project.Name}', '{project.Start.ToString()}', '{project.End.ToString()}', '{project.Progress}')";
            dbconnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {ID} = '{id}'";
            dbconnection.ExecuteNonQuery(sqlStr);
        }
        public void DeleteAll()
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME}";
            dbconnection.ExecuteNonQuery(sqlStr);
        }

        public void Save(Project project)
        {
            string sqlStr = $"UPDATE {TABLE_NAME} SET {NAME} = '{project.Name}', {START} = '{project.Start.ToString()}', {END}= '{project.End.ToString()}',{PROPRESS}= '{project.Progress}' WHERE {ID} = '{project.ID}'";
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
