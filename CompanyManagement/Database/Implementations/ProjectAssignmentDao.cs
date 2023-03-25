using System.Collections.Generic;
using CompanyManagement.Database.Interfaces;

namespace CompanyManagement.Database.Implementations
{
    public class ProjectAssignmentDao : BaseDao, IProjectAssignmentDao
    {
        public List<EmployeeAccount> GetEmployeesInProject(string projectID)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEE_TABLE} WHERE {EMPLOYEE_DEPARTMENT_ID} IN(" +
                $"SELECT {PROJECT_ASSIGNMENT_DEPARTMENT_ID} FROM {PROJECT_ASSIGNMENT_TABLE} WHERE {PROJECT_ASSIGNMENT_PROJECT_ID} = '{projectID}')";
            List<EmployeeAccount> employees = dbConnection.GetList(sqlStr, reader => new EmployeeAccount(reader));
            return employees;
        }
    }
}
