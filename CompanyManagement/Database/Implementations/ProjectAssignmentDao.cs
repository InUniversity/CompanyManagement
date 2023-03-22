using System.Collections.Generic;

namespace CompanyManagement.Database.Implementations
{
    public class ProjectAssignmentDao : BaseDao
    {
        public List<Employee> GetEmployeesInProject(string projectID)
        {
            string sqlStr = $"SELECT * FROM {EMPLOYEE_TABLE} WHERE {EMPLOYEE_DEPARTMENT_ID} IN(" +
                $"SELECT {PROJECT_ASSIGNMENT_DEPARTMENT_ID} FROM {PROJECT_ASSIGNMENT_TABLE} WHERE {PROJECT_ASSIGNMENT_PROJECT_ID} = '{projectID}')";
            List<Employee> employees = dbConnection.GetList(sqlStr, reader => new Employee(reader));
            return employees;
        }
    }
}
