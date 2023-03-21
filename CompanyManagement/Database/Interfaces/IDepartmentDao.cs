using System.Collections.Generic;
using CompanyManagement.Models;

namespace CompanyManagement.Database.Interfaces
{
    public interface IDepartmentDao
    {
        List<Department> GetAll();
        List<Department> GetDepartmentsCanAssignWork(string startTime, string endTime);
    }
}
