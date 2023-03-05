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
        private const string ID = "project_id";
        private const string NAME = "project_name";
        private const string START = "date_start";
        private const string END = "date_end";
        private const string BUDGET = "budget";
        private const string STATUS ="project_status";

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

        public Project SearchByID(string id)
        {
            DataTable dtproject = new DataTable();
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {ID} = '{id}'";
            dtproject =  dbconnection.GetDataTable(sqlStr);
            Project project = new Project(
            dtproject.Columns[0].ToString(),
            dtproject.Columns[1].ToString(),
            DateTime.ParseExact(dtproject.Columns[2].ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
            DateTime.ParseExact(dtproject.Columns[3].ToString(), "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
            int.Parse(dtproject.Columns[4].ToString()),
            dtproject.Columns[5].ToString()
                );
            return project;
        }
    }
}
