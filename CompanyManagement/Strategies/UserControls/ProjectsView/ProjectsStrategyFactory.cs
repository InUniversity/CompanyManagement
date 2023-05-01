using System.Collections.Generic;
using CompanyManagement.Database.Base;

namespace CompanyManagement.Strategies.UserControls.ProjectsView
{
    public class ProjectsStrategyFactory
    {
        private static readonly Dictionary<string, IProjectsStrategy> strategies = new()
        {
            {BaseDao.managerRole, new ProjectsForManager()},
            {BaseDao.deptHeadRole, new ProjectsForDepartmentHead()},
            {BaseDao.regularEmplRole, new ProjectsForEmployee()}
        };

        public static IProjectsStrategy Create(string roleID)
        {
            return strategies.TryGetValue(roleID, out var projectsStrategy) ? projectsStrategy : null;
        }
    }
}