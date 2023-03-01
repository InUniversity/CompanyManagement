using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement
{
    public class ProjectDao
    {
        DBConnection dbconnection = new DBConnection();
        public void Add(Project project)
        {
            string sqlStr = string.Format("INSERT INTO Project(project_id, project_name, date_start, date_end, budget, status_prj) VALUES ('{0}', '{1}', '{2}', '{3}', {4}, '{5}')", project.ID, project.Name, project.Start, project.End, project.Budget,project.Status);
            dbconnection.Running(sqlStr);
        }
        public void Delete(Project project)
        {
            string sqlStr = string.Format("DELETE FROM Project WHERE project_id = {0}", project.ID);
            dbconnection.Running(sqlStr);
        }
        public void Save(Project project)
        {
            string sqlStr = string.Format("UPDATE Project project_name = '{1}', date_start = '{2}', date_end = '{3}', budget = {4}, status_prj = '{5}' WHEREproject_id = '{0}'", project.ID, project.Name, project.Start, project.End, project.Budget, project.Status);
            dbconnection.Running(sqlStr);
        }
        public DataTable ReturnList()
        {
            string sqlStr = string.Format("SELECT * FROM Project");
            return dbconnection.ReturnTable(sqlStr);
        }
    }
}
