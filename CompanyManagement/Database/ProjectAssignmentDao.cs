using System.Collections.Generic;
using System.Linq;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class ProjectAssignmentDao : BaseDao
    {
        public void Add(ProjectAssignment projectAssignment)
        {
            string sqlStr = $"INSERT INTO {PROJECT_ASSIGNMENT_TABLE} ({PROJECT_ASSIGNMENT_PROJECT_ID}, " +
                            $"{PROJECT_ASSIGNMENT_DEPARTMENT_ID}) VALUES ('{projectAssignment.ProjectID}', " +
                            $"'{projectAssignment.DeparmentID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(ProjectAssignment projectAssignment)
        {
            string sqlStr = $"DELETE FROM {PROJECT_ASSIGNMENT_TABLE} " +
                            $"WHERE {PROJECT_ASSIGNMENT_PROJECT_ID}='{projectAssignment.ProjectID}' AND " +
                            $"{PROJECT_ASSIGNMENT_DEPARTMENT_ID}='{projectAssignment.DeparmentID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<Department> GetAllDepartmentInProject(string projectID)
        {
            string sqlStr = $"SELECT D.* FROM {DEPARTMENT_TABLE} D INNER JOIN {PROJECT_ASSIGNMENT_TABLE} PA ON " +
                            $"D.{DEPARTMENT_ID}=PA.{PROJECT_ASSIGNMENT_DEPARTMENT_ID} " +
                            $"WHERE PA.{PROJECT_ASSIGNMENT_PROJECT_ID}='{projectID}'";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }

        public List<Employee> GetEmployeesInProject(string projectID)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEE_TABLE} WHERE {EMPLOYEE_DEPARTMENT_ID} IN(" +
                $"SELECT {PROJECT_ASSIGNMENT_DEPARTMENT_ID} FROM {PROJECT_ASSIGNMENT_TABLE} WHERE {PROJECT_ASSIGNMENT_PROJECT_ID}='{projectID}')";
            return dbConnection.GetList(sqlStr, reader => new Employee(reader));
        }

        public List<Department> GetDepartmentsCanAssignWork(string projectID, string startDateTime, string endDateTime)
        {
            string sqlStr = $"SELECT * FROM {DEPARTMENT_TABLE} WHERE {DEPARTMENT_ID} NOT IN (" +
                            $"SELECT {PROJECT_ASSIGNMENT_DEPARTMENT_ID} FROM {PROJECT_ASSIGNMENT_TABLE} " +
                            $"WHERE {PROJECT_ASSIGNMENT_PROJECT_ID} IN (SELECT {PROJECT_ID} FROM {PROJECT_TABLE} " +
                            $"WHERE {PROJECT_ID} NOT LIKE '{projectID}' AND {PROJECT_PROPRESS} NOT LIKE '100'" +
                            $"AND {PROJECT_START} <= '{endDateTime}'" +
                            $"AND {PROJECT_END} >= '{startDateTime}')) EXCEPT (SELECT D.* FROM {DEPARTMENT_TABLE} D INNER JOIN {PROJECT_ASSIGNMENT_TABLE} PA ON D.{DEPARTMENT_ID}=PA.{PROJECT_ASSIGNMENT_DEPARTMENT_ID} WHERE PA.{PROJECT_ASSIGNMENT_PROJECT_ID}='{projectID}')";
            // TODO
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }

        public List<Project> SearchProjectByEmployeeID(string employeeID)
        {
            string sqlStr = $"SELECT * FROM {PROJECT_TABLE} WHERE {PROJECT_ID} IN " +
                            $"(SELECT {PROJECT_ASSIGNMENT_PROJECT_ID} FROM {PROJECT_ASSIGNMENT_TABLE} PA, {EMPLOYEE_TABLE} E " +
                            $"WHERE PA.{PROJECT_ASSIGNMENT_DEPARTMENT_ID}=E.{EMPLOYEE_DEPARTMENT_ID} " +
                            $"AND E.{EMPLOYEE_ID}='{employeeID}')";
            return dbConnection.GetList(sqlStr, reader => new Project(reader));
        }

        public List<Project> SearchProjectByCreatorID(string managerID)
        {
            string sqlStr = $"SELECT * FROM {PROJECT_TABLE} P WHERE P.{PROJECT_CREATE_BY}='{managerID}'";
            return dbConnection.GetList(sqlStr, reader => new Project(reader));
        }
    }
}
