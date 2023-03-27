using CompanyManagement.Models;
using System.Collections.Generic;

namespace CompanyManagement.Database.Interfaces
{
    public interface IProjectAssignmentDao
    {
        List<Department> GetAllDepartmentInProject(string projectID);
        List<EmployeeAccount> GetEmployeesInProject(string projectID);
        List<Department> GetDepartmentsCanAssignWork(string startTime, string endTime);
    }
}
