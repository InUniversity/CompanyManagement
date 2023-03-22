using System.Collections.Generic;
using CompanyManagement.Models;

namespace CompanyManagement.Database.Interfaces
{
    public interface ITaskInProjectDao
    {
        void Add(TaskInProject task);
        void Delete(string id);
        void Update(TaskInProject task);
        TaskInProject SearchByID(string taskInProjectID);
        List<TaskInProject> SearchByProjectID(string projectID);
    }
}
