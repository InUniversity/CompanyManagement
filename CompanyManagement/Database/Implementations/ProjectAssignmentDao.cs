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
                            $"D.{DEPARTMENT_ID} = PA.{PROJECT_ASSIGNMENT_DEPARTMENT_ID} " +
                            $"WHERE PA.{PROJECT_ASSIGNMENT_PROJECT_ID}='{projectID}'";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));   
        }

        public List<Employee> GetEmployeesInProject(string projectID)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEE_TABLE} WHERE {EMPLOYEE_DEPARTMENT_ID} IN(" +
                $"SELECT {PROJECT_ASSIGNMENT_DEPARTMENT_ID} FROM {PROJECT_ASSIGNMENT_TABLE} WHERE {PROJECT_ASSIGNMENT_PROJECT_ID} = '{projectID}')";
            return dbConnection.GetList(sqlStr, reader => new Employee(reader));
        }
        
        public List<Department> GetDepartmentsCanAssignWork(Project project)
        {
            string sqlStr = $"SELECT * FROM {DEPARTMENT_TABLE} WHERE {DEPARTMENT_ID} NOT IN (" +
                            $"SELECT {PROJECT_ASSIGNMENT_DEPARTMENT_ID} FROM {PROJECT_ASSIGNMENT_TABLE} " +
                            $"WHERE {PROJECT_ASSIGNMENT_PROJECT_ID} IN (" +
                            $"SELECT {PROJECT_ID} FROM {PROJECT_TABLE} " +
                            $"WHERE {PROJECT_ID} NOT LIKE '{project.ID}' AND {PROJECT_PROPRESS} NOT LIKE '100'" +
                            $"AND {PROJECT_START} <= Convert(Datetime, '{project.End}', 105) " +
                            $"AND {PROJECT_END} >= Convert(Datetime, '{project.Start}', 105))))";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }

        public List<Project> SearchProjectByEmployeeID(string employeeID)
        {
            //string sqlStr = $"SELECT * FROM {PROJECT_TABLE} WHERE {PROJECT_ID} IN " +
            //                $"(SELECT {PROJECT_ASSIGNMENT_PROJECT_ID} FROM {PROJECT_ASSIGNMENT_DEPARTMENT_ID}, {EMPLOYEE_TABLE} " +
            //                $"WHERE {PROJECT_ASSIGNMENT_TABLE}.{PROJECT_ASSIGNMENT_DEPARTMENT_ID} = {EMPLOYEE_TABLE}.{EMPLOYEE_DEPARTMENT_ID} " +
            //                $"AND {EMPLOYEE_TABLE}.{EMPLOYEE_ID} =  '{employeeID}')";
            string sqlStr = "SELECT * FROM Project WHERE project_id IN (SELECT project_id FROM ProjectAssignment, Employee WHERE ProjectAssignment.department_id = Employee.department_id AND Employee.employee_id =  'EM001')";

            return dbConnection.GetList(sqlStr, reader => new Project(reader));
        }

        public List<Project> SearchProjectByCreatorID(string managerID)
        {
            string sqlStr = $"SELECT * FROM {PROJECT_TABLE} WHERE {PROJECT_ID} IN " +
                            $"(SELECT {PROJECT_ASSIGNMENT_PROJECT_ID} FROM {PROJECT_ASSIGNMENT_DEPARTMENT_ID}, {EMPLOYEE_TABLE} " +
                            $"WHERE {PROJECT_ASSIGNMENT_TABLE}.{PROJECT_ASSIGNMENT_DEPARTMENT_ID} = {EMPLOYEE_TABLE}.{EMPLOYEE_DEPARTMENT_ID} " +
                            $"AND {PROJECT_CREATE_BY} =  '{managerID}')";
            return dbConnection.GetList(sqlStr, reader => new Project(reader));
        }
    }
}
