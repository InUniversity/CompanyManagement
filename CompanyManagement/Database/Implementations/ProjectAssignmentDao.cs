using System.Collections.Generic;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.Models;

namespace CompanyManagement.Database.Implementations
{
    public class ProjectAssignmentDao : BaseDao, IProjectAssignmentDao
    {
        public void Add(ProjectAssignment projectAssignment)
        {
            string sqlStr = $"INSERT INTO {PROJECT_ASSIGNMENT_TABLE} ({PROJECT_ASSIGNMENT_PROJECT_ID}, " +
                            $"{PROJECT_ASSIGNMENT_DEPARTMENT_ID}) VALUES ('{projectAssignment.ProjectID}', " +
                            $"'{projectAssignment.DeparmentID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string projectID, string departmentID)
        {
            string sqlStr = $"DELETE FROM {PROJECT_ASSIGNMENT_TABLE} " +
                            $"WHERE {PROJECT_ASSIGNMENT_PROJECT_ID}='{projectID}' AND " +
                            $"{PROJECT_ASSIGNMENT_DEPARTMENT_ID}='{departmentID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<Department> GetAllDepartmentInProject(string projectID)
        {
            string sqlStr = $"SELECT D.* FROM {DEPARTMENT_TABLE} D INNER JOIN {PROJECT_ASSIGNMENT_TABLE} PA ON " +
                            $"D.{DEPARTMENT_ID} = PA.{PROJECT_ASSIGNMENT_DEPARTMENT_ID} " +
                            $"WHERE PA.{PROJECT_ASSIGNMENT_PROJECT_ID}='{projectID}'";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));   
        }

        public List<EmployeeAccount> GetEmployeesInProject(string projectID)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEE_TABLE} WHERE {EMPLOYEE_DEPARTMENT_ID} IN(" +
                $"SELECT {PROJECT_ASSIGNMENT_DEPARTMENT_ID} FROM {PROJECT_ASSIGNMENT_TABLE} WHERE {PROJECT_ASSIGNMENT_PROJECT_ID} = '{projectID}')";
            return dbConnection.GetList(sqlStr, reader => new EmployeeAccount(reader));
        }
        
        public List<Department> GetDepartmentsCanAssignWork(Project project)
        {
            string sqlStr = $"SELECT * FROM {DEPARTMENT_TABLE} WHERE {DEPARTMENT_ID} NOT IN (" +
                            $"Select {PROJECT_ASSIGNMENT_DEPARTMENT_ID} FROM {PROJECT_ASSIGNMENT_TABLE} " +
                            $"WHERE {PROJECT_ASSIGNMENT_PROJECT_ID} IN (" +
                            $"Select {PROJECT_ID} FROM {PROJECT_TABLE} " +
                            $"WHERE {PROJECT_PROPRESS} NOT LIKE '100'" +
                            $"AND {PROJECT_START} <= '{project.End}'" +
                            $"AND {PROJECT_END} >= '{project.Start}'))";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }
    }
}
