using System.Collections.Generic;

namespace CompanyManagement.Database.Interfaces
{
    public interface IProjectAssignmentDao
    {
        List<EmployeeAccount> GetEmployeesInProject(string projectID);
    }
}
