using CompanyManagement.Models;
using System.Collections.Generic;
using CompanyManagement.Database.Implementations;

namespace CompanyManagement.Database.Interfaces
{
    public interface IProjectAssignmentDao
    {
        void Add(ProjectAssignment projectAssignment);
        void Delete(ProjectAssignment projectAssignment);
        List<Department> GetAllDepartmentInProject(string projectID);
        List<EmployeeAccount> GetEmployeesInProject(string projectID);
        List<Department> GetDepartmentsCanAssignWork(Project project);
    }
}
