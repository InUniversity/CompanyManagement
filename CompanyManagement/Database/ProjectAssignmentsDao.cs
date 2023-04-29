using System.Collections.Generic;
using System.Linq;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class ProjectAssignmentsDao : BaseDao
    {
        public void Add(ProjectAssignment projectAssignment)
        {
            string sqlStr = $"INSERT INTO {PROJECT_ASSIGNMENTS_TABLE} ({PROJECT_ASSIGNMENTS_PROJECT_ID}, " +
                            $"{PROJECT_ASSIGNMENTS_DEPARTMENT_ID}) VALUES ('{projectAssignment.ProjectID}', " +
                            $"'{projectAssignment.DeparmentID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(ProjectAssignment projectAssignment)
        {
            string sqlStr = $"DELETE FROM {PROJECT_ASSIGNMENTS_TABLE} " +
                            $"WHERE {PROJECT_ASSIGNMENTS_PROJECT_ID}='{projectAssignment.ProjectID}' AND " +
                            $"{PROJECT_ASSIGNMENTS_DEPARTMENT_ID}='{projectAssignment.DeparmentID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<Department> GetAllDepartmentInProject(string projectID)
        {
            string sqlStr = $"SELECT D.* FROM {DEPARTMENTS_TABLE} D INNER JOIN {PROJECT_ASSIGNMENTS_TABLE} PA ON " +
                            $"D.{DEPARTMENTS_ID}=PA.{PROJECT_ASSIGNMENTS_DEPARTMENT_ID} " +
                            $"WHERE PA.{PROJECT_ASSIGNMENTS_PROJECT_ID}='{projectID}'";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }

        public List<Employee> GetEmployeesInProject(string projectID)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEES_TABLE} WHERE {EMPLOYEES_DEPARTMENT_ID} IN(" +
                $"SELECT {PROJECT_ASSIGNMENTS_DEPARTMENT_ID} FROM {PROJECT_ASSIGNMENTS_TABLE} WHERE {PROJECT_ASSIGNMENTS_PROJECT_ID}='{projectID}')";
            return dbConnection.GetList(sqlStr, reader => new Employee(reader));
        }

        public List<Department> GetDepartmentsCanAssignWork(string projectID, string startDateTime, string endDateTime)
        {
            string sqlStr = $"SELECT * FROM {DEPARTMENTS_TABLE} WHERE {DEPARTMENTS_ID} NOT IN (" +
                            $"SELECT {PROJECT_ASSIGNMENTS_DEPARTMENT_ID} FROM {PROJECT_ASSIGNMENTS_TABLE} " +
                            $"WHERE {PROJECT_ASSIGNMENTS_PROJECT_ID} IN (SELECT {PROJECTS_ID} FROM {PROJECTS_TABLE} " +
                            $"WHERE {PROJECTS_ID} NOT LIKE '{projectID}' AND {PROJECTS_PROPRESS} NOT LIKE '100'" +
                            $"AND {PROJECTS_START} <= '{endDateTime}'" +
                            $"AND {PROJECTS_END} >= '{startDateTime}')) EXCEPT (SELECT D.* FROM {DEPARTMENTS_TABLE} D INNER JOIN {PROJECT_ASSIGNMENTS_TABLE} PA ON D.{DEPARTMENTS_ID}=PA.{PROJECT_ASSIGNMENTS_DEPARTMENT_ID} WHERE PA.{PROJECT_ASSIGNMENTS_PROJECT_ID}='{projectID}')";
            // TODO
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }

        public List<Project> SearchProjectByEmployeeID(string employeeID)
        {
            string sqlStr = $"SELECT * FROM {PROJECTS_TABLE} WHERE {PROJECTS_ID} IN " +
                            $"(SELECT {PROJECT_ASSIGNMENTS_PROJECT_ID} FROM {PROJECT_ASSIGNMENTS_TABLE} PA, {EMPLOYEES_TABLE} E " +
                            $"WHERE PA.{PROJECT_ASSIGNMENTS_DEPARTMENT_ID}=E.{EMPLOYEES_DEPARTMENT_ID} " +
                            $"AND E.{EMPLOYEES_ID}='{employeeID}')";
            return dbConnection.GetList(sqlStr, reader => new Project(reader));
        }

        public List<Project> SearchProjectByCreatorID(string managerID)
        {
            string sqlStr = $"SELECT * FROM {PROJECTS_TABLE} P WHERE P.{PROJECTS_OWNER_ID}='{managerID}'";
            return dbConnection.GetList(sqlStr, reader => new Project(reader));
        }
    }
}
