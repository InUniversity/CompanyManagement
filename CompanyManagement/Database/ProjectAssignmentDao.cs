using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CompanyManagement.Database
{
    public class ProjectAssignmentDao
    {

        public const string TABLE_NAME = "ProjectAssignment";
        public const string PROJECT_ID = "project_id";
        public const string ROLE = "role";
        public const string DEPARTMENT_ID = "department_id";

        private DBConnection dbconnection = new DBConnection();

        public DataTable GetDataTable()
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME}";
            return dbconnection.GetDataTable(sqlStr);
        }
    }
}
