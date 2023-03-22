using System.Collections.Generic;
using CompanyManagement.Models;

namespace CompanyManagement.Database.Interfaces
{
    public interface ITaskInProject
    {
        void Add(TaskInProject task);
        void Delete(string id);
        void Update(TaskInProject task);
        List<TaskInProject> SearchByProjectID(string projectID);
    }
}
