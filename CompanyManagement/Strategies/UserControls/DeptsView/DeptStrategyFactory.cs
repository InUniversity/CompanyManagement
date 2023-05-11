using System;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Strategies.UserControls.DeptsView
{
    public class DeptStrategyFactory
    {
        public static IDeptStrategy Create(Permission perms)
        {
            return perms switch
            {
                Permission.Mgr => new DeptForManager(),
                Permission.DepHead => new DeptForDeptHead(),
                Permission.HR => new DeptForHR(),
                _ => throw new ArgumentOutOfRangeException("Department Strategy: Not found permssion")
            };
        } 
    }
}
