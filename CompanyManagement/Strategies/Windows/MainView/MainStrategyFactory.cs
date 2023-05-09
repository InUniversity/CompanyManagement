using System;
using CompanyManagement.Database.Base;

namespace CompanyManagement.Strategies.Windows.MainView
{
    public class MainStrategyFactory
    {
        public static IMainStrategy Create(string roleID)
        {
            return roleID switch
            {
                BaseDao.managerRole => new MainForManager(),
                BaseDao.deptHeadRole => new MainForDeptHead(),
                BaseDao.regularEmplRole => new MainForEmployee(),
                BaseDao.hrRole => new MainForHR(),
                _ => throw new ArgumentOutOfRangeException("Not found role ID")
            };
        } 
    }
}