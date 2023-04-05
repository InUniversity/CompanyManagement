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
        List<Employee> GetEmployeesInProject(string projectID);
        List<Department> GetDepartmentsCanAssignWork(Project project);
        List<Project> SearchProjectByEmployeeID(string employeeID);
        List<Project> SearchProjectByCreatorID(string managerID);
    }
}
