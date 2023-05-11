using System;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Strategies.Windows.MainView
{
    public class MainStrategyFactory
    {
        public static IMainStrategy Create(Permission perms)
        {
            return perms switch
            {
                Permission.Mgr => new MainForManager(),
                Permission.DepHead => new MainForDeptHead(),
                Permission.NorEmpl => new MainForEmployee(),
                Permission.HR => new MainForHR(),
                _ => throw new ArgumentOutOfRangeException("Main Strategy: Not found permssion")
            };
        } 
    }
}