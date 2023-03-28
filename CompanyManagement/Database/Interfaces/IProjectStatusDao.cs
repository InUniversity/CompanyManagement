using CompanyManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyManagement.Database.Interfaces
{
    public interface IProjectStatusDao
    {
        void Add(ProjectStatus projectStatus);
        void Delete(string projectStatusID);
        void Update(ProjectStatus projectStatus);
        List<ProjectStatus> GetAll();
    }
}
