using System;
using CompanyManagement.Enums;
using CompanyManagement.Models;

namespace CompanyManagement.Strategies.UserControls.ProjectsView
{
    public class ProjectsStrategyFactory
    {
        public static IProjectsStrategy Create(EPermission perms)
        {
            return perms switch
            {
                EPermission.Mgr => new ProjectsForManager(),
                EPermission.DepHead => new ProjectsForDepartmentHead(),
                EPermission.NorEmpl => new ProjectsForEmployee(),
                _ => throw new ArgumentOutOfRangeException("Projedt strategy: Not found permssion")
            };
        }
    }
}