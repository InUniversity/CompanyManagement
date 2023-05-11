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
                BaseDao.managerPermsID => new MainForManager(),
                BaseDao.deptHeadPermsID => new MainForDeptHead(),
                BaseDao.emplPerms => new MainForEmployee(),
                BaseDao.hrPermsID => new MainForHR(),
                _ => throw new ArgumentOutOfRangeException("Not found role ID")
            };
        } 
    }
}