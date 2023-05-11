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
                BaseDao.managerPermsID => new DeptForManager(),
                BaseDao.deptHeadPermsID => new DeptForDeptHead(),
                BaseDao.hrPermsID => new DeptForHR(),
                _ => throw new ArgumentOutOfRangeException("Not found role ID")
            };
        } 
    }
}
