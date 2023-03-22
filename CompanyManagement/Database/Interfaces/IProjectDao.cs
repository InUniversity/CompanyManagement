using System.Collections.Generic;

namespace CompanyManagement.Database.Interfaces
{
    public interface IProjectDao
    {
        void Add(Project project);
        void Delete(string id);
        void Update(Project project);
        List<Project> GetAll();
        Project SearchByID(string id);
    }
}
