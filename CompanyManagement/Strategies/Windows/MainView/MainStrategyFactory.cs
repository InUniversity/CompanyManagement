using System;
using CompanyManagement.Enums;

namespace CompanyManagement.Strategies.Windows.MainView
{
    public class MainStrategyFactory
    {
        public static IMainStrategy Create(EPermission perms)
        {
            return perms switch
            {
                EPermission.Mgr => new MainForManager(),
                EPermission.DepHead => new MainForDeptHead(),
                EPermission.NorEmpl => new MainForEmployee(),
                EPermission.HR => new MainForHR(),
                _ => throw new ArgumentOutOfRangeException("Main Strategy: Not found permssion")
            };
        } 
    }
}