using System;
using CompanyManagement.Enums;

namespace CompanyManagement.Strategies.UserControls.DeptsView
{
    public class DeptStrategyFactory
    {
        public static IDeptStrategy Create(EPermission perms)
        {
            return perms switch
            {
                EPermission.Mgr => new DeptForManager(),
                EPermission.DepHead => new DeptForDeptHead(),
                EPermission.HR => new DeptForHR(),
                _ => throw new ArgumentOutOfRangeException("Department Strategy: Not found permssion")
            };
        } 
    }
}
