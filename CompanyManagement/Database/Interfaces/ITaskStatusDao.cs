using CompanyManagement.Models;
using System.Collections.Generic;

namespace CompanyManagement.Database.Interfaces
{
    public interface ITaskStatusDao
    {
        void Add(TaskStatus taskStatus);
        void Delete(string taskStatusID);
        void Update(TaskStatus taskStatus);
        TaskStatus SearchByID(string taskStatus);
        List<TaskStatus> GetAll();
    }
}
