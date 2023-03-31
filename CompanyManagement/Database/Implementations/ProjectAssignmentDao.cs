using System.Collections.Generic;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.Models;

namespace CompanyManagement.Database.Implementations
{
    public class ProjectAssignmentDao : BaseDao, IProjectAssignmentDao
    {
        public void Add(ProjectAssignment projectAssignment)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(ProjectAssignment projectAssignment)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ProjectAssignment projectAssignment)
        {
            throw new System.NotImplementedException();
        }

        public List<Department> GetAllDepartmentInProject(string projectID)
        {
            string sqlStr = $"SELECT * FROM {DEPARTMENT_TABLE} WHERE {DEPARTMENT_ID} IN(" +
               $"SELECT {PROJECT_ASSIGNMENT_DEPARTMENT_ID} FROM {PROJECT_ASSIGNMENT_TABLE} WHERE {PROJECT_ASSIGNMENT_PROJECT_ID} = '{projectID}')";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));          
        }

        public List<EmployeeAccount> GetEmployeesInProject(string projectID)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEE_TABLE} WHERE {EMPLOYEE_DEPARTMENT_ID} IN(" +
                $"SELECT {PROJECT_ASSIGNMENT_DEPARTMENT_ID} FROM {PROJECT_ASSIGNMENT_TABLE} WHERE {PROJECT_ASSIGNMENT_PROJECT_ID} = '{projectID}')";
            return dbConnection.GetList(sqlStr, reader => new EmployeeAccount(reader));
        }
        
        public List<Department> GetDepartmentsCanAssignWork(string startTime, string endTime)
        {
            string sqlStr = $"SELECT * FROM {DEPARTMENT_TABLE} WHERE {DEPARTMENT_ID} NOT IN (" +
                            $"Select {PROJECT_ASSIGNMENT_DEPARTMENT_ID} FROM {PROJECT_ASSIGNMENT_TABLE} " +
                            $"WHERE {PROJECT_ASSIGNMENT_PROJECT_ID} IN (" +
                            $"Select {PROJECT_ID} FROM {PROJECT_TABLE} " +
                            $"WHERE {PROJECT_PROPRESS} NOT LIKE '100'" +
                            $"AND {PROJECT_START} <= '{endTime}'" +
                            $"AND {PROJECT_END} >= '{startTime}'))";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }
    }
}
