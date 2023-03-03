using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CompanyManagement
{
    public class ProjectDao
    {
        private const string TABLE_NAME = "Project";
        private const string ID = "projectID";
        private const string NAME = "projectName";
        private const string START = "dateStart";
        private const string END = "dateEnd";
        private const string BUDGET = "budget";
        private const string STATUS ="statusProject";

        DBConnection dbconnection = new DBConnection();
        public void Add(Project project)
        {
            string sqlStr = $"INSERT INTO {TABLE_NAME}({ID}, {NAME}, {START}, {END}, {BUDGET}, {STATUS}) VALUES ('{project.ID}', '{project.Name}', '{project.Start}', '{project.End}', {project.Budget}, '{project.Status}')";
            dbconnection.ExecuteNonQuery(sqlStr);
        }
        public void Delete(Project project)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {ID} = '{project.ID}'";
            dbconnection.ExecuteNonQuery(sqlStr);
        }
        public void Save(Project project)
        {
            string sqlStr = $"UPDATE {TABLE_NAME} SET {NAME} = '{project.ID}', {START}= '{project.Start}', {END}= '{project.End}', {BUDGET}= {project.Budget}, {STATUS}= '{project.Status}' WHERE {ID} = '{project.ID}'";     
            dbconnection.ExecuteNonQuery(sqlStr);
        }
        public DataTable GetDataTable()
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME}";
            return dbconnection.GetDataTable(sqlStr);
        }
        public DataTable SearchByID(Project project)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {ID} = '{project.ID}'";
            return dbconnection.GetDataTable(sqlStr);
        }
    }
}
