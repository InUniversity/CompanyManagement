using System;
using CompanyManagement.Database.Base;

namespace CompanyManagement.Strategies.UserControls.DeptsView
{
    public class DeptStrategyFactory
    {
        public static IDeptStrategy Create(string roleID)
        {
            return roleID switch
            {
                BaseDao.managerRole => new DeptForManager(),
                BaseDao.deptHeadRole => new DeptForDeptHead(),
                BaseDao.hrRole => new DeptForHR(),
                _ => throw new ArgumentOutOfRangeException("Not found role ID")
            };
        } 
    }
}
