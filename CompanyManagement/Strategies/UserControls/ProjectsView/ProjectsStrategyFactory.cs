using System.Collections.Generic;
using CompanyManagement.Database.Base;

namespace CompanyManagement.Strategies.UserControls.ProjectsView
{
    public class ProjectsStrategyFactory
    {
        private static readonly Dictionary<string, IProjectsStrategy> strategies = new()
        {
            {BaseDao.MANAGER_POS_ID, new ProjectsForManager()},
            {BaseDao.DEPARTMENT_HEAD_POS_ID, new ProjectsForDepartmentHead()},
            {BaseDao.EMPLOYEE_POS_ID, new ProjectsForEmployee()}
        };

        public static IProjectsStrategy Create(string positionID)
        {
            return strategies.TryGetValue(positionID, out var projectsStrategy) ? projectsStrategy : null;
        }
    }
}