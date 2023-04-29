using System.Collections.Generic;
using CompanyManagement.Database.Base;

namespace CompanyManagement.Strategies.UserControls.ProjectsView
{
    public class ProjectsStrategyFactory
    {
        private static readonly Dictionary<string, IProjectsStrategy> strategies = new()
        {
            {BaseDao.MANAGER_ROLE_ID, new ProjectsForManager()},
            {BaseDao.DEPARTMENT_HEAD_ROLE_ID, new ProjectsForDepartmentHead()},
            {BaseDao.EMPLOYEE_ROLE_ID, new ProjectsForEmployee()}
        };

        public static IProjectsStrategy Create(string roleID)
        {
            return strategies.TryGetValue(roleID, out var projectsStrategy) ? projectsStrategy : null;
        }
    }
}