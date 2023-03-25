using CompanyManagement.Models;
using System.Collections.Generic;

namespace CompanyManagement.Database.Interfaces
{
    public interface IProjectAssignmentDao
    {
        List<Employee> GetEmployeesInProject(string projectID);
        List<Department> GetAllDepartmentInProject(string projectID);
    }
}
