using System;
using CompanyManagement.Models;

namespace CompanyManagement.Strategies.UserControls.ProjectsView
{
    public class ProjectsStrategyFactory
    {
        public static IProjectsStrategy Create(Permission perms)
        {
            return perms switch
            {
                Permission.Mgr => new ProjectsForManager(),
                Permission.DepHead => new ProjectsForDepartmentHead(),
                Permission.NorEmpl => new ProjectsForEmployee(),
                _ => throw new ArgumentOutOfRangeException("Projedt strategy: Not found permssion")
            };
        }
    }
}